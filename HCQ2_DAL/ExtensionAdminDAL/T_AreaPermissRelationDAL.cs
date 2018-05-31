using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_AreaPermissRelationDAL:HCQ2_IDAL.IT_AreaPermissRelationDAL
    {
        public int Delete(List<string> unit_ids, int per_id)
        {
            if (unit_ids == null || unit_ids.Count <= 0)
                return 0;
            var listDeleteing = (from o in db.Set<T_AreaPermissRelation>()
                                 where unit_ids.Contains(o.area_code) && o.per_id == per_id
                                 select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T_AreaPermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<T_AreaPermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
