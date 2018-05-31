using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace HCQ2_Common
{
    /// <summary>
    ///  操作说明：json操作帮助类
    ///  创建人：陈敏
    ///  创建时间：2015-5-18 16:00
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        ///  js序列化器
        /// </summary>
        private static JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        ///  把对象 转换为json数组格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string objectToJsonStr(object obj)
        {
            //把对象 转换为json数组格式字符串
            return jss.Serialize(obj);
        }

        /// <summary>
        ///  成功返回信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JObject ResultSuccessMess(object data)
        {
            var jsonObj = new JObject(
                new JProperty("state",0),
                new JProperty("mess",""),
                new JProperty("data", data)
                );
            return jsonObj;
        }

        /// <summary>
        ///  异常返回
        /// </summary>
        /// <param name="mess">异常信息</param>
        /// <returns></returns>
        public static JObject ErrorMess(string mess = "异常错误")
        {
            var jsonObj = new JObject(
                new JProperty("state", 1),
                new JProperty("mess", mess),
                new JProperty("data", "")
                );
            return jsonObj;
        }

        /// <summary>
        ///  根据字段数组，字段值数组生成Json数据
        /// </summary>
        /// <returns></returns>
        public static JObject FiledToJson(string []fieldName,string[]fieldValue)
        {
            try
            {
                if (fieldName.Length == 0 || fieldValue.Length == 0 || fieldName.Length != fieldValue.Length)
                    return ErrorMess("字段名或者字段值有误");
                var obj = new JObject();
                for(int i=0; i<fieldName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fieldName[i]))
                        obj.Add(
                            new JProperty(fieldName[i],fieldValue[i])
                            );
                }
                return ResultSuccessMess(null);
            }
            catch (Exception e) { return ErrorMess(e.Message); }
        }
        /// <summary>
        ///  根据总记录数，数据返回分页格式数据
        /// </summary>
        /// <param name="number">总的数量</param>
        /// <param name="jarray">数据源</param>
        /// <returns></returns>
        public static JObject GetPageData(Object number, JArray jarray)
        {
            var obj = new JObject {new JProperty("total", number), new JProperty("rows", jarray)};
            return obj;
        }
        /// <summary>
        ///  将DataTable或List转换为Json格式的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  static string ObjectToJson(Object obj)
        {
            if (obj == null)
                return null;
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        ///  将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ObjectToJson<T>(Object obj)
        {
            return JsonConvert.DeserializeObject<T>(ObjectToJson(obj));
        }
        /// <summary>
        ///  将对象转换成json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">实例对象</param>
        /// <returns></returns>
        public static string DataContractJsonSerialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            string json;
            using (var ms = new MemoryStream())//定义一个stream用来存序列化之后的内容
            {
                serializer.WriteObject(ms, obj);
                json = Encoding.UTF8.GetString(ms.GetBuffer())
;//将stream读取成一个字符串形式的数据，并且返回
                ms.Close();
            }
            return json;
        }
        /// <summary>
        ///  将DataTable或List转换为JArray格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JArray ObjectToJArray(Object obj)
        {
            string myStr = ObjectToJson(obj);
            return JArray.Parse(myStr);
        }
        /// <summary>
        ///  将DataTable或List转换为JObject格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JObject ObjectToJObject(Object obj)
        {
            string myStr = ObjectToJson(obj);
            return JObject.Parse(myStr);
        }
        /// <summary>
        ///  将json字符串转换为泛型List集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static List<T> JsonStrToList<T>(string jsonStr,List<T> list)
        {
            return JsonConvert.DeserializeAnonymousType(jsonStr, list);
        }
        /// <summary>
        ///  将json字符串转换为实例对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static Object JsonStrToObject(string jsonStr,Object obj)
        {
            return JsonConvert.DeserializeObject(jsonStr, obj.GetType());
        }
        /// <summary>
        ///  将json字符串转换为泛型实例对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        public  static T JsonStrToObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        ///  根据json对象返回update语句
        /// </summary>
        /// <param name="obj">json对象</param>
        /// <param name="keyName">主键</param>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static string JsonStrToSql(JObject obj,string keyName,string tableName,string @where)
        {
            var sb = new StringBuilder();
            foreach(var jToken in obj.Children())
            {
                var pro = (JProperty) jToken;
                if (pro.Name.Equals(keyName))
                    continue;
                sb.Append(string.Format("{0}='{1}',", pro.Name, pro.Value));
            }
            return string.Format("update {0} set {1} where {2};", tableName, sb.ToString().Trim(','), @where);  
        }

        /// <summary>
        /// 将DataTable转化为Json字符串
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable table)
        {
            var JsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
            }
            return JsonString.ToString();
        }
    }
}
