using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HCQ2_DI;
using HCQ2_IBLL;
using HCQ2_IDAL;

namespace HCQ2_BLL
{
    public abstract class BaseBLL<T>:IBaseBLL<T> where T : class, new()
    {
        #region  参数
        //业务层父类中 要定义一个 数据操作对象，方便 后面的方法里 操作数据层方法
        //但是，业务父类中 无法确定 要用 实例化数据层 的哪个类
        protected HCQ2_IDAL.IBaseDAL<T> Dal = null;
        
        /// <summary>
        ///  2.0 数据仓储接口（相当于数据层工厂，可以创建所有的数据子类对象）
        /// </summary>
        private IDBSession iDbSession;
        #endregion

        //所以，声明一个 抽象方法，要子类去重写，并在重写方法中，为 业务父类 里的 数据操作对象实例化
        public abstract void SetDal();

        protected BaseBLL()
        {
            SetDal();
        }
        /// <summary>
        ///  获取或者设置：数据仓储接口对象
        ///  创建DBSession对象
        /// </summary>
        public IDBSession DBSession
        {
            get
            {
                if (iDbSession == null)
                {
                    #region 方法一： 注释
                    //1：读取配置文件
                    //string strFctoryDLL = System.AppDomain.CurrentDomain.BaseDirectory;
                    //strFctoryDLL = strFctoryDLL + Common.ConfigurationHelper.AppSetting("DBSessionFatoryDLL");
                    //string strFctoryType = Common.ConfigurationHelper.AppSetting("DBSessionFatory");
                    //2：通过反射创建DBSessionFactory 工厂对象
                    //根据配置文件内容 使用 DI层里的Spring.Net 创建 DBSessionFactory 工厂对象
                    //Assembly dalDLL = Assembly.LoadFrom(strFctoryDLL);
                    //Type typeDBSessionFactory = dalDLL.GetType(strFctoryType);
                    //HCQ2_IDAL.IDBSessionFactory sessionFactory = Activator.CreateInstance(typeDBSessionFactory) as HCQ2_IDAL.IDBSessionFactory;
                    #endregion

                    //方法二：通过Spring.NET创建工厂对象
                    HCQ2_IDAL.IDBSessionFactory sessionFactory = SpringHelper.GetObject<HCQ2_IDAL.IDBSessionFactory>("iDalSessFactory");
                    //3：通过工厂 创建DBSession对象
                    iDbSession = sessionFactory.GetDBSession();
                }
                return iDbSession;
            }
        }

        #region BaseIBLL<T> 成员
        #region 1.0 新增 实体 +int Add(T model)
        /// <summary>
        ///  1.0 新增 实体 +int Add(T model)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Add(T model)
        {
            return Dal.Add(model);
        }

        #endregion

        #region 2.1 删除：根据实体对象+int Delete(T model);
        /// <summary>
        ///  根据实体对象+int Delete(T model);
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(T model)
        {
            return Dal.Delete(model);
        }
        #endregion

        #region 2.2 删除：根据条件+int Delete(Expression<Func<T, bool>> delWhere);
        /// <summary>
        ///  根据lambda条件删除数据
        /// </summary>
        /// <param name="delWhere">lambda条件</param>
        /// <returns></returns>
        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> delWhere)
        {
            return Dal.Delete(delWhere);
        }
        #endregion

        #region 3.0 更新：根据条件批量+ int Modify
        /// <summary>
        ///  根据条件，待修改字段数组批量修改记录
        /// </summary>
        /// <param name="model">实体，需要修改为的数据集</param>
        /// <param name="whereLambda">Lambda条件</param>
        /// <param name="modifiedProNames">字段集合</param>
        /// <returns></returns>
        public int Modify(T model, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            return Dal.Modify(model, whereLambda, modifiedProNames);
        }
        #endregion

        #region 3.1 更新对象 + int Modify(T model, params string[] modifiedProNames)
        /// <summary>
        ///  更新对象
        /// </summary>
        /// <param name="model">实体，需要修改为的数据集</param>
        /// <param name="modifiedProNames">字段集合</param>
        /// <returns></returns>
        public int Modify(T model, params string[] modifiedProNames)
        {
            return Dal.Modify(model, modifiedProNames);
        } 
        #endregion

        #region 4.1 查询：根据条件+ List<T> Select(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        ///  根据Lambda条件查询并返回List记录
        /// </summary>
        /// <param name="whereLambda">Lambda条件</param>
        /// <returns></returns>
        public List<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return Dal.Select(whereLambda);
        }
        #endregion

        #region 4.2 查询：根据条件排序+List<T> Select<TKey>
        /// <summary>
        ///  根据条件排序查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="isAsc">是否升序排列，默认为升序，false为降序</param>
        /// <returns></returns>
        public List<T> Select<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            return Dal.Select(whereLambda, orderLambda, isAsc);
        }
        #endregion

        #region 4.3 查询：条件，排序，分页+List<T> Select<TKey>
        /// <summary>
        ///  分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda">Lambda条件</param>
        /// <param name="orderLambda">Lambda排序</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="isAsc">是否升序排列，默认为升序，false为降序</param>
        /// <returns></returns>
        public List<T> Select<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize, bool isAsc = true)
        {
            return Dal.Select(whereLambda, orderLambda, pageIndex, pageSize, isAsc);
        }
        #endregion

        #region 4.4 查询：统计数据 +int SelectCount(Expression<Func<T, bool>> whereLambda)

        /// <summary>
        ///  查询：统计数据
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        public int SelectCount(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.SelectCount(whereLambda);
        }
        #endregion

        #region 5.0 封装 Expression<Func<T, bool>>表达式
        /// <summary>
        ///  封装 Expression<Func<T, bool>>表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="options">待封装的数组</param>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetConditionExpression<T>(string[] options, string fieldName)
        {
            ParameterExpression left = Expression.Parameter(typeof(T), "c");//c=>
            Expression expression = Expression.Constant(false);
            foreach (var optionName in options)
            {
                Expression right = Expression.Call
                (
                    Expression.Property(left, typeof(T).GetProperty(fieldName)),  //c.DataSourceName 首先是反射获取c的一个属性 
                    typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),// 声明一个string.Contains的方法     c.DataSourceName.Contains()                反射使用.Contains()方法                        
                    Expression.Constant(optionName)           // .Contains(optionName) 封装常量
                );
                expression = Expression.Or(right, expression);//c.DataSourceName.contain("") || c.DataSourceName.contain("") 
            }
            Expression<Func<T, bool>> finalExpression
                = Expression.Lambda<Func<T, bool>>(expression, new ParameterExpression[] { left });
            return finalExpression;
        }
        #endregion

        #endregion
    }
}
