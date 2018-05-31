using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    /// <summary>
    ///  菜单--权限 业务实现层
    /// </summary>
    public partial class T_FolderPermissRelationBLL:HCQ2_IBLL.IT_FolderPermissRelationBLL
    {
        /// <summary>
        ///  保存菜单--权限设置
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        public bool SaveMenuLimitData(string menus,string reak, int per_id)
        {
            if (reak.Equals("undeal"))
                return true;//无需后端处理
            if (per_id <= 0)
                return false;
            //1. 判断是否删除全部
            if (string.IsNullOrEmpty(menus) || menus.Replace(";", "").Trim().Length == 0){
                Delete(s => s.per_id == per_id);
                return true;
            }
            //2. 保存之前删除之前设置的权限
            string[] menu = menus.Split(';');
            if (menu.Length > 1 && !string.IsNullOrEmpty(menu[1].Trim(',')))
                DBSession.IT_FolderPermissRelationDAL.Delete(new List<string>(menu[1].Trim(',').Split(',')), per_id);
            //3. 添加前先判断
            if (string.IsNullOrEmpty(menu[0].Trim(',').Trim()))
                return true;
            string[] str = menu[0].Trim(',').Split(',');//添加
            if (str.Length > 0)
            {
                foreach (string item in str)
                {
                    DBSession.IT_FolderPermissRelationDAL.Add(
                        new HCQ2_Model.T_FolderPermissRelation()
                        {
                            folder_id = HCQ2_Common.Helper.ToInt(item),
                            per_id = per_id
                        });
                }
            }
            return true;
        }
        /// <summary>
        ///  获取菜单项实现
        /// </summary>
        /// <param name="per_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_FolderPermissRelation> GetMenuLimitData(int per_id)
        {
            if (per_id <= 0)
                return null;
            return Select(s => s.per_id == per_id);
        }
    }
}
