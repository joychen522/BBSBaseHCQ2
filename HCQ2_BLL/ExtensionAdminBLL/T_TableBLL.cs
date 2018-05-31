using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model.TreeModel;

namespace HCQ2_BLL
{
    public partial class T_TableBLL:HCQ2_IBLL.IT_TableBLL
    {
       public List<TableStrcutTreeModel> GetTableTreeModel()
        {
            var query = Select(s => (!string.IsNullOrEmpty(s.table_name)));
            if (query == null)
                return null;
            List<TableStrcutTreeModel> list = new List<TableStrcutTreeModel>();
            foreach(var item in query)
            {
                list.Add(new TableStrcutTreeModel() {
                     text=item.table_cname,
                     table_name=item.table_name,
                     key_name=item.table_key
                });
            }
            return list;
        }
       public List<HCQ2_Model.T_TableField> GetTableDataByName(string tableName, string fieldName, int page, int rows)
        {
            if (string.IsNullOrEmpty(tableName))
                return null;
            if(string.IsNullOrEmpty(fieldName))
                return DBSession.IT_TableFieldDAL.Select<int>(s => s.table_name == tableName, s => s.field_id, page, rows, true);
            else
                return DBSession.IT_TableFieldDAL.Select<int>(s => s.table_name == tableName && (s.field_name.Contains(fieldName) || s.field_cname.Contains(fieldName)), s => s.field_id, page, rows, true);
        }
    }
}
