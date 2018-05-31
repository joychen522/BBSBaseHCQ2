using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_ElementPermissRelationDAL:HCQ2_IDAL.IT_ElementPermissRelationDAL
    {
        public int Delete(List<string> pe_ids, int per_id)
        {
            if (pe_ids == null || pe_ids.Count <= 0)
                return 0;
            var listDeleteing = (from o in db.Set<T_ElementPermissRelation>()
                where pe_ids.Contains(o.pe_id.ToString()) && o.per_id == per_id
                select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T_ElementPermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<T_ElementPermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
