using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace HCQ2_Common
{
    /// <summary>
    ///  图片压缩算法
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        ///  指定缩放类型（枚举）
        /// </summary>
        public enum ImageCompressType
        {
            //***指定高宽缩放（可能变形）
            Wh = 0,
            //***指定宽，高按比例
            W = 1,
            //***指定高，宽按比例
            H = 2,
            //***指定高宽裁减（不变形）
            Cut = 3,
            //***不指定，使用原始
            N = 4
        }
        /// <summary>
        ///  无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片路径</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <param name="type">压缩缩放类型</param>
        /// <param name="deal">当新路径和原始路径相同时是否删除原始图片，默认删除，不删除则新文件名称加1</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int height, int width, int flag, ImageCompressType type, bool deal = true)
        {
            string newFile = dFile;
            if (sFile.Equals(dFile))
                //新路径和原始路径相同时处理
                dFile = dFile.Split('.')[0] + "1" + dFile.Split('.')[1];
            Image iSource = Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;

            //****缩放后的宽度和高度
            int towidth = width;
            int toheight = height;

            int x = 0; int y = 0;
            int ow = iSource.Width; int oh = iSource.Height;
            #region 判断类别
            switch (type)
            {
                case ImageCompressType.N://***原始高宽
                    {
                        towidth = ow;
                        toheight = oh;
                    }
                    break;
                case ImageCompressType.Wh://指定高宽缩放（可能变形）
                    {

                    }
                    break;
                case ImageCompressType.W://指定宽 按比例
                    {
                        toheight = iSource.Height * width / iSource.Width;
                    }
                    break;
                case ImageCompressType.H://指定高 按比例
                    {
                        towidth = iSource.Width * height / iSource.Height;
                    }
                    break;
                case ImageCompressType.Cut://指定高宽裁减（不变形）
                    {
                        if (iSource.Width / (double)iSource.Height > towidth / (double)toheight)
                        {
                            ow = iSource.Height * towidth / toheight;
                            y = 0;
                            x = (iSource.Width - ow) / 2;
                        }
                        else
                        {
                            oh = iSource.Width * height / towidth;
                            x = 0;
                            y = (iSource.Height - oh) / 2;
                        }
                    }
                    break;
            }
            #endregion
            var ob = new Bitmap(towidth, toheight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle(x, y, towidth, toheight),
                new Rectangle(0, 0, iSource.Width, iSource.Height),
                GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            var ep = new EncoderParameters();
            var qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            var eParam = new EncoderParameter(Encoder.Quality, qy);
            ep.Param[0] = eParam; 
            try
            {
                ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegIcIinfo = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                //如若路径相同是否删除原始文件
                if (sFile.Equals(dFile) && deal && File.Exists(sFile))
                    File.Delete(sFile);
                if (jpegIcIinfo != null)
                    ob.Save(dFile, jpegIcIinfo, ep);//dFile是压缩后的新路径
                else
                    ob.Save(dFile, tFormat);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                eParam.Dispose();
                ep.Dispose();
                iSource.Dispose();
                ob.Dispose();
                if (sFile.Equals(newFile) && deal)
                {
                    //删除原始文件
                    if (File.Exists(sFile))
                        File.Delete(sFile);
                    //更新新文件名称
                    if (File.Exists(dFile))
                        File.Move(dFile, newFile);
                }
            }
        }

        /// <summary> 
        /// 按照指定像素大小截图
        /// 按照指定大小缩放图片，但是为了保证图片宽高比自动截取 
        /// </summary> 
        /// <param name="srcImage">原始图片路径</param> 
        /// <param name="iWidth">宽（像素）</param> 
        /// <param name="iHeight">高（像素）</param> 
        /// <returns></returns> 
        public static Bitmap SizeImageWithOldPercent(string sFile, int iWidth, int iHeight)
        {
            try
            {
                Image srcImage = Image.FromFile(sFile);
                // 要截取图片的宽度（临时图片） 
                int newW = srcImage.Width;
                // 要截取图片的高度（临时图片） 
                int newH = srcImage.Height;
                // 截取开始横坐标（临时图片） 
                int newX = 0;
                // 截取开始纵坐标（临时图片） 
                int newY = 0;
                // 截取比例（临时图片） 
                double whPercent = 1;
                whPercent = ((double)iWidth / (double)iHeight) * ((double)srcImage.Height / (double)srcImage.Width);
                if (whPercent > 1)
                {
                    // 当前图片宽度对于要截取比例过大时 
                    newW = int.Parse(Math.Round(srcImage.Width / whPercent).ToString());
                }
                else if (whPercent < 1)
                {
                    // 当前图片高度对于要截取比例过大时 
                    newH = int.Parse(Math.Round(srcImage.Height * whPercent).ToString());
                }
                if (newW != srcImage.Width)
                {
                    // 宽度有变化时，调整开始截取的横坐标 
                    newX = Math.Abs(int.Parse(Math.Round(((double)srcImage.Width - newW) / 2).ToString()));
                }
                else if (newH == srcImage.Height)
                {
                    // 高度有变化时，调整开始截取的纵坐标 
                    newY = Math.Abs(int.Parse(Math.Round(((double)srcImage.Height - (double)newH) / 2).ToString()));
                }
                // 取得符合比例的临时文件 
                Bitmap cutedImage = CutImage(srcImage, newX, newY, newW, newH);
                // 保存到的文件 
                Bitmap b = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(cutedImage, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(0, 0, cutedImage.Width, cutedImage.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 
        /// 剪裁 -- 用GDI+ 
        /// </summary> 
        /// <param name="b">原始Bitmap</param> 
        /// <param name="StartX">开始坐标X</param> 
        /// <param name="StartY">开始坐标Y</param> 
        /// <param name="iWidth">宽度</param> 
        /// <param name="iHeight">高度</param> 
        /// <returns>剪裁后的Bitmap</returns> 
        public static Bitmap CutImage(Image b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                // 开始截取坐标过大时，结束处理 
                return null;
            }
            if (StartX + iWidth > w)
            {
                // 宽度过大时只截取到最大大小 
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                // 高度过大时只截取到最大大小 
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 把一张图片（png bmp jpeg bmp gif）转换为byte数组
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(string imageUrl)
        {
            Image image = Image.FromFile(@imageUrl);
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                var buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                image.Dispose();
                return buffer;
            }
        }

        /// <summary>
        /// byte数组转换为Image对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// 从图片byte数组得到对应图片的格式，生成一张图片保存到磁盘上。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string CreateImageFromBytes(string path,string fileName, byte[] buffer)
        {
            string file = fileName;
            try
            {
                Image image = BytesToImage(buffer);
                ImageFormat format = image.RawFormat;
                if (format.Equals(ImageFormat.Jpeg))
                {
                    file += ".jpeg";
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    file += ".png";
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    file += ".bmp";
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    file += ".gif";
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    file += ".icon";
                }
                System.IO.FileInfo info = new System.IO.FileInfo(path + "/" + file);
                System.IO.Directory.CreateDirectory(info.Directory.FullName);
                File.WriteAllBytes(path + "/" + file, buffer);
            }
            catch
            {
                
            }
            return file;
        }

        /// <summary>
        ///  将Base64字符串转换为图片
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            Image img = (Image)binFormatter.Deserialize(memStream);
            return img;
        }
        /// <summary>
        ///  将图片转成base64
        /// </summary>
        /// <param name="bmap">图片</param>
        /// <param name="imagefile"></param>
        /// <returns></returns>
        public static string ImageToBase64(Bitmap bmap, string imagefile)
        {
            string strbaser64 = "";
            try
            {
                Bitmap bmp = (bmap != null) ? bmap : new Bitmap(imagefile);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                throw new Exception("Something wrong during convert!");
            }
            return strbaser64;
        }
    }
}
