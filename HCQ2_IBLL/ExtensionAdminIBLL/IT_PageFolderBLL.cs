using System;
using System.Collections.Generic;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  菜单管理业务层接口
    /// </summary>
    public partial interface IT_PageFolderBLL
    {
        /// <summary>
        ///  获取指定菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="sm_code"></param>
        /// <param name="isAll">是否获取全部菜单</param>
        /// <returns></returns>
        List<HCQ2_Model.TreeModel.TreeTableAttribute> GetMenuDataByPid(int id,string type,string sm_code,bool isAll=false);
        /// <summary>
        ///  保存编辑菜单
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool EditPageFolder(HCQ2_Model.T_PageFolder folder,int id);
        /// <summary>
        ///  保存新增菜单
        /// </summary>
        /// <param name="folder">菜单对象</param>
        /// <param name="folder_pid">父ID</param>
        /// <returns></returns>
        int AddFolder(HCQ2_Model.T_PageFolder folder,int folder_pid);
        /// <summary>
        ///  删除菜单
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        bool DelFolder(int folder_id);
        /// <summary>
        ///  根据id排序
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="type">排序类型</param>
        /// <returns>上一个/下一个元素id</returns>
        int OrderMenuById(int id,string type);
        /// <summary>
        ///  获取框架页菜单
        /// </summary>
        /// isAll：是否全部
        /// isExcel：是否排除子节点
        /// <returns></returns>
        List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> GetMainMenu(string sm_code, bool isAll = false,bool isExcel=true);
    }
}
