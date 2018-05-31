using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2_BLL
{
    public partial class T_ItemCodeBLL : HCQ2_IBLL.IT_ItemCodeBLL
    {
        /// <summary>
        /// 获取所有的字典说明
        /// </summary>
        /// <returns></returns>
        public List<T_ItemCode> GetItemCode()
        {
            return base.Select(o => o.item_id != 0).OrderBy(o => o.item_id).ToList();
        }

        /// <summary>
        /// 根据ItemCode获取字典说明
        /// </summary>
        /// <returns></returns>
        public T_ItemCode GetByItemCode(string item_code)
        {
            return base.Select(o => o.item_code == item_code).FirstOrDefault();
        }

        /// <summary>
        /// 根据ItemName获取字典说明
        /// </summary>
        /// <returns></returns>
        public T_ItemCode GetByItemName(string item_name)
        {
            return base.Select(o => o.item_name == item_name).FirstOrDefault();
        }

        /// <summary>
        /// 根据ItemName获取字典说明
        /// </summary>
        /// <returns></returns>
        public T_ItemCode GetByItemId(int item_id)
        {
            return base.Select(o => o.item_id == item_id).FirstOrDefault();
        }

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool AddItemCode(object obj)
        {
            bool isAccess = false;
            FormCollection param = (FormCollection)obj;
            string code_id = param["code_id"];
            string item_id = param["item_id"];

            string input_item_name = param["input_name"];
            string input_item_code = param["input_code"];
            string input_item_note = param["input_note"];
            string input_code_type = param["input_code_type"];
            
            if (string.IsNullOrEmpty(item_id) && string.IsNullOrEmpty(code_id))
            {
                //如果有相同的code，则不能添加。
                if (base.Select(o => o.item_code == input_item_code).Count() > 0)
                    return isAccess;

                //添加字典说明表
                T_ItemCode itemCode = new T_ItemCode();
                itemCode.item_name = input_item_name;
                itemCode.item_code = input_item_code;
                itemCode.item_note = input_item_note;
                itemCode.creator_date = DateTime.Now;
                itemCode.creator_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
                itemCode.creator_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name;
                isAccess = base.Add(itemCode) > 0;
            }
            else if (!string.IsNullOrEmpty(item_id) && string.IsNullOrEmpty(code_id))
            {
                //添加第一层字典表
                T_ItemCodeMenumBLL codeBll = new T_ItemCodeMenumBLL();

                //如果有相同的code，则不能添加。
                if (codeBll.GetByItemId(int.Parse(item_id)).Where(o=>o.code_value == input_item_code).Count() > 0)
                    return isAccess;

                T_ItemCodeMenum codeMenu = codeBll.GetByItemId(int.Parse(item_id)).Where(o => o.code_pid == 0).LastOrDefault();
                int? maxOrder = codeMenu != null ? codeMenu.code_order + 1 : 1;
                codeMenu = new T_ItemCodeMenum();
                codeMenu.item_id = int.Parse(item_id);
                codeMenu.code_name = input_item_name;
                codeMenu.code_value = input_item_code;
                codeMenu.code_note = input_item_note;
                codeMenu.if_system = 0;
                codeMenu.if_child = 0;
                codeMenu.code_pid = 0;
                codeMenu.code_order = maxOrder;
                if (!string.IsNullOrEmpty(input_code_type))
                {
                    codeMenu.code_type = input_code_type;
                }
                isAccess = codeBll.Add(codeMenu) > 0;
            }
            else if (!string.IsNullOrEmpty(item_id) && !string.IsNullOrEmpty(code_id))
            {
                //添加字典表字典的子节点
                T_ItemCodeMenumBLL codeBll = new T_ItemCodeMenumBLL();

                //如果有相同的code，则不能添加。
                if (codeBll.GetByItemId(int.Parse(item_id)).Where(o => o.code_value == input_item_code).Count() > 0)
                    return isAccess;

                T_ItemCodeMenum codeMenu = codeBll.GetByItemId(int.Parse(item_id)).Where(o => o.code_pid == int.Parse(code_id)).Max();
                int? maxOrder = codeMenu != null ? codeMenu.code_order + 1 : 1;
                codeMenu = new T_ItemCodeMenum();
                codeMenu.item_id = int.Parse(item_id);
                codeMenu.code_name = input_item_name;
                codeMenu.code_value = input_item_code;
                codeMenu.code_note = input_item_note;
                codeMenu.if_system = 0;
                codeMenu.if_child = 0;
                codeMenu.code_pid = int.Parse(code_id);
                codeMenu.code_order = maxOrder;
                if (!string.IsNullOrEmpty(input_code_type))
                {
                    codeMenu.code_type = input_code_type;
                }
                isAccess = codeBll.Add(codeMenu) > 0;
            }
            return isAccess;
        }

        /// <summary>
        /// 编辑数据字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool EditItemCode(object obj)
        {
            bool isAccess = false;
            FormCollection param = (FormCollection)obj;
            int code_id = int.Parse(param["code_id"]);
            int item_id = int.Parse(param["item_id"]);
            string item_type = param["item_type"];
            string input_item_name = param["input_name"];
            string input_item_code = param["input_code"];
            string input_item_note = param["input_note"];
            string code_type = param["code_type"];
            if (item_type == "M")
            {
                T_ItemCode itemcode = new T_ItemCode();
                itemcode.item_name = input_item_name;
                itemcode.item_code = input_item_code;
                itemcode.item_note = input_item_note;
                isAccess = base.Modify(itemcode, o => o.item_id == item_id, "item_name", "item_code", "item_note") > 0;
            }
            else if (item_type == "N")
            {
                T_ItemCodeMenumBLL menu_bll = new T_ItemCodeMenumBLL();
                T_ItemCodeMenum menu = new T_ItemCodeMenum();
                menu.code_name = input_item_name;
                menu.code_value = input_item_code;
                menu.code_note = input_item_note;
                menu.code_type = code_type;
                isAccess = menu_bll.Modify(menu, o => o.code_id == code_id, "code_name", "code_value", "code_note","code_type") > 0;
            }

            return isAccess;
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteItemCode(object obj)
        {
            bool isAccess = false;
            FormCollection param = (FormCollection)obj;
            string code_type = param["item_type"];
            if (code_type == "M")
            {
                int item_id = int.Parse(param["item_id"]);
                isAccess = base.Delete(o => o.item_id == item_id) > 0;
                T_ItemCodeMenumBLL menuBll = new T_ItemCodeMenumBLL();
                menuBll.Delete(o => o.item_id == item_id);
            }
            else if (code_type == "N")
            {
                int code_id = int.Parse(param["code_id"]);
                T_ItemCodeMenumBLL menuBll = new T_ItemCodeMenumBLL();
                isAccess = menuBll.Delete(o => o.code_id == code_id) > 0;
                menuBll.Delete(o => o.code_pid == code_id);
            }
            return isAccess;
        }

        /// <summary>
        /// 返回页面的json
        /// </summary>
        /// <returns></returns>
        public string ReturnPageJson(object obj)
        {
            FormCollection param = (FormCollection)obj;
            T_ItemCodeMenumBLL _bll_codeitemmenu = new T_ItemCodeMenumBLL();
            List<T_ItemCodeMenum> list = _bll_codeitemmenu.GetItemCodeMenu();
            int page = int.Parse(param["page"]);
            int rows = int.Parse(param["rows"]);
            if (!string.IsNullOrEmpty(param["item_id"]))
                list = list.Where(o => o.item_id == int.Parse(param["item_id"])).ToList();
            if (!string.IsNullOrEmpty(param["code_id"]))
                list = list.Where(o => o.code_id == int.Parse(param["code_id"])).ToList();
            if (!string.IsNullOrEmpty(param["txtSearchName"]))
                list = list.Where(o => o.code_name.Contains(param["txtSearchName"])).ToList();

            List<T_ItemCode> listItemCode = GetItemCode();
            if (!string.IsNullOrEmpty(param["item_id"]))
                listItemCode = listItemCode.Where(o => o.item_id == int.Parse(param["item_id"])).ToList();
            if (!string.IsNullOrEmpty(param["code_id"]))
                listItemCode = listItemCode.Where(o => o.item_id == -1).ToList();
            if (!string.IsNullOrEmpty(param["txtSearchName"]))
                listItemCode = listItemCode.Where(o => o.item_name.Contains(param["txtSearchName"])).ToList();

            DisplayCode dis = new DisplayCode();
            List<DisplayCode> displayList = new List<DisplayCode>();
            if (listItemCode.Count() > 0)
            {
                for (int i = 0; i < listItemCode.Count(); i++)
                {
                    dis = new DisplayCode();
                    dis.code_id = 0;
                    dis.code_name = listItemCode[i].item_name;
                    dis.code_value = listItemCode[i].item_code;
                    dis.code_note = listItemCode[i].item_note;
                    dis.item_id = listItemCode[i].item_id;
                    dis.item_type = "M";
                    displayList.Add(dis);
                }
            }
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    dis = new DisplayCode();
                    dis.code_id = list[i].code_id;
                    dis.code_name = list[i].code_name;
                    dis.code_value = list[i].code_value;
                    dis.code_note = list[i].code_note;
                    dis.item_id = list[i].item_id;
                    dis.code_type = list[i].code_type;
                    dis.item_type = "N";
                    displayList.Add(dis);
                }
            }
            return HCQ2_Common.ReturnJson.GetReturnJson<DisplayCode>(displayList, page, rows);
        }

        /// <summary>
        /// 验证ItemCode是否重复
        /// </summary>
        /// <param name="item_code"></param>
        /// <returns></returns>
        public bool ValidataItemCode(string item_code)
        {
            bool isAccess = false;
            if (base.Select(o => o.item_code == item_code).Count() <= 0)
                isAccess = true;
            return isAccess;
        }

        public List<T_ItemCodeMenum> GetItemByCode(string fieldCode)
        {
            if (string.IsNullOrEmpty(fieldCode))
                return null;
            T_ItemCode item = Select(s => s.item_code == fieldCode).FirstOrDefault();
            if (item == null)
                return null;
            List<T_ItemCodeMenum> list =
                DBSession.IT_ItemCodeMenumDAL.Select(s => s.item_id == item.item_id);
            return list;
        }

        #region 字典树

        /// <summary>
        /// 返回字符串
        /// </summary>
        StringBuilder sbJson = new StringBuilder();

        /// <summary>
        /// 获取所有的字典目录树t_itemcode第一级目录
        /// </summary>
        /// <returns></returns>
        public string GetItemTreeJson(List<T_ItemCode> listItemCode)
        {
            T_ItemCodeMenumBLL codeMenuBll = new T_ItemCodeMenumBLL();
            List<T_ItemCodeMenum> listMenu = codeMenuBll.GetItemCodeMenu();
            sbJson.Append("[");
            T_ItemCode itemCode = new T_ItemCode();
            for (int i = 0; i < listItemCode.Count; i++)
            {
                if (i != 0)
                    sbJson.Append(" , ");
                itemCode = listItemCode[i];
                sbJson.Append(AddModleItem(itemCode));
                var data = listMenu.Where(o => o.item_id == itemCode.item_id && o.code_pid == 0);
                if (data.Count() > 0)
                {
                    sbJson.Append(" ,nodes:  ");
                    sbJson.Append(GetItemCodeMenuTreeJson(data.ToList()));
                    sbChild = new StringBuilder();
                    sbJson.Append(" } ");
                }
                else
                {
                    sbJson.Append(" } ");
                }
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }

        /// <summary>
        /// 需求返回的子节点字符串T_ItemCodeMenum
        /// </summary>
        StringBuilder sbChild = new StringBuilder();
        private string GetItemCodeMenuTreeJson(List<T_ItemCodeMenum> list)
        {
            T_ItemCodeMenumBLL codeMenuBll = new T_ItemCodeMenumBLL();
            List<T_ItemCodeMenum> listMenu = codeMenuBll.GetItemCodeMenu();

            sbChild.Append("[");
            T_ItemCodeMenum itemCodeMenum = new T_ItemCodeMenum();
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sbChild.Append(" , ");
                itemCodeMenum = list[i];
                sbChild.Append(AddModleItemMenu(itemCodeMenum));
                var data = listMenu.Where(o => o.code_pid == itemCodeMenum.code_id);
                if (data.Count() > 0)
                {
                    sbChild.Append(" ,nodes:  ");
                    GetItemCodeMenuTreeJson(data.ToList());
                    sbChild.Append(" } ");
                }
                else
                {
                    sbChild.Append(" } ");
                }
            }
            sbChild.Append("]");
            return sbChild.ToString();
        }

        /// <summary>
        /// 添加itemcodemenum表的数据
        /// </summary>
        /// <param name="menum"></param>
        /// <returns></returns>
        private string AddModleItemMenu(T_ItemCodeMenum menum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ \"code_name\" :\"" + menum.code_name + "\" ");
            sb.Append(", \"item_id\" :\"" + menum.item_id + "\" ");
            sb.Append(", \"code_value\" :\"" + menum.code_value + "\" ");
            sb.Append(", \"text\" :\"" + menum.code_name + "\" ");
            sb.Append(", \"code_id\" :\"" + menum.code_id + "\" ");
            return sb.ToString();
        }

        /// <summary>
        /// 需要添加的itemcode表的数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private string AddModleItem(T_ItemCode itemCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ \"item_name\" :\"" + itemCode.item_name + "\" ");
            sb.Append(", \"item_id\" :\"" + itemCode.item_id + "\" ");
            sb.Append(", \"item_code\" :\"" + itemCode.item_code + "\" ");
            sb.Append(", \"text\" :\"" + itemCode.item_name + "\" ");
            return sb.ToString();
        }

        #endregion
    }


    /// <summary>
    /// 返回字典页面显示的数据
    /// </summary>
    public class DisplayCode
    {
        /// <summary>
        /// name
        /// </summary>
        public string code_name { get; set; }
        /// <summary>
        /// value、code
        /// </summary>
        public string code_value { get; set; }
        /// <summary>
        /// note
        /// </summary>
        public string code_note { get; set; }
        /// <summary>
        /// code_id
        /// </summary>
        public int code_id { get; set; }
        /// <summary>
        /// item_id
        /// </summary>
        public int item_id { get; set; }
        /// <summary>
        /// 分类状态
        /// </summary>
        public string item_type { get; set; }
        /// <summary>
        /// 字典类别
        /// </summary>
        public string code_type { get; set; }
    }

}
