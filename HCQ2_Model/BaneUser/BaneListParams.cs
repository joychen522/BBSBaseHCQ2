using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneListParams
    {
        /// <summary>
        ///  用户id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        ///  人员姓名
        /// </summary>
        public string baneName { get; set; }
        /// <summary>
        ///  人员类别
        /// </summary>
        public string baneType { get; set; }
        /// <summary>
        ///  结束原因
        /// </summary>
        public string baneEnd { get; set; }
        /// <summary>
        ///  组织机构ID
        /// </summary>
        public int orgId { get; set; }
        /// <summary>
        ///  路径
        /// </summary>
        public string folder_path { get; set; }
        /// <summary>
        ///  是否是父节点
        /// </summary>
        public bool isParent { get; set; }
        /// <summary>
        ///  全部 0，尿检任务 :1
        /// </summary>
        public string baneTask { get; set; }
        /// <summary>
        ///  尿检时间查询
        /// </summary>
        public string banedays { get; set; }
        /// <summary>
        ///  页面查询类别 
        /// </summary>
        public string queryType { get; set; }
        public int page { get; set; }
        public int rows { get; set; }

        /// <summary>
        ///  构造
        /// </summary>
        /// <param name="baneName">人员姓名</param>
        /// <param name="baneType">人员类别</param>
        /// <param name="baneEnd"> 结束原因</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        public BaneListParams(int user_id,string baneName,string baneType,string baneEnd,string folder_path,bool isParent,int orgId, int page,int rows,string banedays, string queryType, string baneTask="0")
        {
            this.user_id = user_id;
            this.baneName = baneName;
            this.baneType = baneType;
            this.baneEnd = baneEnd;
            this.folder_path = folder_path;
            this.isParent = isParent;
            this.orgId = orgId;
            this.page = page;
            this.rows = rows;
            this.banedays = banedays;
            this.queryType = queryType;
            this.baneTask = baneTask;
        }
    }
}
