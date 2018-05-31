using HCQ2_Model.BaneUser;
using HCQ2_Model.WebApiModel.ParamModel;
using System.Collections.Generic;
using HCQ2_Model.BaneUser.APP.Result;

namespace HCQ2_IDAL
{
    public partial interface IBane_UserDAL
    {
        /// <summary>
        ///  获取戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BaneListModel> GetBaneData(BaneListParams param, List<int> perList,bool isManager);
        /// <summary>
        ///  统计戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int GetGetBaneDataCount(BaneListParams param, List<int> perList, bool isManager);
        /// <summary>
        ///  获取定期尿检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BaneProModel> GetBaneProData(BaneListParams param, List<int> perList, bool isManager);
        /// <summary>
        ///  统计定期尿检人员数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int GetBaneProDataCount(BaneListParams param, List<int> perList, bool isManager);
        /// <summary>
        ///  根据身份证获取数据
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneAddUser GetBaneUser(string user_identify);
        /// <summary>
        ///  本月应检人数统计
        /// </summary>
        /// <returns></returns>
        int GetCountByMonth(int user_id, List<int> perList, bool isManager);
        /// <summary>
        ///  过检人数统计，根据权限
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int PassCountPerson(int user_id, List<int> perList, bool isManager);
        /// <summary>
        ///  统计一周内应到检测人员数量
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetWeekCountPerson(int user_id, List<int> perList, bool isManager);
        /// <summary>
        ///  统计总人数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int AllPersonCount(int user_id, List<int> perList, bool isManager);
        /// <summary>
        ///  统计社区数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetBBSCount(int user_id, List<int> perList, bool isManager);

        //**************************************接口*******************************************
        /// <summary>
        /// 获取人员同步数据
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="deviceid">设备编码</param>
        /// <returns></returns>
        List<PersonCL> GetPersonsSynchronousData(int userid, string deviceid);
        /// <summary>
        ///  获取下发所有数据 根据权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<PersonCL> GetPersonsSentDownData(string userid);

        //**************************************APP接口*******************************************
        /// <summary>
        ///  注册
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="pwd"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        int RegBaneUser(int user_id,string pwd,string phone);
        /// <summary>
        ///  根据身份证获取首页答题信息
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneHomeAnswerModel GetAnswerDAL(string user_identify);
        /// <summary>
        ///  获取APP 首页个人状态、本次、下次、管控 (日期)
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneDetectionModel GetBaneDetection(string user_identify);
    }
}
