using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace HCQ2_Common.Encrypt
{
    /// <summary>
    /// @Descripte:EncryptHelper加密帮助类.        
    /// @Author:Joychen    
    /// @Date:2015/7/30    
    /// </summary>    
    public class EncryptHelper
    {
        static string HCQ2 = "HCQ2";//加密前缀
        //Md5 32位
        /// <summary>
        ///  MD5加密32位
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static string Md5Encryption(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            string cl = HCQ2 + str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X").PadLeft(2, '0');
            }
            return pwd;
            ////获取加密服务
            //MD5 md5 = MD5.Create();
            ////将需要加密的字符串转换为byte数组
            //byte[] buffer = Encoding.Unicode.GetBytes(str);
            ////加密数组
            //byte[] output = md5.ComputeHash(buffer);
            //var sb = new StringBuilder();
            //int i = 0;
            //for (; i < output.Length; i++)
            //{
            //    sb.Append(i.ToString("x2"));
            //}
            //return sb.ToString();
        }
        /// <summary>
        ///  验证Md5
        /// </summary>
        /// <param name="argInput">待验证字符串</param>
        /// <param name="argHash">哈希值</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(string argInput, string argHash)
        {
            string hashInputMd5 = Md5Encryption(argInput);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashInputMd5, argHash))
                return true;
            return false;
        }
        //AES
        /// <summary>
        ///  ASE加密
        /// </summary>
        /// <returns></returns>
        public static string AseEncryption(string toEncryption)
        {
            byte[] keyArray =
                Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncryption);

            var rDel = new RijndaelManaged { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  ASE解密
        /// </summary>
        /// <param name="toDecryption"></param>
        /// <returns></returns>
        public static string AseDecryption(string toDecryption)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = Convert.FromBase64String(toDecryption);

            var rDel = new RijndaelManaged { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        ///  加密数据
        /// </summary>
        /// <param name="str">待加密的数据</param>
        /// <param name="code">向数据中加入的特殊字符</param>
        /// <returns></returns>
        public static string EncryptDouble(string str, string code)
        {
            string str2 = EncryptSHA1(str);
            string str3 = EncryptSHA1(code);
            string str4 = string.Empty;
            StringBuilder builder = new StringBuilder(str2);
            StringBuilder builder2 = new StringBuilder(str3);
            int length = builder.Length;
            for (int i = 0; i < builder.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    char ch = builder[i];
                    str4 = str4 + ch.ToString();
                }
                else
                {
                    str4 = str4 + builder2[i].ToString();
                }
            }
            return str4;
        }
        public static string EncryptSHA1(string PasswordString)
        {
            if (string.IsNullOrEmpty(PasswordString))
            {
                PasswordString = string.Empty;
                return PasswordString;
            }
            PasswordString = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
            return PasswordString;
        }
        /// <summary>
        ///  生成Guid值
        /// </summary>
        /// <returns></returns>
        public static string CreateGuidValue()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
