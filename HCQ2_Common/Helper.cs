using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace HCQ2_Common
{
    /// <summary>
    ///  说明：基础帮助类
    ///  1.0：数据转换操作
    ///  2.0：加解密
    ///  3.0：其他公共方法
    ///  4.0：生成验证码
    /// </summary>
    public class Helper
    {
        #region 1.0 数据类型转换
        /// <summary>
        ///  判断数据是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object objValue)
        {
            if (objValue == null)
            {
                return true;
            }
            if (objValue == DBNull.Value)
            {
                return true;
            }
            char[] trimChars = new char[] { ' ' };
            return string.IsNullOrEmpty(objValue.ToString().Trim(trimChars));
        }
        /// <summary>
        ///  转换为字符串
        /// </summary>
        /// <param name="objValue">值</param>
        /// <returns></returns>
        public static string ToString(object objValue)
        {
            if (objValue==null)
            {
                return string.Empty;
            }
            if (ReferenceEquals(objValue, DBNull.Value))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(objValue.ToString().Trim(new[] { ' ' })))
            {
                return string.Empty;
            }
            return objValue.ToString();
        }
        /// <summary>
        ///  转换为bool型
        /// </summary>
        /// <param name="obj">值</param>
        /// <returns></returns>
        public static bool ObjectToBool(object obj)
        {
            bool flag = false;
            if(obj!=null)
            {
                if (ReferenceEquals(obj, DBNull.Value))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    return false;
                }
                if (obj.ToString() == "0")
                {
                    return false;
                }
                if (obj.ToString() == "1")
                {
                    return true;
                }
                if (bool.TryParse(obj.ToString(),out flag))
                {
                    return flag;
                }
            }
            return flag;
        }
        /// <summary>
        ///  转换为Decimal
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object objValue)
        {
            decimal num = 0.0M;
            if(objValue!=null)
            {
                if(ReferenceEquals(objValue,DBNull.Value))
                {
                    return num;
                }
                if(string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    return num;
                }
                if(decimal.TryParse(objValue.ToString(),out num))
                {
                    return num;
                }
            }
            return num;
        }
        /// <summary>
        ///  转换为float
        /// </summary>
        /// <param name="objValue">值</param>
        /// <returns></returns>
        public static float ToFloat(object objValue)
        {
            float num = 0f;
            if(objValue!=null)
            {
                if(ReferenceEquals(objValue,DBNull.Value))
                {
                    return num;
                }
                if(string.IsNullOrEmpty(objValue.ToString().Trim()))
                {
                    return num;
                }
                if(float.TryParse(objValue.ToString(),out num))
                {
                    return num;
                }
            }
            return num;
        }
        /// <summary>
        ///  转换为int
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ToInt(object objValue)
        {
            int num;
            if(int.TryParse(objValue.ToString(),out num))
            {
                return num;
            }
            return num;
        }
        /// <summary>
        ///  转换为Byte
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static byte ToByte(object objValue)
        {
            if (objValue.ToString() != "")
                return byte.Parse(objValue.ToString());
            else
                return 0;
        }
        /// <summary>
        ///  转换为Long
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static long ToLong(object objValue)
        {
            long num;
            if (long.TryParse(objValue.ToString(), out num))
                return num;
            else
                return 0;
        }
        /// <summary>
        ///  转换为DateTime
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objValue)
        {
            if (objValue.ToString() != "")
                return DateTime.Parse(objValue.ToString());
            else
                return DateTime.Now;
        }
        /// <summary>
        ///  格式化日期
        /// </summary>
        /// <param name="obj">日期串</param>
        /// <param name="format">格式化字符串</param>
        /// <returns></returns>
        public static string FormatDateTime(object obj,string format)
        {
            if (obj.ToString() != "")
                return DateTime.Parse(obj.ToString()).ToString(format);
            else
                return "";
        }
        /// <summary>   
        /// 判断用户输入是否为日期   
        /// 可判断格式如下（其中-可替换为.，不影响验证) 
        /// </summary>   
        /// <param ></param>   
        /// <returns></returns>   
        /// <remarks>   
        /// 可判断格式如下（其中-可替换为.，不影响验证)   
        /// YYYY | YYYY-MM |YYYY.MM| YYYY-MM-DD|YYYY.MM.DD | YYYY-MM-DD HH:MM:SS | YYYY.MM.DD HH:MM:SS | YYYY-MM-DD HH:MM:SS.FFF | YYYY.MM.DD HH:MM:SS:FF (年份验证从1000到2999年)
        /// </remarks>   
        public static bool IsDateTime(string strValue)
        {
            if (strValue == null || strValue == "")
            {
                return false;
            }
            string regexDate = @"[1-2]{1}[0-9]{3}((-|[.]){1}(([0]?[1-9]{1})|(1[0-2]{1}))((-|[.]){1}((([0]?[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0-3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\.[0-9]{3})?)?)?)?$";
            if (Regex.IsMatch(strValue, regexDate))
            {
                //以下各月份日期验证，保证验证的完整性   
                int _IndexY = -1;
                int _IndexM = -1;
                int _IndexD = -1;
                if (-1 != (_IndexY = strValue.IndexOf("-")))
                {
                    _IndexM = strValue.IndexOf("-", _IndexY + 1);
                    _IndexD = strValue.IndexOf(":");
                }
                else
                {
                    _IndexY = strValue.IndexOf(".");
                    _IndexM = strValue.IndexOf(".", _IndexY + 1);
                    _IndexD = strValue.IndexOf(":");
                }
                //不包含日期部分，直接返回true   
                if (-1 == _IndexM)
                {
                    return true;
                }
                if (-1 == _IndexD)
                {
                    _IndexD = strValue.Length + 3;
                }
                int iYear = Convert.ToInt32(strValue.Substring(0, _IndexY));
                int iMonth = Convert.ToInt32(strValue.Substring(_IndexY + 1, _IndexM - _IndexY - 1));
                int iDate = Convert.ToInt32(strValue.Substring(_IndexM + 1, _IndexD - _IndexM - 4));
                //判断月份日期   
                if ((iMonth < 8 && 1 == iMonth % 2) || (iMonth > 8 && 0 == iMonth % 2))
                {
                    if (iDate < 32)
                    { return true; }
                }
                else
                {
                    if (iMonth != 2)
                    {
                        if (iDate < 31)
                        { return true; }
                    }
                    else
                    {
                        //闰年   
                        if ((0 == iYear % 400) || (0 == iYear % 4 && 0 < iYear % 100))
                        {
                            if (iDate < 30)
                            { return true; }
                        }
                        else
                        {
                            if (iDate < 29)
                            { return true; }
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region 3.0 其他公共方法
        /// <summary>
        ///  判断字符串是否为中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsChineseChar(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        ///  虚拟路径转换为物理路径
        /// </summary>
        /// <param name="virtualUrl">虚拟路径</param>
        /// <returns></returns>
        public static string VirtualToEntryPath(string virtualUrl)
        {
            if (string.IsNullOrEmpty(virtualUrl))
                return null;
            return HttpContext.Current.Server.MapPath(virtualUrl);
        }
        /// <summary>
        ///  清除DataTable里重复的行
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="key">列数组</param>
        /// <returns></returns>
        public static DataTable ClearDoubleRows(DataTable dt,string[] key)
        {
            DataView dv = dt.DefaultView;
            return dv.ToTable(true, key);
        }
        /// <summary>
        ///  清除字符串中的所有空格
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static string ClearStrNull(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            return System.Text.RegularExpressions.Regex.Replace(str, @"\s+", "");
        }
        /// <summary>
        ///  分割字符串
        /// </summary>
        /// <param name="strContent">原始字符串</param>
        /// <param name="strSplit">需要分割的字符</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent,string strSplit)
        {
            if (string.IsNullOrEmpty(strContent))
                return new string[0] { };
            if (strContent.IndexOf(strSplit) < 0)
                return new string[] { strContent};
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否转换
        /// </summary>
        /// <returns></returns>
        public static string IFbool(string str)
        {
            if (str.Equals("是"))
            {
                return "1";
            }
            else if (str.Equals("否"))
            {
                return "0";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 判断是否是汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected bool Han(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 根据虚拟路径返回物理路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string MapPath(string str)
        {
            return HttpContext.Current.Server.MapPath(str);
        }

        /// <summary>
        /// 去除DataTable里面重复行
        /// 参数：DataTable、重复列名
        /// </summary>
        /// <param name="Dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DataTable ClearDataTableRow(DataTable Dt, string[] key)
        {
            DataView Dv = Dt.DefaultView;
            return Dv.ToTable(true, key);
        }

        /// <summary>
        /// 清除字符串里面的空白
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearNull(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"\s+", ""); ;
        }
        #endregion

        #region 4.0 生成验证码
        /// <summary>
        ///  获得验证码字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] getCheckCode()
        {
            using(Image img=new Bitmap(80,30))
            {
                string strCode = GetRandomStr();
                HttpContext.Current.Session["vcode"] = strCode;
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);
                    g.DrawRectangle(Pens.Blue, 0, 0, img.Width - 1, img.Height - 1);
                    DrawPoint(g);
                    g.DrawString(strCode, new Font("微软雅黑", 15), Brushes.Blue, new PointF(5, 2));
                    DrawPoint(g);
                    using (System.IO.MemoryStream ms = new MemoryStream())
                    {
                        //将图片 保存到内存流中
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //将内存流 里的 数据  转成 byte 数组 返回
                        return ms.ToArray();
                    }
                }
            }
            //return null;
        }
        Random random = new Random();
        private string GetRandomStr()
        {
            string str = string.Empty;
            string[] strArr = { "a", "b", "b", "蛇", "1", "2", "仓", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(strArr.Length);
                str += strArr[index];
            }
            return str;
        }
        private void DrawPoint(Graphics g)
        {
            Pen[] pens = { Pens.Blue, Pens.Black, Pens.Red, Pens.Green };
            Point p1;
            Point p2;
            int length = 1;
            for (int i = 0; i < 50; i++)
            {
                p1 = new Point(random.Next(79), random.Next(29));
                p2 = new Point(p1.X - length, p1.Y - length);
                length = random.Next(5);
                g.DrawLine(pens[random.Next(pens.Length)], p1, p2);
            }
        }
        #endregion

        #region 5.0 全球唯一码GUID
        public static string GetGuid
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("-", "");
            }
        }
        #endregion

        #region 6.0 组装SQL
        /// <summary>
        /// 返回更新所有update语句
        /// 参数：表名、更新字符串
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string Update(string TableName, string Str)
        {
            return string.Format("update {0} set {1}", TableName, Str);
        }
        /// <summary>
        /// 返回更新update语句
        /// 参数：表名、更新字符串、条件
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Str"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static string Update(string TableName, string Str, string Where)
        {
            return string.Format("update {0} set {1} where {2}", TableName, Str, Where);
        }
        /// <summary>
        /// 根据表明获取select语句
        /// 参数：表名
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static string Select(string TableName)
        {
            return string.Format("select * from {0}", TableName);
        }

        /// <summary>
        /// 获取指定条件的select语句
        /// 参数：表名、条件
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static string Select(string TableName, string Where)
        {
            return string.Format("select * from {0} where {1}", TableName, Where);
        }
        /// <summary>
        /// 获取insert语句
        /// 参数：表名、更新的字段名、更新的值
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="UStr"></param>
        /// <param name="UValue"></param>
        /// <returns></returns>
        public static string Insert(string TableName, string UStr, string UValue)
        {
            return string.Format("INSERT INTO {0} ({1}) values ({2})", TableName, UStr, UValue);
        }
        /// <summary>
        /// 删除SQL
        /// 参数：表名、条件字段名、值
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FolderName"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Delete(string TableName, string Where)
        {
            return string.Format("delete {0} where {1}", TableName, Where);
        }
        /// <summary>
        /// 删除SQL
        /// 参数：表名
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static string Delete(string tablename)
        {
            return string.Format("delete {0}", tablename);
        }
        /// <summary>
        /// 根据DataTable返回插入SQL语句
        /// 参数：DataTable、表明
        /// 说明：空的数据源会抛出异常
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string Insert(DataTable Data, string TableName, string sp_id)
        {
            if (Data == null)
                throw new Exception("DataTable为空");
            StringBuilder sql = new StringBuilder();
            StringBuilder FolderName = new StringBuilder();
            StringBuilder Values = new StringBuilder();
            int mymax = Data.Columns.Count;
            foreach (DataRow pro in Data.Rows)
            {
                for (int i = 0; i < mymax; i++)
                {
                    if (Data.Columns[i].ColumnName.ToString().Equals(sp_id))
                    {
                        continue;
                    }
                    else
                    {
                        FolderName.Append(Data.Columns[i].ColumnName.ToString() + ",");
                        Values.Append(string.Format("'{0}',", pro[Data.Columns[i].ColumnName.ToString()].ToString()));
                    }
                }
                FolderName.Remove(FolderName.ToString().Length - 1, 1);
                Values.Remove(Values.ToString().Length - 1, 1);
                sql.Append(Insert(TableName, FolderName.ToString(), Values.ToString()) + ";");
                FolderName.Clear();
                Values.Clear();
            }
            return sql.ToString();
        }
        #endregion

        #region 7.0 分页
        /// <summary>
        /// 获取分页SQL
        /// 参数：表名、标识列名、页码、每页显示多少条
        /// </summary>
        /// <returns></returns>
        public static string PageSql(string TableName, string FolderName, int Page, int Row)
        {
            StringBuilder sb = new StringBuilder();
            Page = Page - 1;
            sb.Append(string.Format("select * from {0} where {1} in ", TableName, FolderName));
            sb.Append(string.Format("(select top {0} {1} from {2} where {3}", Row, FolderName, TableName, FolderName));
            sb.Append(string.Format(" not in(select top ({0}*{1}) {2} from {3}))", Row, Page, FolderName, TableName));
            return sb.ToString();
        }
        /// <summary>
        /// 获取分页SQL带条件
        /// 参数：表名、标识列名、页码、每页显示多少条,条件字段名,条件值
        /// </summary>
        /// <returns></returns>
        public static string PageSql(string TableName, string FolderName, int Page, int Row, string FolderNameWhere, string WhereValue)
        {
            StringBuilder sb = new StringBuilder();
            Page = Page - 1;
            sb.Append(string.Format("select * from {0} where {1} in ", TableName, FolderName));
            sb.Append(string.Format("(select top {0} {1} from {2} where {3} not in", Row, FolderName, TableName, FolderNameWhere));
            sb.Append(string.Format("(select top ({0}*{1}) {2} from {3} where {4}='{5}' order by {6})", Row, Page, FolderNameWhere, TableName, FolderNameWhere, WhereValue, FolderName));
            sb.Append(string.Format("and {0}='{1}' order by {2})", FolderNameWhere, WhereValue, FolderName));
            return sb.ToString();
        }

        /// <summary>
        /// 获取分页SQL-无自增列方式
        /// 参数：表名、排序字段名、页码、每页显示数量、true
        /// </summary>
        /// <returns></returns>
        public static string PageSql(string TableName, string OrderFolder, int Page, int Row, bool bl)
        {
            StringBuilder sql = new StringBuilder();
            Page = Page - 1;
            sql.Append(string.Format("select TT.* from (SELECT ROW_NUMBER() OVER(ORDER BY T.{0}) AS Row,T.* FROM {1} T) TT ", OrderFolder, TableName));
            sql.Append(string.Format(" where TT.Row in (select top {0} TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{1}) AS Row,T.* FROM {2} T) TT", Row, OrderFolder, TableName));
            sql.Append(" where TT.Row not in");
            sql.Append(string.Format("(select top ({0}*{1}) TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{2}) AS Row,T.* FROM {3} T) TT))", Page, Row, OrderFolder, TableName));
            return sql.ToString();
        }

        /// <summary>
        /// 获取分页SQL-无自增列方式-带条件查询
        /// 参数：表名、排序字段名、页码、每页显示数量、条件字段、条件值、true
        /// </summary>
        /// <returns></returns>
        public static string PageSql(string TableName, string OrderFolder, int Page, int Row, string WhereFolder, string WhereValue, bool bl)
        {
            StringBuilder sql = new StringBuilder();
            Page = Page - 1;
            sql.Append(string.Format("select TT.* from (SELECT ROW_NUMBER() OVER(ORDER BY T.{0}) AS Row,T.* FROM {1} T) TT ", OrderFolder, TableName));
            sql.Append(" where TT.Row in");
            sql.Append(string.Format("(select top {0} TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{1}) AS Row,T.* FROM {2} T) TT ", Row, OrderFolder, TableName));
            sql.Append(" where TT.Row not in");
            sql.Append(string.Format("(select top ({0}*{1}) TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{2}) AS Row,T.* FROM {3} T) TT ", Page, Row, OrderFolder, TableName));
            sql.Append(string.Format(" where TT.{0}='{1}' order by TT.Row) and TT.{2}='{3}' order by TT.Row)", WhereFolder, WhereValue, WhereFolder, WhereValue));
            return sql.ToString();
        }

        /// <summary>
        /// 获取分页SQL-无自增列方式-带多条件查询
        /// 参数：表名、排序字段名、页码、每页显示数量、条件、true
        /// 说明：条件字段前面需加TT.因为是表名的别名
        /// </summary>
        /// <returns></returns>
        public static string PageSql(string TableName, string OrderFolder, int Page, int Row, string WhereValue, bool bl)
        {
            StringBuilder sql = new StringBuilder();
            Page = Page - 1;
            sql.Append(string.Format("select TT.* from (SELECT ROW_NUMBER() OVER(ORDER BY T.{0}) AS Row,T.* FROM {1} T) TT ", OrderFolder, TableName));
            sql.Append(" where TT.Row in");
            sql.Append(string.Format("(select top {0} TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{1}) AS Row,T.* FROM {2} T) TT ", Row, OrderFolder, TableName));
            sql.Append(" where TT.Row not in");
            sql.Append(string.Format("(select top ({0}*{1}) TT.Row from (SELECT ROW_NUMBER() OVER(ORDER BY T.{2}) AS Row,T.* FROM {3} T) TT ", Page, Row, OrderFolder, TableName));
            sql.Append(string.Format(" where {0} order by TT.Row) and {1} order by TT.Row)", WhereValue, WhereValue));
            return sql.ToString();
        }
        /// <summary>
        ///  直接给sql语句添加排序功能
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="TableName">待排序的表明</param>
        /// <param name="FolderName">待排序的字段名</param>
        /// <param name="Page">第几页</param>
        /// <param name="Row">每页记录数</param>
        /// <param name="whereConcat">排序连接字符，有where条件的则不需要传递</param>
        /// <returns></returns>
        public static string PageSql(string Sql, string TableName, string FolderName, int Page, int Row,string whereConcat="")
        {
            Page = Page - 1;
            StringBuilder sb = new StringBuilder();
            sb.Append(Sql+whereConcat);
            sb.Append(
                string.Format("{0} in(select top {1} from {2} where {0} not in(select top ({3}*{1}) {0} from {2}))",
                    FolderName, Row, TableName, Page));
            return sb.ToString();
        }
        #endregion

        #region 8.0 添加事物处理
        /// <summary>
        /// 为SQL语句添加事务
        /// 参数：执行的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string TranSql(string sql)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("begin tran\n");
            sb.Append(sql + "\n");
            sb.Append("if @@ERROR<>0\n");
            sb.Append("begin\n");
            sb.Append("	rollback tran\n");
            sb.Append("end\n");
            sb.Append("else\n");
            sb.Append("begin\n");
            sb.Append("	commit tran\n");
            sb.Append("end");
            return sb.ToString();
        }
        #endregion
    }
}
