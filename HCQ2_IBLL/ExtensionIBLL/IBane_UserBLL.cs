using HCQ2_Model.BaneUser;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using HCQ2_Model.WebApiModel.ParamModel;
using System.Collections.Generic;
using System.Data;
using static HCQ2_Model.BaneUser.BaneLogParam;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  戒毒人员业务层接口
    /// </summary>
    public partial interface IBane_UserBLL
    {
        /// <summary>
        ///  获取戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BaneListModel> GetBaneData(BaneListParams param);
        /// <summary>
        ///  统计戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int GetGetBaneDataCount(BaneListParams param);
        /// <summary>
        ///  获取定期尿检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BaneProModel> GetBaneProData(BaneListParams param);
        /// <summary>
        ///  统计定期尿检人员数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int GetBaneProDataCount(BaneListParams param);
        /// <summary>
        ///  添加戒毒人员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddUser(BaneAddModel user, BaneRecoveryModel model);
        /// <summary>
        ///  编辑戒毒人员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditUser(BaneAddModel user, BaneRecoveryModel model);
        /// <summary>
        ///  根据身份证获取数据
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneAddUser GetBaneUser(string user_identify);
        /// <summary>
        ///  本月应检人数统计 根据传递的用户统计
        /// </summary>
        /// <returns></returns>
        int GetCountByMonth(int user_id);
        /// <summary>
        ///  过检人数统计，根据权限
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int PassCountPerson(int user_id);
        /// <summary>
        ///  统计一周内应到检测人员数量
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetWeekCountPerson(int user_id);
        /// <summary>
        ///  统计总人数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int AllPersonCount(int user_id);
        /// <summary>
        ///  统计社区数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetBBSCount(int user_id);

        //**************************************接口*******************************************
        /// <summary>
        /// 人员同步
        /// </summary>
        /// <param name="match_timestamp">时间戳</param>
        /// <param name="userid">用户guid</param>
        /// <param name="deviceid">设备编码</param>
        /// <returns></returns>
        List<PersonCL> PersonsSynchronous(PersonSysn person);
        /// <summary>
        ///  获取下发所有数据 根据权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<PersonCL> GetPersonsSentDownData(string userid);

        //**************************************APP接口*******************************************
        /// <summary>
        ///  根据身份证获取首页答题信息
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneHomeAnswerModel GetAnswerDAL(string user_identify);
        /// <summary>
        ///  禁毒人员注册
        /// </summary>
        /// <param name="bane"></param>
        /// <returns></returns>
        BaneRegisterType BaneRegister(BaneRegModel bane);
        /// <summary>
        ///  获取APP 首页个人状态、本次、下次、管控 (日期)
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneDetectionModel GetBaneDetectionByID(string user_identify);
        /// <summary>
        ///  根据guid获取 定期检测提醒记录数量
        /// </summary>
        /// <param name="user_guid"></param>
        /// <returns></returns>
        int GetDetectionNumByID(string user_guid);
        /// <summary>
        ///  根据用户身份证获取是否需要提醒 定期检测
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneMyMessageModel GetDetectionContentByID(string user_identify);
    }
}
