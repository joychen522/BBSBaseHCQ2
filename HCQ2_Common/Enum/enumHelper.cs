using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common
{
    public class enumHelper
    {
        /// <summary>
        ///  获取枚举数字
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="str">对应的字符串</param>
        /// <returns></returns>
        public static int GetEnumIntByStr<T>(string str)
        {
            return (int) (Enum.Parse(typeof (T), str, true));
        }
    }
}
