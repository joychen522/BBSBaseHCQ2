using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  备注：数据层父接口IDAL
    ///  创建人：陈敏
    ///  创建时间：2016-11-05
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDAL<T> where T : class, new()
    {
        #region 1.0 新增: 实体 +int Add(T model)
        /// <summary>
        ///  1.0 新增 实体 +int Add(T model)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        int Add(T model);

        #endregion

        #region 2.1 删除：根据实体删除+int Delete(T mdoel)

        /// <summary>
        ///  根据实体删除
        /// </summary>
        /// <returns></returns>
        int Delete(T model);
        #endregion

        #region 2.2 删除：根据条件删除+int Delete(Expression<Func<T, bool>> delWhere)
        /// <summary>
        ///  根据条件删除
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> delWhere);
        #endregion

        #region 3.1 更新 + int Modify(T model, params string[] proNames)
        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        int Modify(T model, params string[] proNames);

        #endregion

        #region 3.2 更新：根据条件更新+int Modify
        /// <summary>
        ///  根据条件更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="modifiedProNames">要修改的 属性 名称</param>
        /// <returns></returns>
        int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames);
        #endregion
         
        #region 4.1查询：根据条件查询
        /// <summary>
        ///  根据条件查询
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        List<T> Select(Expression<Func<T, bool>> whereLambda);
        #endregion

        #region 4.2查询：根据条件查询，排序
        /// <summary>
        ///  根据条件查询，排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// <param name="isAsc">是否升序排列</param>
        /// <returns></returns>
        List<T> Select<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc);
        #endregion

        #region 4.3查询：根据条件，排序，分页
        /// <summary>
        ///  根据条件，排序，分页
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="pageIndex">当前显示第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="isAsc">是否升序排列</param>
        /// <returns></returns>
        List<T> Select<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, int pageIndex, int pageSize, bool isAsc);
        #endregion

        #region 4.4查询：根据条件获取数据量 +int SelectCount(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        ///  查询：根据条件获取数据量
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        int SelectCount(Expression<Func<T, bool>> whereLambda); 
        #endregion
    }
}

