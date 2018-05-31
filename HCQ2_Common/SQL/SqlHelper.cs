using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HCQ2_Common.SQL
{
    /// <summary>
    ///  Ado.net SQL帮助类
    /// </summary>
    public static class SqlHelper
    {
        private static readonly string  ConnName="myDataBase";
        private static string connStrName;
        /// <summary>
        ///  链接字符串
        /// </summary>
        public static string connStr 
        {
            get
            {
                try{
                    connStrName = VirtualPath;
                }
                catch{
                    connStrName = ConnName;
                }
                connStrName = Helper.IsNullOrEmpty(connStrName) ? ConnName : connStrName;
                string result = AdoConnection.GetConnStr(connStrName);
                if (string.IsNullOrEmpty(result))
                    return AdoConnection.GetConnStr(ConnName);
                return result;
            }
        }

        public static SqlConnection dbConn
        {
            get { return new SqlConnection {ConnectionString = connStr}; }
        }
        #region IIS配置的虚拟名称
        /// <summary>
        ///  IIS配置的虚拟名称
        /// </summary>
        public static string VirtualPath
        {
            get
            {
                if (Helper.IsNullOrEmpty(HttpContext.Current.Application["VirtualPath"]))
                {
                    string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
                    HttpContext.Current.Application["VirtualPath"] = appDomainAppVirtualPath.Substring(1, appDomainAppVirtualPath.Length - 1);
                }
                return Helper.ToString(HttpContext.Current.Application["VirtualPath"]);
            }
            set
            {
                HttpContext.Current.Application["VirtualPath"] = value;
            }
        }
        #endregion


        #region 封装Insert/update/delete
        /// <summary>
        ///  封装Insert/update/delete~返回受影响的行数
        /// </summary>
        /// <param name="sql">执行sql语句</param>
        /// <param name="cmdType">执行类别</param>
        /// <param name="psm">可变参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,CommandType cmdType=CommandType.Text,params SqlParameter[] psm)
        {
            //创建SqlConnection
           using(SqlConnection con=new SqlConnection(connStr))
           {
               //创建SqlCommand
               using(SqlCommand cmd=new SqlCommand(sql,con))
               {
                   cmd.CommandType = cmdType;
                   if(psm!=null)
                   {
                       cmd.Parameters.AddRange(psm);
                   }
                   con.Open();
                   return cmd.ExecuteNonQuery();
               }
           }
        }
        #endregion

        #region 返回单个值
        /// <summary>
        ///  返回单个值Object
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="cmdType">执行类别</param>
        /// <param name="psm">可变参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType cmdType = CommandType.Text, params SqlParameter[] psm)
        {
            //创建SqlConnection
            using(SqlConnection con = new SqlConnection(connStr))
            {
                using(SqlCommand cmd =new SqlCommand(sql,con))
                {
                    cmd.CommandType = cmdType;
                    if(psm!=null)
                    {
                        cmd.Parameters.AddRange(psm);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region 返回多行多列
        /// <summary>
        ///  返回多行多列SqlDataReader
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="cmdType">执行类别</param>
        /// <param name="psm">可变参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql,CommandType cmdType = CommandType.Text, params SqlParameter[] psm)
        {
            SqlConnection sqlCon = new SqlConnection(connStr);
            //创建SqlCommand
            using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
            {
                cmd.CommandType = cmdType;
                if (psm != null)
                {
                    cmd.Parameters.AddRange(psm);
                }
                //打开连接
                try
                {
                    sqlCon.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch(Exception)
                {
                    sqlCon.Dispose();
                    throw;
                }
            }
        }
        #endregion

        #region 返回DataTable
        /// <summary>
        ///  返回DataTable
        /// </summary>
        /// <param name="sql">执行的语句</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="psm">可变参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql,CommandType cmdType = CommandType.Text, params SqlParameter[] psm)
        {
            DataTable dt = new DataTable();
            using(SqlDataAdapter sda=new SqlDataAdapter(sql,connStr))
            {
                sda.SelectCommand.CommandType = cmdType;
                if(psm!=null)
                {
                    sda.SelectCommand.Parameters.AddRange(psm);
                }
                sda.Fill(dt);
            }
            return dt;
        }
        #endregion

        #region 封装Sql参数数组
        /// <summary>
        ///  封装参数数组
        /// </summary>
        /// <param name="obj">泛型集合</param>
        /// <returns></returns>
        public static SqlParameter[] GetParameters(Dictionary<string,object> obj)
        {
            SqlParameter[] psm = new SqlParameter[obj.Count];
            if(obj!=null)
            {
                int i = 0;
                foreach(KeyValuePair<string,object> item in obj)
                {
                    psm[i] = new SqlParameter(item.Key, item.Value);
                    i++;
                }
            }
            else
            {
                return null;
            }
            return psm;
        }
        #endregion
    }
}
