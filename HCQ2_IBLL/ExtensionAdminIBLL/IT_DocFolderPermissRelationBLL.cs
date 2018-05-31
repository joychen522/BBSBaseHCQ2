using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_DocFolderPermissRelationBLL
    {
        /// <summary>
        ///  保存文档菜单--权限设置
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        bool SaveDocMenuLimitData(string menus, string reak, int per_id);
        /// <summary>
        ///  根据权限id获取对应文档目录树
        /// </summary>
        /// <param name="per_id">权限id</param>
        /// <returns></returns>
        List<T_DocFolderPermissRelation> GetDocMenuLimitData(int per_id);
    }
}
