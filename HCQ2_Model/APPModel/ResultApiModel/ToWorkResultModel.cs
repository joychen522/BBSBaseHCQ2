using HCQ2_Model.APPModel.ParamModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class ToWorkResultModel
    {
        /// <summary>
        ///  出工+未出工总人数
        /// </summary>
        public int allCount { get; set; }
        /// <summary>
        ///  出工人数
        /// </summary>
        public int toWork { get; set; }
        /// <summary>
        ///  未出工人数
        /// </summary>
        public int onWork { get; set; }
        /// <summary>
        ///  出工人数集合
        /// </summary>
        public List<string> toWorkList { get; set; }
        //public List<WorkParamModel> toWorkList { get; set; }
        /// <summary>
        ///  未出工人数集合
        /// </summary>
        public List<string> onWorkList { get; set; }
        //public List<WorkParamModel> onWorkList { get; set; }
    }
}
