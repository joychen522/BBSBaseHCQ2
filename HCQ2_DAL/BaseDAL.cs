using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HCQ2_IDAL;
using HCQ2_DI;
using Spring.Data.Core;

namespace HCQ2_DAL_MSSQL
{
    /// <summary>
    ///  MSSQL 数据层父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDAL<T> : AdoDaoSupport, IBaseDAL<T> where T : class, new()
    {
        /// <summary>
        ///  数据上下文对象
        ///  创建方式：通过控制反转DI(Spring.net)
        /// </summary>
        private readonly DbContext _db = SpringHelper.GetObject<DBContextFactory>("DBContextFactory").GetDbContext(); 

        /// <summary>
        ///  EF上下文对象 提供给子类使用
        /// </summary>
        protected DbContext db
        {
            get { return _db; }
        }

        #region 1.0 新增: 实体 +int Add(T model)
        /// <summary>
        ///  1.0 新增: 实体 +int Add(T model)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public int Add(T model)
        {
            db.Set<T>().Add(model);
            return db.SaveChanges();//保存成功后，会将自增的id设置给 model的 主键属性，并返回受影响行数
        }
        #endregion

        #region 2.1 删除：根据实体删除+int Delete(T mdoel)

        /// <summary>
        ///  根据实体删除
        /// </summary>
        /// <returns></returns>
        public int Delete(T model)
        {
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
            return db.SaveChanges();
        }
        #endregion

        #region 2.2删除：根据条件查询+int Delete
        /// <summary>
        ///  根据条件查询
        /// </summary>
        /// <param name="delWhere">func条件</param>
        /// <returns></returns>
        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> delWhere)
        {
            //筛选待删除的数据
            List<T> listDeleteing = db.Set<T>().Where(delWhere).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T>().Attach(o);//将数据添加到EF容器
                db.Set<T>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
        #endregion

        #region 3.1 更新
        /// <summary>
        ///  根据数据对象更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="proNames"></param>
        /// <returns></returns>
        public int Modify(T model, params string[] proNames)
        {
            //3.1将实体对象添加到EF容器
            DbEntityEntry entry = db.Entry(model);
            //3.2 先设置对象的包装状态为：Unchanged
            entry.State = EntityState.Unchanged;
            //循环遍历需要修改的字段数组
            foreach (string proName in proNames)
            {
                //3.3将每个被修改的属性的状态设置为已修改，作用为：为以后生成update语句时，就只更新被修改的属性
                entry.Property(proName).IsModified = true;
            }
            //3.4 一次性生成sql update语句执行更新
            return db.SaveChanges();
        }
        #endregion

        #region 3.2 批量更新
        /// <summary>
        ///  3.2批量修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="modifiedProNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public int Modify(T model, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            //3.2.1 查询要被修改的记录
            List<T> listModifing = db.Set<T>().Where(whereLambda).ToList();
            //3.2.2 获取实体类 类型对象
            Type t = typeof(T);
            //通过反射获取类的实例，共有属性
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            //3.2.3 创建实体属性 字典集合
            var dictPros = new Dictionary<string, PropertyInfo>();
            //3.2.4 将实体属性 中修改的属性名 添加到字典集合中键：属性名 值：属性对象
            proInfos.ForEach(p =>
            {
                if (modifiedProNames.Contains(p.Name))
                    dictPros.Add(p.Name, p);
            });
            //3.2.5 循环要修改的属性名
            foreach (string proName in modifiedProNames)
            {
                //如果集合包含当前字段
                if (dictPros.ContainsKey(proName))
                {
                    //获取当前属性对象
                    PropertyInfo proInfo = dictPros[proName];
                    object newValue = proInfo.GetValue(model, null);
                    foreach (T user in listModifing)
                        proInfo.SetValue(user, newValue, null);
                }
            }
            //3.2.6 一次性生成语句执行
            return db.SaveChanges();
        }
        #endregion

        #region 4.1 根据条件查询
        /// <summary>
        ///  根据条件查询
        /// </summary>
        /// <param name="whereLambda">Lamdba条件表达式</param>
        /// <returns></returns>
        public List<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).ToList();
        }
        #endregion

        #region 4.2 根据条件查询，并排序
        /// <summary>
        ///  根据条件查询，并排序
        /// </summary>
        /// <typeparam name="TKey">排序类型>注：为数据类型</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="isAsc">是否升序排列，默认为升序，false为降序</param>
        /// <returns></returns>
        public List<T> Select<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            if (isAsc)
                return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).ToList();
            return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).ToList();
        }

        #endregion

        #region 4.3 根据条件查询，排序，并且分页
        /// <summary>
        ///   根据条件查询，排序，并且分页
        /// </summary>
        /// <typeparam name="TKey">排序属性>注：为数据类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="pageIndex">当前显示第几页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="isAsc">是否升序排列</param>
        /// <returns></returns>
        public List<T> Select<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TKey>> orderBy, int pageIndex, int pageSize, bool isAsc = true)
        {
            if (isAsc)
                return db.Set<T>().Where(whereLambda).OrderBy(orderBy).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return db.Set<T>().Where(whereLambda).OrderByDescending(orderBy).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        #endregion

        #region 4.4 查询：根据条件获取数据量 +int SelectCount(Expression<Func<T, bool>> whereLambda)

        /// <summary>
        ///  查询：根据条件获取数据量
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        public int SelectCount(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
                return db.Set<T>().Count();
            return db.Set<T>().Where(whereLambda).Count();
        }
        #endregion
    }
}
