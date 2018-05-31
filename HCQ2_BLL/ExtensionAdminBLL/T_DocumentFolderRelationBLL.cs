using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class T_DocumentFolderRelationBLL:HCQ2_IBLL.IT_DocumentFolderRelationBLL
    {
        /// <summary>
        ///  添加对应关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddDocFolderRelation(T_DocumentFolderRelation model)
        {
            if (null == model)
                return 0;
            return Add(model);
        }
    }
}
