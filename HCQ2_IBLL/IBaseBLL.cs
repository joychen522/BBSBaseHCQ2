using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  说明：业务类接口
    ///  封装业务中基本的方法增、删、该、查
    /// </summary>
    public interface IBaseBLL<T> where T:class,new()
    {
        #region 1.0 新增：实体 +int Add(T model)
        /// <summary>
        ///   1.0 新增 实体 +int Add(T model)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        int Add(T model);
        #endregion

        #region 2.1 删除：根据实体对象+int Delete(T model);
        /// <summary>
        ///  2.1 删除：根据实体对象
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        int Delete(T model);
        #endregion

        #region 2.2 删除：根据条件+int Delete(Expression<Func<T, bool>> delWhere);
        /// <summary>
        ///  2.2 删除 根据lambda条件删除数据
        /// </summary>
        /// <param name="delWhere">lambda条件</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> delWhere);
        #endregion

        #region 3.0 更新：根据条件批量+ int Modify
        /// <summary>
        ///  3.0 更新 根据条件，待修改字段数组批量修改记录
        /// </summary>
        /// <param name="model">实体，需要修改为的数据集</param>
        /// <param name="whereLambda">Lambda条件</param>
        /// <param name="modifiedProNames">字段集合</param>
        /// <returns></returns>
        int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames);
        #endregion

        #region 4.1 查询：根据条件+ List<T> Select(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        ///  4.1 查询 根据Lambda条件查询并返回List记录
        /// </summary>
        /// <param name="whereLambda">Lambda条件</param>
        /// <returns></returns>
        List<T> Select(Expression<Func<T, bool>> whereLambda);
        #endregion

        #region 4.2 查询：根据条件排序+List<T> Select<TKey>
        /// <summary>
        ///  4.2 查询 根据条件排序查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="isAsc">是否升序排列，默认为升序，false为降序</param>
        /// <returns></returns>
        List<T> Select<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true);
        #endregion

        #region 4.3 查询：条件，排序，分页+List<T> Select<TKey>
        /// <summary>
        ///  4.3 查询 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda">Lambda条件</param>
        /// <param name="orderLambda">Lambda排序</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="isAsc">是否升序排列，默认为升序，false为降序</param>
        /// <returns></returns>
        List<T> Select<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize, bool isAsc = true);
        #endregion

        #region 4.4 查询：统计数据 +int SelectCount(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        ///  查询：统计数据
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        int SelectCount(Expression<Func<T, bool>> whereLambda);
        #endregion

        #region 5.0 封装 Expression<Func<T, bool>>表达式
        /// <summary>
        ///  封装 Expression<Func<T, bool>>表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="options">待封装的数组</param>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        Expression<Func<T, bool>> GetConditionExpression<T>(string[] options, string fieldName);

        #endregion
    }
}
