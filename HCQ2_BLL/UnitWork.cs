using System;
using System.Linq;
using System.Linq.Expressions;

namespace HCQ2_BLL
{
    public class UnitWork: HCQ2_IBLL.IUnitWork
    {
        private HCQ2_IDAL.IUnitWork unitWork = HCQ2_DI.SpringHelper.GetObject<HCQ2_IDAL.IUnitWork>("DalUnitWork");

        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return unitWork.Find<T>(exp);
        }

        public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return unitWork.IsExist<T>(exp);
        }

        /// <summary>
        /// 查找单个
        /// </summary>
        public T FindSingle<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return unitWork.FindSingle<T>(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="orderby">排序，格式如："Id"/"Id descending"</param>
        public IQueryable<T> Find<T>(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null) where T : class
        {
            return unitWork.Find<T>(pageindex, pagesize, orderby, exp);
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return unitWork.GetCount<T>(exp);
        }

        public void Add<T>(T entity) where T : class
        {
            unitWork.Add<T>(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd<T>(T[] entities) where T : class
        {
            unitWork.BatchAdd<T>(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            unitWork.Update<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            unitWork.Delete<T>(entity);
        }

        /// <summary>
        /// 按指定id更新实体,会更新整个实体
        /// </summary>
        /// <param name="identityExp">The identity exp.</param>
        /// <param name="entity">The entity.</param>
        public void Update<T>(Expression<Func<T, object>> identityExp, T entity) where T : class
        {
            unitWork.Update<T>(identityExp, entity);
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class
        {
            unitWork.Update<T>(where, entity);
        }

        public virtual void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        {
            unitWork.Delete<T>(exp);
        }

        public void Save()
        {
            unitWork.Save();
        }
    }
}
