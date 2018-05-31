using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_ItemCodeMenumBLL
    {
        /// <summary>
        /// 获取所有的详细字典
        /// </summary>
        /// <returns></returns>
        List<T_ItemCodeMenum> GetItemCodeMenu();



        /// <summary>
        /// 添加具体字典
        /// </summary>
        /// <param name="itemCodeMenu"></param>
        /// <returns></returns>
        bool AddCodeItemMenu(T_ItemCodeMenum itemCodeMenu);

      
        /// <summary>
        /// 获取分类目录树
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetMenuTree(List<T_ItemCodeMenum> list);

        #region MRV 商品分类

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="code_name"></param>
        /// <param name="code_value"></param>
        /// <param name="code_note"></param>
        /// <returns></returns>
        bool AddGoodsType(string code_name,string code_value,string code_note);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="code_id"></param>
        /// <returns></returns>
        bool DeleteGoodsType(int code_id);

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="code_name"></param>
        /// <param name="code_value"></param>
        /// <param name="code_note"></param>
        /// <param name="code_id"></param>
        /// <returns></returns>
        bool UpdateGoodsType(string code_name, string code_value, string code_note,int code_id);

        /// <summary>
        /// 首页显示数据源JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string ReturnPageJson(object obj);

        #endregion
    }
}
