using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common
{
    public static class ReturnJson
    {
        /// <summary>
        /// 返回固定格式的json字符串，并且ref最终显示出来的泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <param name="row"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public static string GetReturnJson<T>(List<T> list, int page, int row) where T : class
        {
            var data = list.Skip((page * row) - row).Take(row);
            return "{\"total\":" + list.Count() + ",\"rows\":" + JsonHelper.ObjectToJson(data) + "}";
        }
    }
}
