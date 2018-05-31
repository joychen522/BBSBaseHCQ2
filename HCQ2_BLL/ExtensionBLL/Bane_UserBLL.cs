using HCQ2_Model;
using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Drawing;
using HCQ2_Model.WebApiModel.ParamModel;
using System.Data;
using HCQ2_Model.BaneUser.APP.Result;
using HCQ2_Model.BaneUser.APP.Params;
using static HCQ2_Model.BaneUser.BaneLogParam;

namespace HCQ2_BLL
{
    public partial class Bane_UserBLL:HCQ2_IBLL.IBane_UserBLL
    {
        /// <summary>
        ///  获取戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaneListModel> GetBaneData(BaneListParams param)
        {
            if (null == param)
                return null;
            return DBSession.IBane_UserDAL.GetBaneData(param, GetPermissions(param.user_id),HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  统计戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetGetBaneDataCount(BaneListParams param)
        {
            if (null == param)
                return 0;
            return DBSession.IBane_UserDAL.GetGetBaneDataCount(param, GetPermissions(param.user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  获取定期尿检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaneProModel> GetBaneProData(BaneListParams param)
        {
            if (null == param)
                return null;
            return DBSession.IBane_UserDAL.GetBaneProData(param, GetPermissions(param.user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  统计定期尿检人员数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetBaneProDataCount(BaneListParams param)
        {
            if (null == param)
                return 0;
            return DBSession.IBane_UserDAL.GetBaneProDataCount(param, GetPermissions(param.user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  添加戒毒人员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(BaneAddModel user, BaneRecoveryModel model)
        {
            //报到时间
            DateTime next_date = DateTime.ParseExact(model.start_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"));
            int addMonth = 1;//默认一个月周期
            //检查时间
            List<Bane_UrinalysisTimeSet> set= DBSession.IBane_UrinalysisTimeSetDAL.Select(s => s.user_type.Equals(user.user_type), o => o.gap_month,true);
            if (set != null)
                addMonth = set[0].gap_month;
            DateTime user_next_date= next_date.AddMonths(addMonth).AddDays(6);
            DateTime? control_date;
            if (!string.IsNullOrEmpty(user.control_date))
                control_date = DateTime.ParseExact(user.control_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"));
            else
                control_date = null;
            Bane_User baneUser = new Bane_User
            {
                user_name = user.user_name,
                alias_name = user.alias_name,
                user_sex = user.user_sex,
                user_birth = DateTime.ParseExact(user.user_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),
                user_height = user.user_height,
                user_identify = user.user_identify,
                user_edu = user.user_edu,
                job_status = user.job_status,
                bane_type = user.bane_type,
                birth_url = user.birth_url,
                family_phone = user.family_phone,
                live_url = user.live_url,
                move_phone = user.move_phone,
                attn_name = user.attn_name,
                attn_url = user.attn_url,
                attn_relation = user.attn_relation,
                attn_phone = user.attn_phone,
                marital_status = user.marital_status,
                is_live_parent = user.is_live_parent,
                user_status = user.user_status,
                is_pro_train = user.is_pro_train,
                user_skill = user.user_skill,
                user_type = user.user_type,
                user_phone = user.user_phone,
                ur_next_date = user_next_date,
                user_photo = GetPhotoUrl(user.user_photo, user.user_identify, user.org_id.ToString()),
                user_note = user.user_note,
                org_id = user.org_id,
                is_send = false,//是否下发
                user_resume = user.user_resume,
                iris_data1 = ss1(user.iris_data2),
                iris_data2 = ss2(user.iris_data2),
                update_date = DateTime.Now,
                user_guid = HCQ2_Common.Helper.GetGuid,
                control_date= control_date,
                user_pwd = "",
                user_total = 0,
                is_control = false,
                last_score = 0,
                the_score = 0,
                the_num = 0
            };
            Bane_RecoveryInfo reInfo = new Bane_RecoveryInfo
            {
                user_identify= model.user_identify,
                exec_area = model.exec_area,
                exec_unit = model.exec_unit,
                order_unit = model.order_unit,
                is_aids = model.is_aids,
                isolation_url = model.isolation_url,
                isolation_out_date = (!string.IsNullOrEmpty(model.isolation_out_date)) ? DateTime.ParseExact(model.isolation_out_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                cure_ups = model.cure_ups,
                in_recovery = model.in_recovery,
                start_date = next_date,
                end_date = (!string.IsNullOrEmpty(model.end_date)) ? DateTime.ParseExact(model.end_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                end_reason = model.end_reason
            };
            int mark = DBSession.IBane_UserDAL.Add(baneUser);
            DBSession.IBane_RecoveryInfoDAL.Add(reInfo);
            return mark > 0 ? true : false;
        }
        /// <summary>
        ///  根据用户ID获取所有权限集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private List<int> GetPermissions(int user_id)
        {
            List<T_Permissions> perList = DBSession.IT_PermissionsDAL.GetPermissById(user_id);
            List<int> perID = new List<int>();
            if (perList != null && perList.Count > 0)
                perID = perList.Select(s => s.per_id).ToList();
            return perID;
        }

        /// <summary>
        ///  64位图片格式存储并返回存储地址
        /// </summary>
        /// <returns></returns>
        public string GetPhotoUrl(string photo64,string user_identify,string orgId)
        {
            if (string.IsNullOrEmpty(photo64))
                return "";
            //判断文件夹是否存在
            string folderUrl = HttpContext.Current.Server.MapPath(string.Format("~/UpFile/BaneImg/{0}", orgId));
            if (!Directory.Exists(folderUrl))
                Directory.CreateDirectory(folderUrl);//文件夹不存在则创建
            string path = string.Format("~/UpFile/BaneImg/{0}/{1}.jpg", orgId, user_identify),
            pathUrl = HttpContext.Current.Server.MapPath(path);
            Bane_User user = DBSession.IBane_UserDAL.Select(s => s.user_identify == user_identify).FirstOrDefault();
            if (user != null && !string.IsNullOrEmpty(user.user_photo))
                File.Delete(HttpContext.Current.Server.MapPath(user.user_photo));
            //将64位编码转换为字节数组
            byte[] img = Convert.FromBase64String(photo64);
            //用filestream创造一个文件
            MemoryStream ms = new MemoryStream(img);
            Bitmap bmp = new Bitmap(ms);
            bmp.Save(pathUrl,System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Close();
            return path;
        }

        //加密
        private string ss1(string iris_data) {
            if (string.IsNullOrEmpty(iris_data))
                return "";
            string iris = iris_data;
            byte[] small = String16ToByte(iris.Substring(0, iris.Length / 2));
            return Convert.ToBase64String(small);
        }

        private string ss2(string iris_data)
        {
            if (string.IsNullOrEmpty(iris_data))
                return "";
            string iris = iris_data;
            byte[] big = String16ToByte(iris.Substring(iris.Length / 2, iris.Length / 2));
            return Convert.ToBase64String(big);
        }

        //解密
        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制转化为byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte[] String16ToByte(string str)
        {
            str = str.ToUpper();
            char[] hexChars = str.ToCharArray();
            byte[] returnBytes = new byte[str.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = (byte)(charToBtye(hexChars[i * 2]) << 4 | charToBtye(hexChars[i * 2 + 1]));
            }
            return returnBytes;
        }

        /// <summary>
        /// 处理16进制字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte charToBtye(char str)
        {
            return (byte)"0123456789ABCDEF".IndexOf(str);
        }

        /// <summary>
        ///  编辑戒毒人员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditUser(BaneAddModel user, BaneRecoveryModel model)
        {
            List<Bane_UrinalysisRecord> list = DBSession.IBane_UrinalysisRecordDAL.Select(s => s.user_identify.Equals(user.user_identify));
            //报到时间
            DateTime next_date = DateTime.ParseExact(model.start_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"));
            //下次尿检时间
            string updateItem = "";
            Bane_User baneUser = new Bane_User();
            DateTime? control_date;
            if (!string.IsNullOrEmpty(user.control_date))
                control_date = DateTime.ParseExact(user.control_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"));
            else
                control_date = null;
            if (list==null || list.Count <= 0)
            {
                int addMonth = 1;//默认一个月周期
                //检查时间
                List<Bane_UrinalysisTimeSet> set = DBSession.IBane_UrinalysisTimeSetDAL.Select(s => s.user_type.Equals(user.user_type), o => o.gap_month, true);
                if (set != null)
                    addMonth = set[0].gap_month;
                 //下次尿检时间
                baneUser.ur_next_date = next_date.AddMonths(addMonth).AddDays(6);
                updateItem = ",ur_next_date";
            }
            baneUser.user_name = user.user_name;
            baneUser.alias_name = user.alias_name;
            baneUser.user_sex = user.user_sex;
            baneUser.user_birth = DateTime.ParseExact(user.user_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"));
            baneUser.user_height = user.user_height;
            baneUser.user_identify = user.user_identify;
            baneUser.user_edu = user.user_edu;
            baneUser.job_status = user.job_status;
            baneUser.bane_type = user.bane_type;
            baneUser.birth_url = user.birth_url;
            baneUser.family_phone = user.family_phone;
            baneUser.live_url = user.live_url;
            baneUser.move_phone = user.move_phone;
            baneUser.attn_name = user.attn_name;
            baneUser.attn_url = user.attn_url;
            baneUser.attn_relation = user.attn_relation;
            baneUser.attn_phone = user.attn_phone;
            baneUser.marital_status = user.marital_status;
            baneUser.is_live_parent = user.is_live_parent;
            baneUser.user_status = user.user_status;
            baneUser.is_pro_train = user.is_pro_train;
            baneUser.user_skill = user.user_skill;
            baneUser.user_type = user.user_type;
            baneUser.user_phone = user.user_phone;
            baneUser.user_photo = GetPhotoUrl(user.user_photo, user.user_identify, user.org_id.ToString());
            baneUser.user_note = user.user_note;
            baneUser.org_id = user.org_id;
            baneUser.is_send = false;//是否下发
            baneUser.user_resume = user.user_resume;
            baneUser.iris_data1 = ss1(user.iris_data2);
            baneUser.iris_data2 = ss2(user.iris_data2);
            baneUser.update_date = DateTime.Now;
            baneUser.control_date = control_date;
            Bane_RecoveryInfo reInfo = new Bane_RecoveryInfo
            {
                user_identify = model.user_identify,
                exec_area = model.exec_area,
                exec_unit = model.exec_unit,
                order_unit = model.order_unit,
                is_aids = model.is_aids,
                isolation_url = model.isolation_url,
                isolation_out_date = (!string.IsNullOrEmpty(model.isolation_out_date)) ? DateTime.ParseExact(model.isolation_out_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                cure_ups = model.cure_ups,
                in_recovery = model.in_recovery,
                start_date = next_date,
                end_date = (!string.IsNullOrEmpty(model.end_date)) ? DateTime.ParseExact(model.end_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                end_reason = model.end_reason
            };
            string[] param = { "user_name", "alias_name", "user_sex", "user_birth", "user_height", "user_identify", "user_edu", "job_status", "bane_type", "birth_url", "family_phone", "live_url", "move_phone", "attn_name", "attn_url", "attn_relation", "attn_phone", "marital_status", "is_live_parent", "user_status", " is_pro_train", "user_skill", "user_type", "user_phone", "user_note", "is_send", "user_resume", "iris_data1", "iris_data2", "update_date", "control_date" };
            List<string> _list = param.ToList();
            if (!string.IsNullOrEmpty(updateItem))
                _list.Add("ur_next_date");
            if (!string.IsNullOrEmpty(user.user_photo))
                _list.Add("user_photo");
            DBSession.IBane_UserDAL.Modify(baneUser, s => s.user_id == user.user_id, _list.ToArray());
            DBSession.IBane_RecoveryInfoDAL.Modify(reInfo,s=>s.ri_id==model.ri_id, "user_identify","exec_area","exec_unit","order_unit","is_aids","isolation_url","isolation_out_date","cure_ups","in_recovery","start_date","end_date","end_reason");
            return true;
        }
        /// <summary>
        ///  根据身份证获取数据
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneAddUser GetBaneUser(string user_identify)
        {
            if (string.IsNullOrEmpty(user_identify))
                return null;
            BaneAddUser user= DBSession.IBane_UserDAL.GetBaneUser(user_identify);
            if (user != null && !string.IsNullOrEmpty(user.iris_data2))
                user.iris_data2 = byteToHexStr(Convert.FromBase64String(user.iris_data1))+ byteToHexStr(Convert.FromBase64String(user.iris_data2));
            return user;
        }
        /// <summary>
        ///  本月应检人数统计
        /// </summary>
        /// <returns></returns>
        public int GetCountByMonth(int user_id)
        {
            return DBSession.IBane_UserDAL.GetCountByMonth(user_id, GetPermissions(user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  过检人数统计，根据权限
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int PassCountPerson(int user_id)
        {
            return DBSession.IBane_UserDAL.PassCountPerson(user_id, GetPermissions(user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  统计一周内应到检测人员数量
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetWeekCountPerson(int user_id)
        {
            return DBSession.IBane_UserDAL.GetWeekCountPerson(user_id, GetPermissions(user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  统计总人数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int AllPersonCount(int user_id)
        {
            return DBSession.IBane_UserDAL.AllPersonCount(user_id, GetPermissions(user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        /// <summary>
        ///  统计社区数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetBBSCount(int user_id)
        {
            return DBSession.IBane_UserDAL.GetBBSCount(user_id, GetPermissions(user_id), HCQ2UI_Helper.Session.SysPermissSession.isAreaManager);
        }
        //**************************************接口*******************************************
        /// <summary>
        /// 人员同步
        /// </summary>
        /// <param name="match_timestamp">时间戳</param>
        /// <param name="userid">用户guid</param>
        /// <param name="deviceid">设备编码</param>
        /// <returns></returns>
        public List<PersonCL> PersonsSynchronous(PersonSysn person)
        {
            List<PersonCL> userList = new List<PersonCL>();
            //1.查询登录用户
            T_User u = DBSession.IT_UserDAL.Select(s => s.user_guid.Equals(person.userid)).FirstOrDefault();
            //2.获取时间戳格式化日期
            string syData = person.match_timestamp.Substring(0, 4) + "-" + person.match_timestamp.Substring(4, 2) + "-" + person.match_timestamp.Substring(6, 2);
            syData += " " + person.match_timestamp.Substring(8, 2) + ":" + person.match_timestamp.Substring(10, 2) + ":" + person.match_timestamp.Substring(12, 2);
            //3.查询满足条件人员
            userList = DBSession.IBane_UserDAL.GetPersonsSynchronousData(u.user_id, person.deviceid);
            //先删除上一次同步的记录
            DBSession.IT_SynchronousDAL.Delete(s => s.deviceid == person.deviceid);
            //获取当前用户代管的区域
            List<T_UserUnitRelation> org = DBSession.IT_UserUnitRelationDAL.Select(s => s.user_id == u.user_id).ToList();
            foreach(T_UserUnitRelation unit in org)
            {
                DBSession.IT_SynchronousDAL.Add(new T_Synchronous
                {
                    sy_unit_id = unit.unit_id,
                    sy_user_id = u.user_id,
                    sy_date = Convert.ToDateTime(syData),
                    deviceid = person.deviceid
                });
            }
            return userList;
        }
        /// <summary>
        ///  获取下发所有数据 根据权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PersonCL> GetPersonsSentDownData(string userid)
        {
            return DBSession.IBane_UserDAL.GetPersonsSentDownData(userid);
        }

        //**************************************APP接口*******************************************
        /// <summary>
        ///  根据身份证获取首页答题信息
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneHomeAnswerModel GetAnswerDAL(string user_identify)
        {
            if (string.IsNullOrEmpty(user_identify))
                return null;
            return DBSession.IBane_UserDAL.GetAnswerDAL(user_identify);
        }

        /// <summary>
        ///  禁毒人员注册
        /// </summary>
        /// <param name="bane"></param>
        /// <returns></returns>
        public BaneRegisterType BaneRegister(BaneRegModel bane)
        {
            Bane_User user = Select(s => s.user_identify == bane.user_identify).FirstOrDefault();
            if (user == null)
                return BaneRegisterType.EXCEPTION;
            string user_pwd = user.user_pwd;
            if (!string.IsNullOrEmpty(user_pwd))
                return BaneRegisterType.FINASH;//返回2表示已注册
            int mark = DBSession.IBane_UserDAL.RegBaneUser(user.user_id, HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(bane.user_pwd), bane.user_phone);
            return mark > 0 ? BaneRegisterType.OK : BaneRegisterType.ERROR;
        }
        /// <summary>
        ///  获取APP 首页个人状态、本次、下次、管控 (日期)
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneDetectionModel GetBaneDetectionByID(string user_identify)
        {
            if (string.IsNullOrEmpty(user_identify))
                return null;
            return DBSession.IBane_UserDAL.GetBaneDetection(user_identify);
        }
        /// <summary>
        ///  根据用户身份证获取 定期检测提醒记录数量
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public int GetDetectionNumByID(string user_guid)
        {
            Bane_User user = Select(s => s.user_guid == user_guid).FirstOrDefault();
            //提前3天 提醒
            DateTime dtNext = user.ur_next_date.AddDays(-9);//减去6天，回到正常下次检测日期,-3表示提前3天
            if (DateTime.Now >= dtNext)
                return 1;
            return 0;
        }
        /// <summary>
        ///  根据用户身份证获取是否需要提醒 定期检测
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneMyMessageModel GetDetectionContentByID(string user_guid)
        {
            if (GetDetectionNumByID(user_guid) <= 0)
                return null;
            Bane_User user = Select(s => s.user_guid == user_guid).FirstOrDefault();
            return new BaneMyMessageModel
            {
                mess_title = "定期检测提醒",
                send_name = "系统管理员",
                send_date = DateTime.Now.ToString("yyyy-MM-dd"),
                mess_content = "您应于" + user.ur_next_date.AddDays(-6).ToString("D") + "前到相应社工站做定期检测，请准时参加。"
            };
        }
    } 
}
