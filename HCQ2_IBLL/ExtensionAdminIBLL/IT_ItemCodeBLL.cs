using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_ItemCodeBLL
    {
        /// <summary>
        /// 获取所有的字典说明
        /// </summary>
        /// <returns></returns>
        List<T_ItemCode> GetItemCode();

        /// <summary>
        /// 根据ItemCode获取字典说明
        /// </summary>
        /// <returns></returns>
        T_ItemCode GetByItemCode(string item_code);

        /// <summary>
        /// 根据ItemName获取字典说明
        /// </summary>
        /// <returns></returns>
        T_ItemCode GetByItemName(string item_name);

        /// <summary>
        /// 根据ItemName获取字典说明
        /// </summary>
        /// <returns></returns>
        T_ItemCode GetByItemId(int item_id);

        /// <summary>
        /// 新增数据字典，包括判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool AddItemCode(object obj);

        /// <summary>
        /// 编辑数据字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool EditItemCode(object obj);

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool DeleteItemCode(object obj);

        /// <summary>
        /// 返回页面的json
        /// </summary>
        /// <returns></returns>
        string ReturnPageJson(object obj);

        /// <summary>
        /// 获取所有的字典目录树
        /// </summary>
        /// <returns></returns>
        string GetItemTreeJson(List<T_ItemCode> listItemCode);

        /// <summary>
        /// 验证ItemCode是否重复
        /// </summary>
        /// <param name="item_code"></param>
        /// <returns></returns>
        bool ValidataItemCode(string item_code);

        //**************************Joychen*****************************
        /// <summary>
        ///  根据code获取字典项
        /// </summary>
        /// <param name="fieldCode"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_ItemCodeMenum> GetItemByCode(string fieldCode);
    }
}
