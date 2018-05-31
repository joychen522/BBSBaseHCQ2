using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  页面元素数据层接口
    /// </summary>
    public partial interface IT_PageElementDAL
    {
        /// <summary>
        ///  根据页面父ID 获取 相关元素集合
        /// </summary>
        /// <param name="folder_pid">页面父ID</param>
        /// <returns></returns>
        List<HCQ2_Model.ExtendsionModel.T_PageElementModel> GetElementByFolderId(int folder_pid,string sm_code);
    }
}
