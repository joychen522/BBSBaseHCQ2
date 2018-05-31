using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户管理业务层接口
    /// </summary>
    public partial interface IT_UserBLL
    {
        /// <summary>
        ///  保存用户对象
        /// </summary>
        /// <param name="user">用户对象</param>
        bool AddUser(UserModel user);
        /// <summary>
        ///  编辑用户对象
        /// </summary>
        /// <param name="user">对象</param>
        /// <param name="id">主键值</param
        /// <param name="selUnit">选择单位</param>
        /// <returns></returns>
        bool EditUser(UserModel user,int id,string selUnit);
        /// <summary>
        ///  查询用户数据
        /// </summary>
        /// <param name="unitid">组织机构代码</param>
        /// <param name="keyword">关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页几条</param>
        /// <returns></returns>
        List<UserModel> GetUserData(int folder_id,string keyword,int page,int rows);
        /// <summary>
        ///  根据条件获取数据量
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="keyword">区域主键</param>
        /// <returns></returns>
        int GetCountByData(string keyword,int folder_id);
        /// <summary>
        ///  判断登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <param name="result">返回登录消息</param>
        /// <returns></returns>
        LoginResultModel GetByUser(string loginName, string loginPwd);
        /// <summary>
        ///  根据用户Id获取登录信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        T_Login GetLoginMessageById(int id);
        /// <summary>
        ///  根据用户ID获取单位树
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.TreeModel.AreaTreeModel> GetAreaData(int user_id);
    }
}
