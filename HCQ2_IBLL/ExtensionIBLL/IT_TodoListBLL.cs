using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ViewModel;

namespace HCQ2_IBLL
{
    public partial interface IT_TodoListBLL
    {
        /// <summary>
        /// 获取所有的信箱信息
        /// </summary>
        /// <returns></returns>
        List<T_TodoList> GetTodoList();

        /// <summary>
        /// 根据ID获取详细
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        T_TodoList GetByToId(int to_id);

        /// <summary>
        /// 获取该用户的信件
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<T_TodoList> GetByUserId(int user_id);

        /// <summary>
        /// 获取该用户发送的信件
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<T_TodoList> GetByUserSend(int user_id);

        /// <summary>
        /// 写信
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool NewTodo(object obj);

        /// <summary>
        /// 根据ID删除信件
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        bool DeleteTodo(int to_id);

        /// <summary>
        /// 获取当前人员的所有信件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TableModel GetUserAllTodoList(object obj);

        /// <summary>
        /// 获取用户目录树
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, object>> GetUserTree();

        /// <summary>
        /// 获取用户已经发送的消息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<T_TodoList> ReceiveTodo(int user_id);

        /// <summary>
        /// 获取用户已经接收的消息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<T_TodoList> SendTodo(int user_id);

        /// <summary>
        /// 回复待办事宜 只有收件人是一个人的情况下才允许回复
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool ReTodoContent(object obj);

        #region APP接口

        /// <summary>
        /// 根据guid获取用户信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        T_User GetByUserGuid(string guid);

        /// <summary>
        /// 分页获取待办事宜
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        List<HCQ2_Model.AppModel.TodoReturn> GetTodoPageRowList(HCQ2_Model.AppModel.ToDoRecred todo);

        #endregion

    }
}
