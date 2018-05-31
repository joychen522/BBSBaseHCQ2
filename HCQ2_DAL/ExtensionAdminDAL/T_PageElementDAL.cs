using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ExtendsionModel;

namespace HCQ2_DAL_MSSQL
{
    /// <summary>
    ///  元素管理数据层实现
    /// </summary>
    public partial class T_PageElementDAL:HCQ2_IDAL.IT_PageElementDAL
    {
        /// <summary>
        ///  父页面ID 获取元素实现
        /// </summary>
        /// <param name="folder_pid"></param>
        /// <returns></returns>
        public List<HCQ2_Model.ExtendsionModel.T_PageElementModel> GetElementByFolderId(int folder_pid,string sm_code)
        {
            var query = (from o in db.Set<T_PageFolder>()
                join c in db.Set<T_PageElement>()
                    on o.folder_id equals c.folder_id
                where o.sm_code.Equals(sm_code)
                select new
                {
                    c.folder_id,
                    c.pe_id,
                    c.pe_name
                }).ToList();
            List<HCQ2_Model.ExtendsionModel.T_PageElementModel> list = new List<T_PageElementModel>();
            foreach (var item in query)
            {
                list.Add(new T_PageElementModel()
                {
                    folder_id=item.folder_id,
                    pe_id=item.pe_id,
                    pe_name=item.pe_name
                });
            }
            return list;
        }
    }
}
