using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HCQ2_Model.APPModel.ParamModel;

namespace HCQ2_Model.AppModel
{

    /// <summary>
    /// 项目传入参数
    /// </summary>
    public class Compay: BaseAPI
    {
        /// <summary>
        /// 企业、项目名称
        /// </summary>
        public string unit_name { get; set; }
    }

    /// <summary>
    /// 企业项目人员详细
    /// </summary>
    public class CompayPersonDetail : Compay {
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string person_name { get; set; }
    }

    /// <summary>
    /// 分页获取项目列表
    /// </summary>
    public class CompayList:BaseAPI
    {
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; } = 20;
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
    }

    /// <summary>
    /// 企业项目返回值列表
    /// </summary>
    public class UnitReturn
    {
        /// <summary>
        /// 企业项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 企业项目代码
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string contract_money { get; set; }
        /// <summary>
        /// 工人数量
        /// </summary>
        public int worker_num { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string unit_contact { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string unit_phone { get; set; }
        /// <summary>
        /// 企业还是项目 N：企业、M：项目
        /// </summary>
        public string unit_type { get; set; }
        /// <summary>
        /// 工程类别
        /// </summary>
        public string project_type { get; set; }
        /// <summary>
        /// 保障金额
        /// </summary>
        public decimal? unit_security { get; set; }
    }

    /// <summary>
    /// 项目信息返回值
    /// </summary>
    public class ProjectDetail
    {
        /// <summary>
        /// 项目信息
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 开工日期
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string contract_money { get; set; }
        /// <summary>
        /// 工人数量
        /// </summary>
        public int worker_num { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string unit_contact { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        /// 保障金
        /// </summary>
        public string unit_security { get; set; }
        /// <summary>
        /// 工程类别
        /// </summary>
        public string project_type { get; set; }
    }
    
    /// <summary>
    /// 企业详细信息返回值
    /// </summary>
    public class CompayDetail
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string compay_name { get; set; }
        /// <summary>
        /// 企业简称
        /// </summary>
        public string compay_as { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public string compay_type { get; set; }
        /// <summary>
        /// 营业执照编码
        /// </summary>
        public string business_code { get; set; }
        /// <summary>
        /// 住所
        /// </summary>
        public string compay_address { get; set; }
        /// <summary>
        /// 法定代表人
        /// </summary>
        public string compay_representative { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public string reg_capital { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public string compay_start_date { get; set; }
        /// <summary>
        /// 营业期限
        /// </summary>
        public string up_end_date { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string compay_scope { get; set; }

        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public string compay_code { get; set; }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string XXFZR { get; set; }
        /// <summary>
        /// 项目负责人电话
        /// </summary>
        public string XXFZRDH { get; set; }
        /// <summary>
        /// 劳资专管员一
        /// </summary>
        public string LZZGYYI { get; set; }
        /// <summary>
        /// 劳资专管员一电话
        /// </summary>
        public string LZZGYYIDH { get; set; }
        /// <summary>
        /// 劳资专管员二
        /// </summary>
        public string LZZGYER { get; set; }
        /// <summary>
        /// 劳资专管员二电话
        /// </summary>
        public string LZZGYERDH { get; set; }
        /// <summary>
        /// 劳资专管员三
        /// </summary>
        public string LZZGYSAN { get; set; }
        /// <summary>
        /// 劳资专管员三电话
        /// </summary>
        public string LZZGYSANDH { get; set; }
    }

    public class UnitPerson
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string person_phone { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string person_jobs { get; set; }
    }
}
