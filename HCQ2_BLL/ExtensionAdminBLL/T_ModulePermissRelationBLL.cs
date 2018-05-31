using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    public partial class T_ModulePermissRelationBLL:HCQ2_IBLL.IT_ModulePermissRelationBLL
    {
        /// <summary>
        ///  保存权限--模块对于关系
        /// </summary>
        /// <param name="menus">待保存的模块数据集合</param>
        /// <param name="reak">标记</param>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public bool SaveModulePerData(string userData, string reak, int per_id)
        {
            if (reak.Equals("undeal"))
                return true;//无需后端处理
            if (per_id <= 0)
                return false;//权限主键值有误
            //1. 判断是否删除全部
            if (string.IsNullOrEmpty(userData) || userData.Replace(";", "").Trim().Length == 0)
            {
                Delete(s => s.per_id == per_id);
                return true;
            }
            //2. 保存之前删除之前设置的权限
            string[] menu = userData.Split(';');//0添加，1删除
            string[] temp = menu[1].Trim(',').Split(',');
            List<int> strList = new List<int>();
            foreach (var item in temp)
                strList.Add(HCQ2_Common.Helper.ToInt(item));
            if (menu.Length > 1 && !string.IsNullOrEmpty(menu[1].Trim(',')))
                DBSession.IT_ModulePermissRelationDAL.Delete(strList, per_id);
            //3. 添加前先判断
            if (string.IsNullOrEmpty(menu[0].Trim(',').Trim()))
                return true;
            string[] str = menu[0].Trim(',').Split(',');//添加
            if (str.Length > 0)
            {
                foreach (string item in str)
                {
                    DBSession.IT_ModulePermissRelationDAL.Add(
                        new T_ModulePermissRelation()
                        {
                            sm_id = HCQ2_Common.Helper.ToInt(item),
                            per_id = per_id
                        });
                }
            }
            return true;
        }
    }
}
