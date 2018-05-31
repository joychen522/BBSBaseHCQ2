using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  元素业务接口层
    /// </summary>
    public partial interface IT_PageElementBLL
    {
        /// <summary>
        ///  根据folder_id菜单Id获取元素集合
        /// </summary>
        /// <param name="folder_id">菜单ID</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementDataByFolderId(int folder_id,int page,int rows);
        /// <summary>
        ///  根据页面父ID 获取相关页面的元素集合
        /// </summary>
        /// <param name="folder_pid">页面父ID</param>
        /// <returns></returns>
        List<HCQ2_Model.ExtendsionModel.T_PageElementModel> GetElementByFolderPId(int folder_pid,string sm_code);
        /// <summary>
        ///  根据页面ID获取 页面元素
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementById(int folder_id);
    }
}
