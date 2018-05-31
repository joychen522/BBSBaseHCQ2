using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_DocFolderPermissRelationDAL:HCQ2_IDAL.IT_DocFolderPermissRelationDAL
    {
        /// <summary>
        ///  根据权限删除配置权限
        /// </summary>
        /// <param name="folder_ids"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        public int Delete(List<string> folder_ids, int per_id)
        {
            if (folder_ids == null || folder_ids.Count <= 0)
                return 0;
            var listDeleteing = (from o in db.Set<T_DocFolderPermissRelation>()
                                 where folder_ids.Contains(o.folder_id.ToString()) && o.per_id == per_id
                                 select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T_DocFolderPermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<T_DocFolderPermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
