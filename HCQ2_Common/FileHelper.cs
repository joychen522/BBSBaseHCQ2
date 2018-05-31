using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Web;

namespace HCQ2_Common
{
    /// <summary>
    ///  说明：文件帮助类
    ///  创建人：陈敏
    ///  创建时间：2014-12-29
    /// </summary>
    public static class FileHelper
    {
        #region  压缩文件

        /// <summary>
        /// 压缩指定目录下指定文件(包括子目录下的文件)
        /// </summary>
        /// <param name="zippath">args[0]为你要压缩的目录所在的路径 
        /// 例如：D:\\temp\\   (注意temp 后面加 \\ 但是你写程序的时候怎么修改都可以)</param>
        /// <param name="zipfilename">args[1]为压缩后的文件名及其路径
        /// 例如：D:\\temp.zip</param>
        /// <param name="fileFilter">文件过滤, 例如*.xml,这样只压缩.xml文件.注意：*.*表示所有</param>
        /// <param name="password"></param>
        public static bool ZipFileMain(string zippath, string zipfilename, string fileFilter = "*.*", string password = null)
        {
            try
            {
                //string filenames = Directory.GetFiles(args[0]);

                var crc = new Crc32();
                var s = new ZipOutputStream(File.Create(zipfilename));

                s.SetLevel(6); // 0 - store only to 9 - means best compression

                var di = new DirectoryInfo(zippath);

                FileInfo[] a = di.GetFiles(fileFilter);
                if (password != null)
                {
                    s.Password = password;
                }
                //压缩这个目录下的所有文件
                WriteStream(ref s, a, crc);
                s.Finish();
                s.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  压缩给定目录下的文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="a"></param>
        /// <param name="crc"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void WriteStream(ref ZipOutputStream s, IEnumerable<FileInfo> a, Crc32 crc)
        {
            try
            {
                foreach (FileInfo fi in a)
                {
                    FileStream fs = fi.OpenRead();
                    var buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string fileName = fi.Name;
                    var entry = new ZipEntry(fileName) {DateTime = DateTime.Now, Size = fs.Length};
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                    //压缩完成后，删除这个文件
                    fi.Delete();
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        internal static void DeleteFile(string destinationFile)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 创建文件夹并写入文件数据
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="FileName"></param>
        /// <param name="contenxt"></param>
        public static void CreateFile(string strPath,string FileName,string contenxt)
        {
            string path = strPath;
            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));//创建该文件
            }
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(path + "/" + FileName), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(contenxt);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string ReadeFile(string strPath)
        {
            StreamReader sr = new StreamReader(strPath);
            return sr.ReadToEnd();
        }
    }
}
