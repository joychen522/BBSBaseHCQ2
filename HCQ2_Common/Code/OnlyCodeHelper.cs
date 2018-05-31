using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Code
{
    /// <summary>
    ///  唯一编码
    /// </summary>
    public class OnlyCodeHelper
    {
        /// <summary>
        ///  根据GUID获取16位的唯一字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateOnlyCode() { 
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int) b + 1);
            return string.Format("{0:x}",i-DateTime.Now.Ticks);
        }

        /// <summary>
        ///  创建13位 唯一编码
        /// </summary>
        /// <returns></returns>
        public static string CreateIntCode()
        {
            Random rd = new Random(2);
            return DateTime.Now.ToString("yyyyMMddHHmm") + rd.Next();
        }
    }
}
