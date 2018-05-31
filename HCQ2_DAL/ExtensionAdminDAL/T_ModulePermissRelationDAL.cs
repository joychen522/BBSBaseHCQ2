using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_ModulePermissRelationDAL:HCQ2_IDAL.IT_ModulePermissRelationDAL
    {
        /// <summary>
        ///  删除之前配置的权限
        /// </summary>
        /// <param name="sm_id"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        public int Delete(List<int> sm_id, int per_id)
        {
            if (sm_id == null || sm_id.Count <= 0)
                return 0;
            var listDeleteing = (from o in db.Set<T_ModulePermissRelation>()
                                 where sm_id.Contains(o.sm_id) && o.per_id == per_id
                                 select o).ToList();
            listDeleteing.ForEach(o =>
            {
                db.Set<T_ModulePermissRelation>().Attach(o);//将数据添加到EF容器
                db.Set<T_ModulePermissRelation>().Remove(o);//标记容器为删除
            });
            return db.SaveChanges();//生成sql删除语句一次性删除
        }
    }
}
