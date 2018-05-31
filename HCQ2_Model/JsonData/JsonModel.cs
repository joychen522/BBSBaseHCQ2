using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.JsonData
{
    /// <summary>
    ///  Json模型
    /// </summary>
    public class JsonModel
    {
        /// <summary>
        ///  url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///  消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        ///  状态
        ///  0：成功
        ///  1：失败
        /// </summary>
        public int Statu { get; set; }
        /// <summary>
        ///  数据
        /// </summary>
        public object Data { get; set; }
    }
}
