using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_IBLL;
using HCQ2_Model;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2_BLL
{
    /// <summary>
    ///  元素业务实现层
    /// </summary>
    public partial class T_PageElementBLL: IT_PageElementBLL
    {
        /// <summary>
        ///  根据folder_id菜单Id获取元素集合
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_PageElement> GetElementDataByFolderId(int folder_id, int page, int rows)
        {
            if (folder_id <= 0)
                return null;
            return base.Select<int>(s => s.folder_id == folder_id, s => s.pe_order,
                true);
        }
        /// <summary>
        ///  根据页面父ID 获取相关元素集合
        /// </summary>
        /// <param name="folder_pid"></param>
        /// <returns></returns>
        public List<HCQ2_Model.ExtendsionModel.T_PageElementModel> GetElementByFolderPId(int folder_pid,string sm_code)
        {
            return DBSession.IT_PageElementDAL.GetElementByFolderId(folder_pid,sm_code);
        }
        /// <summary>
        ///  根据页面ID 获取 元素
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        public List<T_PageElement> GetElementById(int folder_id)
        {
            return Select(s => s.folder_id == folder_id);
        }
    }
}
