using System.Security.Cryptography;
using System.Text;

namespace HCQ2WebAPI_Logic.Authority
{
    /// <summary>
    ///  授权验证WebAPI
    /// </summary>
    public class AuthorityCheck
    {
        /// <summary>
        ///  生成加密签名
        /// </summary>
        /// <param name="matchTimestamp">时间戳 年月日时分秒 yyyyMMddHHmmss</param>
        /// <param name="matchNonce">1-20位随机数字字符串</param>
        /// <returns></returns>
        public static string CreateMatchSignature(string matchTimestamp, string matchNonce)
        {
            var mergestr = string.Concat(HCQ2_Common.Constant.AuthorityConstant.SECRET_KEY, matchTimestamp,matchNonce);
            return CreateHashMd5(mergestr);
        }
        /// <summary>
        ///  生成Md5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string CreateHashMd5(string input)
        {
            using (HashAlgorithm hash = MD5.Create())
            {
                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
                return sBuilder.ToString();
            }
        }
        /// <summary>
        ///  身份合法验证
        /// </summary>
        /// <param name="matchSignature">加密签名</param>
        /// <param name="matchTimestamp">时间戳 年月日时分秒 yyyyMMddHHmmss</param>
        /// <param name="matchNonce">1-20随机字符串</param>
        /// <returns></returns>
        public static bool AuthoritySignature(string matchSignature, string matchTimestamp, string matchNonce)
        {
            if (string.IsNullOrEmpty(matchSignature))
                return false;
            if (matchSignature.Equals(CreateMatchSignature(matchTimestamp, matchNonce)))
                return true;
            return false;
        }
    }
}
