using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_UserPermissRelationDAL:HCQ2_IDAL.IBane_UserPermissRelationDAL
    {
        /// <summary>
        ///  需要删除的权限配置
        /// </summary>
        /// <param name="unit_ids">单位代码集合</param>
        /// <param name="per_id">权限代码</param>
        /// <returns></returns>
        public int Delete(List<string> unit_ids, int per_id)
        {
            if (unit_ids == null || unit_ids.Count <= 0)
                return 0;
            var listDeleteing = (from o in db.Set<Bane_UserPermissRelation>()
                                 where unit_ids.Contains(o.user_id.ToString()) && o.per_id == per_id
                                 select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<Bane_UserPermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<Bane_UserPermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
