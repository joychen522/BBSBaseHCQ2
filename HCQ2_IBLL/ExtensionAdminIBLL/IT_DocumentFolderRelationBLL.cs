using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  文档-目录对应关系 业务接口定义
    /// </summary>
     public partial interface IT_DocumentFolderRelationBLL
    {
        /// <summary>
        ///  添加对应关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddDocFolderRelation(T_DocumentFolderRelation model);
    }
}
