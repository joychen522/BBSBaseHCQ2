using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using System.Web.Mvc;

namespace HCQ2_BLL
{
    public partial class T_TodoListBLL : HCQ2_IBLL.IT_TodoListBLL
    {
        /// <summary>
        /// 获取所有的信箱信息
        /// </summary>
        /// <returns></returns>
        public List<T_TodoList> GetTodoList()
        {
            return base.Select(o => o.to_remove == 0).OrderByDescending(o => o.to_id).ToList();
        }

        /// <summary>
        /// 根据ID获取详细
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        public T_TodoList GetByToId(int to_id)
        {
            return base.Select(o => o.to_remove == 0 && o.to_id == to_id).FirstOrDefault();
        }

        /// <summary>
        /// 获取该用户的信件
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_TodoList> GetByUserId(int user_id)
        {
            List<T_TodoList> userList = new List<T_TodoList>();
            var list = GetTodoList();
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.to_user_id.ToString()))
                {
                    string[] sendUser = item.to_user_id.ToString().Split(';');
                    for (int i = 0; i < sendUser.Length; i++)
                    {
                        if (sendUser[i] == user_id.ToString())
                        {
                            userList.Add(item);
                            break;
                        }
                    }
                }
            }
            return userList;
        }

        /// <summary>
        /// 获取该用户发送的信件
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_TodoList> GetByUserSend(int user_id)
        {
            return GetTodoList().Where(o => o.to_send_user_id == user_id).ToList();
        }

        /// <summary>
        /// 写信
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool NewTodo(object obj)
        {
            FormCollection param = (FormCollection)obj;
            List<T_User> userList = HCQ2UI_Helper.OperateContext.Current.bllSession.T_User.Select(o => o.user_id > 0);

            string[] sendUserName = param["to_user_id"].Split(';');
            string sendUserID = "";
            for (int i = 0; i < sendUserName.Length; i++)
            {
                string user_name = sendUserName[i];
                var data = userList.Where(o => o.user_name == user_name);
                if (data.Count() > 0)
                {
                    if (string.IsNullOrEmpty(sendUserID))
                    {
                        sendUserID = data.FirstOrDefault().user_id.ToString();
                    }
                    else
                    {
                        sendUserID += ";" + data.FirstOrDefault().user_id.ToString();
                    }
                }
            }

            T_TodoList todo = new T_TodoList();
            todo.to_user_id = sendUserID;
            todo.to_user_name = param["to_user_id"];
            todo.to_title = param["to_title"];
            todo.to_content = param["content"];

            todo.to_send_user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            todo.to_send_user_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name;
            todo.to_send_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (HCQ2UI_Helper.OperateContext.Current.Usr.login_name == "develop")
            {
                todo.is_system = 1;
            }
            else
            {
                todo.is_system = 0;
            }
            todo.to_remove = 0;

            return base.Add(todo) > 0;
        }

        /// <summary>
        /// 根据ID删除信件
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        public bool DeleteTodo(int to_id)
        {
            T_TodoList todo = new T_TodoList();
            todo.to_remove = 1;
            return base.Modify(todo, o => o.to_id == to_id, "to_remove") > 0;
        }

        /// <summary>
        /// 获取当前人员的所有信件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TableModel GetUserAllTodoList(object obj)
        {
            FormCollection param = (FormCollection)obj;
            TableModel modle = new TableModel();

            //当前登陆用户ID
            int user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;

            //接收还是发送，1：接收，0：发送
            string itemType = "1";
            if (!string.IsNullOrEmpty(param["itemType"]))
            {
                itemType = param["itemType"] == "0001" ? "1" : "0";
            }

            //数据源
            var dataSoure = itemType == "1" ? SendTodo(user_id) : ReceiveTodo(user_id);

            int page = int.Parse(param["page"]);
            int rows = int.Parse(param["rows"]);
            if (!string.IsNullOrEmpty(param["title"]))
            {
                string seatchTitle = param["title"];
                dataSoure = dataSoure.Where(o => o.to_title.Contains(seatchTitle)).ToList();
            }

            modle.rows = dataSoure.Skip((page * rows) - rows).Take(rows);
            modle.total = dataSoure.Count();

            return modle;
        }

        /// <summary>
        /// 获取用户目录树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetUserTree()
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            List<T_User> userList = HCQ2UI_Helper.OperateContext.Current.bllSession.T_User.Select(o => o.user_id > 0);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var user in userList)
            {
                dic = new Dictionary<string, object>();
                dic.Add("user_id", user.user_id);
                dic.Add("user_name", user.user_name);
                dic.Add("text", user.user_name);
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// 获取用户已经发送的消息
        /// </summary>
        /// <param name="user_id"></param>
        public List<T_TodoList> ReceiveTodo(int user_id)
        {
            return GetTodoList().Where(o => o.to_send_user_id == user_id).ToList();
        }

        /// <summary>
        /// 获取用户已经接收的消息
        /// </summary>
        /// <param name="user_id"></param>
        public List<T_TodoList> SendTodo(int user_id)
        {
            List<T_TodoList> list = new List<T_TodoList>();
            var data = GetTodoList();
            foreach (var item in data)
            {
                if (item.to_user_id == user_id.ToString())
                {
                    list.Add(item);
                    continue;
                }
                if (!string.IsNullOrEmpty(item.to_user_id))
                {
                    string[] copyUser = item.to_user_id.Split(';');
                    for (int i = 0; i < copyUser.Length; i++)
                    {
                        if (copyUser[i] == user_id.ToString())
                        {
                            list.Add(item);
                            continue;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 回复待办事宜 只有收件人是一个人的情况下才允许回复
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ReTodoContent(object obj)
        {
            FormCollection param = (FormCollection)obj;
            T_TodoList todo = new T_TodoList();
            todo.re_content = param["re_content"].Trim();
            todo.re_date = todo.to_send_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int todo_id = int.Parse(param["to_id"]);
            return base.Modify(todo, o => o.to_id == todo_id, "re_content", "re_date") > 0;
        }

        #region APP接口

        /// <summary>
        /// 根据guid获取用户信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public T_User GetByUserGuid(string guid)
        {
            return new T_UserBLL().Select(o => o.user_guid == guid).FirstOrDefault();
        }

        /// <summary>
        /// 分页获取待办事宜
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public List<HCQ2_Model.AppModel.TodoReturn> GetTodoPageRowList(HCQ2_Model.AppModel.ToDoRecred todo)
        {
            List<HCQ2_Model.AppModel.TodoReturn> list = new List<HCQ2_Model.AppModel.TodoReturn>();
            HCQ2_Model.AppModel.TodoReturn rTodo = new HCQ2_Model.AppModel.TodoReturn();
            T_User user = GetByUserGuid(todo.userid);

            ///获取该用户接受的全部消息
            var data = GetTodoList().Where(o => o.to_user_id == user.user_id.ToString());
            if (todo.todo_type == "0")
            {
                data = data.Where(o => o.re_content == null);
            }
            else
            {
                data = data.Where(o => o.re_content != null);
            }

            if (data.Count() > 0)
            {
                int rows = todo.rows;
                int page = todo.page;
                List<T_TodoList> todoList = data.Skip((rows * page) - rows).Take(rows).ToList();
                if (todoList.Count > 0)
                {
                    foreach (var item in todoList)
                    {
                        rTodo = new HCQ2_Model.AppModel.TodoReturn();
                        rTodo.todo_id = item.to_id;
                        rTodo.todo_title = item.to_title;
                        rTodo.todo_content = item.to_content;
                        rTodo.send_msg_date = Convert.ToDateTime(item.to_send_date).ToString("yyyy-MM-dd");
                        rTodo.send_msg_user = item.to_send_user_name;
                        list.Add(rTodo);
                    }
                }
            }
            return list;
        }

        #endregion

    }
}
