using System;
using System.Collections.Generic;
using System.Linq;
using HCQ2_Common.Encrypt;
using HCQ2_IBLL;
using HCQ2_Model;
using HCQ2_Model.EnumModel;
using HCQ2_Model.TreeModel;
using HCQ2_Model.ViewModel;
using HCQ2_Model.ViewModel.SysAdmin;
using Spring.Transaction.Interceptor;

namespace HCQ2_BLL
{
    /// <summary>
    /// 用户对象数据层实现
    /// </summary>
    public partial class T_UserBLL:IT_UserBLL
    {
        /// <summary>
        ///  保存用户对象
        /// </summary>
        /// <param name="user">用户对象</param>
        [Transaction]
        public bool AddUser(UserModel user)
        {
            if (user == null)
                return false;
            try
            {
                //1：保存用户数据
                T_User t_user = new T_User()
                {
                    user_name = user.user_name,
                    login_name = user.login_name,
                    user_pwd = EncryptHelper.Md5Encryption(user.user_pwd),
                    user_sex = user.user_sex,
                    user_qq = user.user_qq,
                    user_email = user.user_email,
                    user_phone = user.user_phone,
                    user_address = user.user_address,
                    user_guid= EncryptHelper.CreateGuidValue(),
                    user_birth = (!string.IsNullOrEmpty(user.user_birth))?DateTime.ParseExact(user.user_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")):(DateTime?) null,
                    user_note = user.user_note
                };
                //添加用户
                DBSession.IT_UserDAL.Add(t_user);
                //2：判断用户是否有组织机构代码，有则保存进关联关系表
                if (user.orgUnit>0)
                    DBSession.IT_Org_UserDAL.Add(new T_Org_User() {user_id = t_user.user_id, UnitID = user.orgUnit.ToString()});
                //3：保存用户--角色设置
                if (!string.IsNullOrEmpty(user.user_role))
                {
                    DBSession.IT_UserRoleRelationDAL.Delete(s => s.user_id == t_user.user_id);
                    string[] roles = user.user_role.Split(',');
                    foreach (string item in roles)
                    {
                        DBSession.IT_UserRoleRelationDAL.Add(new T_UserRoleRelation()
                        {
                            user_id = t_user.user_id,
                            role_id = HCQ2_Common.Helper.ToInt(item)
                        });
                    }
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        ///  编辑用户对象
        /// </summary>
        /// <param name="user">用户模型</param>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public bool EditUser(UserModel user,int id,string selUnit)
        {
            if (user == null)
                return false;
            //1.添加前判断修改的用户名是否被占用
            List<T_User> Uer = Select(s => s.login_name == user.login_name && s.user_id != id);
            if (null != Uer && Uer.Count > 0)
                throw new Exception("当前登录名已被占用，请重新设置~");
            T_User t_user = new T_User()
            {
                user_id = id,
                user_name = user.user_name,
                login_name = user.login_name,
                //user_pwd = HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(user.user_pwd),
                user_sex = user.user_sex,
                user_qq = user.user_qq,
                user_email = user.user_email,
                user_phone = user.user_phone,
                user_address = user.user_address,
                user_birth = (!string.IsNullOrEmpty(user.user_birth) ? DateTime.ParseExact(user.user_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null),
                user_note = user.user_note,
                user_type = user.user_type,
                user_identify = user.user_identify
            };
            base.DBSession.IT_UserDAL.EditUser(t_user,selUnit);
            //保存用户--角色设置
            if (!string.IsNullOrEmpty(user.user_role))
            {
                DBSession.IT_UserRoleRelationDAL.Delete(s => s.user_id == id);
                string[] roles = user.user_role.Split(',');
                foreach (string item in roles)
                {
                    DBSession.IT_UserRoleRelationDAL.Add(new T_UserRoleRelation()
                    {
                        user_id=id,
                        role_id=HCQ2_Common.Helper.ToInt(item)
                    });
                }
            }
            return true;
        }
        /// <summary>
        ///  查询用户对象
        /// </summary>
        /// <param name="unitid">组织机构代码</param>
        /// <param name="keyword">关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页几条</param>
        /// <returns></returns>
        public List<UserModel> GetUserData(int folder_id, string keyword,int page,int rows)
        {
            if(folder_id<=0)
                return DBSession.IT_UserDAL.GetUserLimt(keyword, page, rows);
            List<T_User> users = DBSession.IT_UserDAL.GetUserByUnitID(folder_id, keyword, page, rows);
            List<UserModel> mList = new List<UserModel>();
            if (null == users)
                return mList;
            foreach (T_User item in users)
                mList.Add(item.ToPOCO());
            return mList;
        }

        /// <summary>
        ///  根据条件获取数量
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int GetCountByData(string keyword, int folder_id)
        {
            return DBSession.IT_UserDAL.GetCountByData(keyword, folder_id);
        }

        /// <summary>
        ///  判断登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public LoginResultModel GetByUser(string loginName, string loginPwd)
        {
            LoginResultModel rModel = new LoginResultModel()
            {
                Status = true,
                Msg = LoginEnum.LoginResult.登陆成功
            };
            //1 判断用户名是否存在
            T_User u = base.DBSession.IT_UserDAL.Select(s => s.login_name == loginName).FirstOrDefault();
            if (u == null) {
                rModel.Status = false;
                rModel.Msg = LoginEnum.LoginResult.用户名不存在;
                return rModel;
            }
            //2 判断是否为受限用户
            HCQ2_Model.T_LimitUser limtUser =
                base.DBSession.IT_LimitUserDAL.Select(s => s.user_id == u.user_id).FirstOrDefault();
            if (null != limtUser && limtUser.to_time> DateTime.Now){
                rModel.Status = false;
                rModel.Message = limtUser.limit_note;
                return rModel;//受限制用户
            }
            rModel.user = u;
            //3 判断密码是否正确
            if (u.user_pwd != EncryptHelper.Md5Encryption(loginPwd))
            {
                rModel.Status = false;
                rModel.Msg = LoginEnum.LoginResult.密码错误;
                return rModel;
            }
            return rModel;
        }
        /// <summary>
        ///  获取登录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_Login GetLoginMessageById(int id)
        {
            if(id<=0)
                return DBSession.IT_LoginDAL.Select(s => s.user_id == id).FirstOrDefault();
            return null;
        }
        /// <summary>
        ///  根据用户ID获取单位树
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<AreaTreeModel> GetAreaData(int user_id)
        {
            return HCQ2UI_Helper.OperateContext.Current.bllSession.T_OrgFolder.GetAreaData(user_id);
        }
    }
}
