using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2_BLL
{
    public partial class T_ItemCodeMenumBLL : HCQ2_IBLL.IT_ItemCodeMenumBLL
    {
        /// <summary>
        /// 获取所有的详细字典
        /// </summary>
        /// <returns></returns>
        public List<T_ItemCodeMenum> GetItemCodeMenu()
        {
            return base.Select(o => o.item_id != 0).OrderBy(o => o.item_id).ToList();
        }

        /// <summary>
        /// 根据ItemCode表的ItemId来获取字典
        /// </summary>
        /// <returns></returns>
        public List<T_ItemCodeMenum> GetByItemId(int item_id)
        {
            return base.Select(o => o.item_id == item_id);
        }

        /// <summary>
        /// 添加具体字典
        /// </summary>
        /// <param name="itemCodeMenu"></param>
        /// <returns></returns>
        public bool AddCodeItemMenu(T_ItemCodeMenum itemCodeMenu)
        {
            return base.Add(itemCodeMenu) > 0;
        }

        /// <summary>
        /// 获取分类目录树
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMenuTree(List<T_ItemCodeMenum> list)
        {
            List<Dictionary<string, object>> nodeList = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var item in list)
            {
                dic = new Dictionary<string, object>();
                dic.Add("text", item.code_name);
                dic.Add("code_name", item.code_name);
                dic.Add("code_value", item.code_value);
                dic.Add("code_id", item.code_id);
                dic.Add("code_pid", item.code_pid);
                var data = list.Where(o => o.code_pid == item.code_id);
                if (data.Count() > 0)
                {
                    dic.Add("nodes", GetNodesTree(list, data.ToList()));
                }
                nodeList.Add(dic);
            }
            return nodeList;
        }

        /// <summary>
        /// 获取子节点信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="nodesList"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetNodesTree(List<T_ItemCodeMenum> list, List<T_ItemCodeMenum> nodesList)
        {
            List<Dictionary<string, object>> nodeList = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var item in nodesList)
            {
                dic = new Dictionary<string, object>();
                dic.Add("text", item.code_name);
                dic.Add("code_name", item.code_name);
                dic.Add("code_value", item.code_value);
                dic.Add("code_id", item.code_id);
                dic.Add("code_pid", item.code_pid);
                var data = list.Where(o => o.code_pid == item.code_id);
                if (data.Count() > 0)
                {
                    dic.Add("nodes", GetNodesTree(list, data.ToList()));
                }
                nodeList.Add(dic);
            }
            return nodeList;
        }


        #region MRV 商品分类

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="code_name"></param>
        /// <param name="code_value"></param>
        /// <param name="code_note"></param>
        /// <returns></returns>
        public bool AddGoodsType(string code_name, string code_value, string code_note)
        {
            T_ItemCodeBLL itemCodeBll = new T_ItemCodeBLL();
            T_ItemCode item_code = itemCodeBll.GetByItemCode("food_type");
            if (item_code != null)
            {
                List<T_ItemCodeMenum> list = GetByItemId(item_code.item_id);
                T_ItemCodeMenum Menu = new T_ItemCodeMenum();
                Menu.item_id = item_code.item_id;
                Menu.code_name = code_name;
                Menu.code_value = code_value;
                Menu.code_note = code_note;
                Menu.code_pid = 0;
                Menu.if_system = 0;
                Menu.if_child = 0;
                Menu.code_order = list == null ? 1 : list[list.Count() - 1].code_order + 1;
                return base.Add(Menu) > 0;
            }
            else
                return false;
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="code_name"></param>
        /// <param name="code_value"></param>
        /// <param name="code_note"></param>
        /// <param name="code_id"></param>
        /// <returns></returns>
        public bool UpdateGoodsType(string code_name, string code_value, string code_note, int code_id)
        {
            T_ItemCodeMenum Menu = new T_ItemCodeMenum();
            Menu.code_name = code_name;
            Menu.code_value = code_value;
            Menu.code_note = code_note;
            return base.Modify(Menu, o => o.code_id == code_id, "code_name", "code_value", "code_note") > 0;
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="code_id"></param>
        /// <returns></returns>
        public bool DeleteGoodsType(int code_id)
        {
            return base.Delete(o => o.code_id == code_id) > 0;
        }

        /// <summary>
        /// 首页显示数据源JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ReturnPageJson(object obj)
        {
            FormCollection param = (FormCollection)obj;
            T_ItemCodeBLL itemCodeBll = new T_ItemCodeBLL();
            T_ItemCode item_code = itemCodeBll.GetByItemCode("food_type");
            List<T_ItemCodeMenum> list = GetByItemId(item_code.item_id);
            int page = int.Parse(param["page"]);
            int rows = int.Parse(param["rows"]);
            if (!string.IsNullOrEmpty(param["txtSearchName"]))
            {
                list = list.Where(o => o.code_name.Contains(param["txtSearchName"])).ToList();
            }
            var data = list.Skip((page * rows) - rows).Take(rows).OrderByDescending(o => o.code_order);
            return "{\"total\":" + list.Count() + ",\"rows\":" + HCQ2_Common.JsonHelper.ObjectToJson(data) + "}";
        }
        #endregion
    }
}
