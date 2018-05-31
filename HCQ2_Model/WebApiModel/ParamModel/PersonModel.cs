using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{

    #region 虹膜接口

    /// <summary>
    ///  人员基本信息模型
    /// </summary>
    public class PersonModel : BaseModel
    {
        /// <summary>
        ///  姓名
        /// </summary>
        [Required]
        public string person_name { get; set; }

        /// <summary>
        ///  性别
        /// </summary>
        public string person_sex { get; set; }

        /// <summary>
        ///  民族
        /// </summary>
        public string person_nation { get; set; }

        /// <summary>
        ///  出生日期（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_birthday { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string person_address { get; set; }

        /// <summary>
        ///  身份证号码
        /// </summary>
        public string person_cardno { get; set; }

        /// <summary>
        ///  签发机关
        /// </summary>
        public string person_police { get; set; }

        /// <summary>
        ///  有效日期起（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_userlifebegin { get; set; }

        /// <summary>
        ///  有效日期止（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_userlifeend { get; set; }

        /// <summary>
        ///  照片（base64）
        /// </summary>
        public string person_photo { get; set; }

        /// <summary>
        ///  虹膜信息（base64）
        /// </summary>
        [Required]
        public string iris_data { get; set; }
        /// <summary>
        /// 新增虹膜信息
        /// </summary>
        public string big_iris_data { get; set; }

    }

    /// <summary>
    /// 数据下发
    /// </summary>
    public class PersonIris
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string orgid { get; set; }

        /// <summary>
        /// 人员ID
        /// </summary>
        public string personid { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string person_name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_cardno { get; set; }

        /// <summary>
        /// 虹膜信息
        /// </summary>
        public string iris_data { get; set; }

        /// <summary>
        /// 新增虹膜信息
        /// </summary>
        public string big_iris_data { get; set; }
    }

    /// <summary>
    /// 人员采集返回数据
    /// </summary>
    public class returnPerson
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string personid { get; set; }
    }

    /// <summary>
    /// 人员同步接口参数
    /// </summary>
    public class PersonSysn : BaseModel
    {
        /// <summary>
        /// 同步时间戳
        /// </summary>
        [Required]
        public string match_timestamp { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        [Required]
        public string deviceid { get; set; }
    }

    /// <summary>
    /// 人员同步数据下发
    /// </summary>
    public class PersonCL
    {
        public string orgid { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string personid { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string person_name { get; set; }

        /// <summary>
        ///  性别
        /// </summary>
        public string person_sex { get; set; }

        /// <summary>
        ///  民族
        /// </summary>
        public string person_nation { get; set; }

        /// <summary>
        ///  出生日期（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_birthday { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string person_address { get; set; }

        /// <summary>
        ///  身份证号码
        /// </summary>
        public string person_cardno { get; set; }

        /// <summary>
        ///  签发机关
        /// </summary>
        public string person_police { get; set; }

        /// <summary>
        ///  有效日期起（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_userlifebegin { get; set; }

        /// <summary>
        ///  有效日期止（字符格式：yyyy-MM-dd）
        /// </summary>
        public string person_userlifeend { get; set; }

        /// <summary>
        ///  照片（base64）
        /// </summary>
        public string person_photo { get; set; }

        /// <summary>
        ///  虹膜信息（base64）
        /// </summary>
        public string iris_data { get; set; }
        /// <summary>
        /// 新增虹膜信息
        /// </summary>
        public string big_iris_data { get; set; }


    }

    #endregion

    #region app接口

    /// <summary>
    /// APP人员查询接口
    /// </summary>
    public class PersonSelect
    {
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string identifyCode { get; set; }
    }

    /// <summary>
    /// APP人员查询返回数据
    /// </summary>
    public class PersonSelectReturn
    {

    }

    /// <summary>
    /// 重复用工返回数据
    /// </summary>
    public class PersonRepeatReturn
    {
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 征信状态
        /// </summary>
        public string personStatus { get; set; }

        /// <summary>
        /// 所在项目
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string unitWork { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string workStart { get; set; }

        /// <summary>
        /// 出工天数
        /// </summary>
        public string workDays { get; set; }

        /// <summary>
        /// 薪资金额
        /// </summary>
        public string wageMoney { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string contactPhone { get; set; }

    }

    /// <summary>
    /// 工人出工排名返回数据
    /// </summary>
    public class PersonWorkDay
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 出工天数
        /// </summary>
        public string workDays { get; set; }
    }

    #endregion
}
