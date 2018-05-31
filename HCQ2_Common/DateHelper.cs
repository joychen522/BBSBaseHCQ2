using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common
{
    public class DateHelper
    {
        /// <summary>
        /// 获取两个日期之间所有的日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetDateDetail(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                return null;
            List<DateTime> list = new List<DateTime>();
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                list.Add(dt);
            }
            return list;
        }

        /// <summary>
        /// 转化CST日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetCSTDate(string date)
        {
            string strDate = "";
            //目标字符串：Mon Apr 27 09:08:20 CST 2015
            DateTime dt = Convert.ToDateTime(date);
            strDate = GetWeek(dt.DayOfWeek.ToString()) + " ";
            strDate += GetMonths(dt.Month.ToString()) + " ";
            strDate += dt.ToString("dd HH:mm:ss") + " CST ";
            strDate += dt.ToString("yyyy");
            return strDate;
        }

        /// <summary>
        /// 获取周数
        /// </summary>
        /// <returns></returns>
        private static string GetWeek(string num)
        {
            string strWeek = "";
            switch (num)
            {
                case "Monday":
                    strWeek = "Mon";
                    break;
                case "Tuesday":
                    strWeek = "Tue";
                    break;
                case "Wednesday":
                    strWeek = "Wed";
                    break;
                case "Thursday":
                    strWeek = "Thu";
                    break;
                case "Friday":
                    strWeek = "Fri";
                    break;
                case "Saturday":
                    strWeek = "Sat";
                    break;
                case "Sunday":
                    strWeek = "Sun";
                    break;
                default:
                    break;
            }
            return strWeek;
        }

        /// <summary>
        /// 获取月数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string GetMonths(string num)
        {
            string strMonth = "";

            switch (num)
            {
                case "1":
                    strMonth = "Jan";
                    break;
                case "2":
                    strMonth = "Feb";
                    break;
                case "3":
                    strMonth = "Mar";
                    break;
                case "4":
                    strMonth = "Apr";
                    break;
                case "5":
                    strMonth = "May";
                    break;
                case "6":
                    strMonth = "Jun";
                    break;
                case "7":
                    strMonth = "Jul";
                    break;
                case "8":
                    strMonth = "Aug";
                    break;
                case "9":
                    strMonth = "Sep";
                    break;
                case "10":
                    strMonth = "Oct";
                    break;
                case "11":
                    strMonth = "Nov";
                    break;
                case "12":
                    strMonth = "Dec";
                    break;
                default:
                    break;
            }

            return strMonth;
        }
    }
}
