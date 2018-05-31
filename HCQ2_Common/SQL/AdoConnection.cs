using System.Configuration;
using HCQ2_Common.SQL;
namespace HCQ2_Common.SQL
{
    /// <summary>
    ///  ADO.NET配置
    /// </summary>
    public class AdoConnection
    {
        private static string Ip { get; set; }
        private static string DbName { get; set; }
        private static string UserId { get; set; }
        private static string Password { get; set; }

        /// <summary>
        ///  ADO.NET连接字符串
        /// </summary>
        private static string AdoConnectionStr { get;set;}

        public static string GetConnStr(string connStrName)
        {
            if (string.IsNullOrEmpty(AdoConnectionStr))
                return GetDBSetting(connStrName);
            return AdoConnectionStr;
        } 
        private static string GetDBSetting(string connStrName)
        {
            var connectStr = ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
            if (string.IsNullOrEmpty(connectStr))
                return null;
            var settingArray = connectStr.Split(';');
            foreach (var setting in settingArray)
            {
                var keyVal = setting.Split('=');
                switch (keyVal[0].ToLower())
                {
                    case "data source":
                        Ip = keyVal[1];
                        break;
                    case "initial catalog":
                        DbName = keyVal[1];
                        break;
                    case "user id":
                        UserId = keyVal[1];
                        break;
                    case "password":
                        Password = keyVal[1];
                        break;
                }
            }
            AdoConnectionStr = string.Format("Data Source ={0}; Initial Catalog = {1}; User ID = {2}; Password = {3}",
                Ip, DbName, UserId, Password);
            return AdoConnectionStr;
        }
    }
}
