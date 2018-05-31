using System;
using System.Collections.Generic;
using System.Linq;
using HCQ2_Model;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_FolderPermissRelationDAL:HCQ2_IDAL.IT_FolderPermissRelationDAL
    {
        public int Delete(List<string> folder_ids, int per_id)
        {
            if (folder_ids== null || folder_ids.Count<=0)
                return 0;
            var listDeleteing = (from o in db.Set<T_FolderPermissRelation>()
                where folder_ids.Contains(o.folder_id.ToString()) && o.per_id == per_id
                select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T_FolderPermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<T_FolderPermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
