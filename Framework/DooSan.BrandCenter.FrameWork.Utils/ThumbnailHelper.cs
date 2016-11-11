using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

using System.Drawing.Drawing2D;
using System.Configuration;
using System.Diagnostics;

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Controls;

using System.Windows.Converters; //C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.0\WindowsBase.dll
using System.Windows.Markup;
using DooSan.BrandCenter.FrameWork.Static;

namespace CII.CMS.Framework.Helper
{
    public class ThumbnailHelper
    {


        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }


        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(outFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);

                    }
                }
            }
        }


        public static byte[] Compress(byte[] bytearr)
        {
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(bytearr, 0, bytearr.Length);
            }
            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);
            return compressedData;


            //var gZipBuffer = new byte[compressedData.Length + 4];
            //Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            //Buffer.BlockCopy(BitConverter.GetBytes(bytearr.Length), 0, gZipBuffer, 0, 4);
            //return Convert.ToBase64String(gZipBuffer);

        }

        public static byte[] Decompress(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 0, gZipBuffer.Length);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return buffer;
            }
        }
        //private ImageCodecInfo GetEncoder(ImageFormat format)
        //{

        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.FormatID == format.Guid)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}
        //public static void IMGInfo(string filepath, ImageFileInfo result)
        //{
        //    FileInfo fileinfo = new FileInfo(filepath);
        //    result.FileSize = fileinfo.Length;

        //    var img = Image.FromFile(filepath);

        //    result.FileType = img.RawFormat.ToString();
        //    result.width = img.Width;
        //    result.height = img.Height;

        //}



        private static byte[] BMP2JPEG(string filepath, Image img)
        {
            //bmp가 아니면 그대로 리턴
            //if (!ImageFormat.Bmp.Equals(img.RawFormat))
            //jpg면 그냥 리턴하고 그외는 모두 변환
            if (ImageFormat.Jpeg.Equals(img.RawFormat))
            {
                FileStream FileStreams = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] bytearr = new byte[FileStreams.Length];
                FileStreams.Read(bytearr, 0, (int)FileStreams.Length);
                return bytearr;

            }
            //"B96B3CAB-0728-11D3-9D7B-0000F81EF32E"
            var memoryStream = new MemoryStream();
            memoryStream.Position = 0;

            img.Save(memoryStream, ImageFormat.Jpeg);

            var buffer = new byte[memoryStream.Length];
            return memoryStream.ToArray();
        }

        //public static byte[] BMP2JPEG(string filepath, DownloadFile result, int PercentSize = 100)
        //{
        //    //Bitmap bmp1 = new Bitmap(filepath);
        //    //bmp1.Save(memoryStream, ImageFormat.Jpeg);


        //    var img = Image.FromFile(filepath);
        //    result.width = img.Width;
        //    result.height = img.Height;

        //    if (PercentSize != 100)
        //    {
        //        double Percent = PercentSize * 0.01;
        //        img = PercentImage(img, Percent);
        //    }
        //    //요기는변환된 사이즈 담기
        //    //result.width = img.Width;
        //    //result.height = img.Height;

        //    return BMP2JPEG(filepath, img);
        //}


        //이미지 높이, 넓이 가져오기 - 썸네일용도
        public static Size GetImageSize(string filepath)
        {

            var img = Image.FromFile(filepath);
            return img.Size;
        }

        public static byte[] BMP2JPEG(string filepath, int PercentSize = 100)
        {

            var img = Image.FromFile(filepath);

            if (PercentSize != 100)
            {
                double Percent = PercentSize * 0.01;
                img = PercentImage(img, Percent);
            }

            return BMP2JPEG(filepath, img);
        }
        //public byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    return ms.ToArray();
        //}


        //public static byte[] BMP2JPEGResize(string filepath, DownloadFile result, int width = -1, int height = -1)
        //{
        //    //Bitmap bmp1 = new Bitmap(filepath);
        //    //bmp1.Save(memoryStream, ImageFormat.Jpeg);


        //    var img = Image.FromFile(filepath);
        //    result.width = img.Width;
        //    result.height = img.Height;

        //    if (width != -1)
        //    {
        //        Size size = new Size(width, height);
        //        img = resizeImage(img, size);
        //    }

        //    return BMP2JPEG(filepath, img);
        //}
        public static byte[] BMP2JPEGResize(string filepath, int width = -1, int height = -1)
        {

            var img = Image.FromFile(filepath);

            if (width != -1)
            {
                Size size = new Size(width, height);
                img = resizeImage(img, size);
            }

            return BMP2JPEG(filepath, img);
        }


        /// <summary>
        /// home, list 2개의 썸네일을 동시저장한다.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="orImageName"></param>
        /// <param name="quality"></param>
        public static ThumbMakeResponse SaveJpegFromHeight(string path, string orImageName, string custompath = "", long quality = 100)
        {
            var list = new List<ThumbTypeClass>();
                        
            list.Add(SaveJpegFromHeight(path, Thumbtype.Home, orImageName, Path.Combine(custompath, Thumbtype.Home.ToString()), quality));
            list.Add(SaveJpegFromHeight(path, Thumbtype.List, orImageName, Path.Combine(custompath, Thumbtype.List.ToString()), quality));

            ThumbMakeResponse thlist = new ThumbMakeResponse();
            thlist.Thumblist = list;
            return thlist;
        }
        
        /// <summary>
        /// 대표이미지 저장용. orImageName 은 확장자를 제외한 원 파일명. symbol은 높이가 190이상인것만
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumbType"></param>
        /// <param name="orImageName"></param>
        /// <param name="quality"></param>
        public static ThumbTypeClass SaveJpegFromHeight(string path, Thumbtype thumbType, string orImageName, string custompath = "", long quality = 100)
        {
            Bitmap img = new Bitmap(path);
            int orh = img.Height;
            int orw = img.Width;
            int chw;

            /*
            int thHeight = 190; //Thumbtype.Symbol 일때

            if (thumbType == Thumbtype.Home)
                thHeight = 150;
            
            
            if (thumbType == Thumbtype.List) //list는 고정 width
            {
                thHeight = 74;
                chw = 49;
            }
            else
                chw = (orw * 190) / orh;
            */


            var wsize = new System.Windows.Size((double)img.Width, (double)img.Height);
            var thsize = new ThumbTypeClass(wsize, thumbType); //.ThumbSize

            int thHeight = thsize.Height;
            chw = thsize.Width;

            var bitc = ResizeBitmap(img, chw, thHeight);

            //리턴용. 굳이 높이까지 할 필요는 없지만..
            thsize.Width = bitc.Width;
            thsize.Height = bitc.Height;

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");
            if (jpegCodec == null)
                return thsize;
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            string ImageName = string.Format("{0}_{1}.jpg", orImageName, thumbType.ToString() );

            string thumbpath = "c:\\";

            if (custompath.Equals(""))
            {
                if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                    thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];

                if (!Directory.Exists(thumbpath))
                    Directory.CreateDirectory(thumbpath);

            }
            //직접 경로 전달시
            else
            {
                if (!Directory.Exists(custompath))
                    Directory.CreateDirectory(custompath);

                thumbpath = custompath;
            }
            
            //기존 파일이 존재시 delete
            if (File.Exists(Path.Combine(thumbpath, ImageName)))
                File.Delete(Path.Combine(thumbpath, ImageName));

            //bitc.Save(Path.Combine(thumbpath, ImageName), jpegCodec, encoderParams);
            bitc.Save(Path.Combine(thumbpath, ImageName), ImageFormat.Jpeg);
            img.Dispose();
            bitc.Dispose();

            return thsize;

        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");
            if (jpegCodec == null)
                return;
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Image PercentImage(Image imgToResize, double PercentSize = 1)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;

            nPercent = (float)PercentSize;

            //이하 같은 코드
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            }
            return (Image)b;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            //작은 쪽 percent로 맞춤
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //이하 같은 코드
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            }
            return (Image)b;
        }

        //shell 이미지 저장 방식의 재저장.
        public static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }


        public static Bitmap GetThumbnailIcon(string VideoPath, Thumbtype thumbType, System.Drawing.Imaging.ImageFormat Format, int width = 74, int height = 49)
        {
            if (!File.Exists(VideoPath))
                throw new InvalidOperationException("Video Not Exists!");

            string thumbpath = "c:\\";

            if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];


            var shell = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(VideoPath);
            //System.Windows.Size size = new System.Windows.Size(height, width);
            //shell.Thumbnail.CurrentSize = size;
            return ResizeBitmap(shell.Thumbnail.ExtraLargeBitmap, width, height);

        }

        /// <summary>
        /// 동영상에서 직접 이미지 bitmap을 리턴 (웹에서는 사용이 다소 힘들고 cs에선 더 효율적일듯)
        /// </summary>
        /// <param name="VideoPath"></param>
        /// <param name="thumbType"></param>
        /// <returns></returns>
        public static Bitmap GetThumbnailIcon(string VideoPath, Thumbtype thumbType)
        {
            if (!File.Exists(VideoPath))
                throw new InvalidOperationException("Video Not Exists!");

            string thumbpath = "c:\\";

            if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];


            var shell = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(VideoPath);

            var thsize = new ThumbTypeClass(thumbType); //.ThumbSize
            //shell.Thumbnail.CurrentSize = thsize;

            //바로 저장시엔 shell.Thumbnail.Bitmap이 좋지만 리사이즈시엔 ExtraLargeBitmap을 해야 퀄리티가 유지된다.
            return ResizeBitmap(shell.Thumbnail.ExtraLargeBitmap, thsize.Width, thsize.Height);

        }


        //기본 높,넓이는 리스트 기준
        //해상도별 리사이즈는 Bitmap이 제대로인데 비율이 유지되는 문제가 있다. 즉 세로 기준으로 들어간다.
        /// <summary>
        /// window shell 을 이용하여 이미지 리턴. ffmpeg보다 더 빠르고 윈도우 탐색기에서 보는것과 같은 이미지가 나오는게 장점.
        /// 정해진 장면이 단점. 그리고 1개의 영상만 테스트해서 불명확하나 큰 사이즈일 경우 용량이 다소 더 크게 나옴
        /// </summary>
        /// <param name="VideoPath"></param>
        /// <param name="thumbType"></param>
        /// <param name="ImageName"></param>
        /// <param name="Format"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SaveThumbnailIcon(string VideoPath, Thumbtype thumbType, string ImageName, System.Drawing.Imaging.ImageFormat Format, int width = 74, int height = 49)
        {
            if (!File.Exists(VideoPath))
                throw new InvalidOperationException("Video Not Exists!");

            string thumbpath = "c:\\";

            if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];

            //기존 파일이 존재시 delete
            if (File.Exists(Path.Combine(thumbpath, ImageName)))
                File.Delete(Path.Combine(thumbpath, ImageName));

            var shell = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(VideoPath);
            //System.Windows.Size size = new System.Windows.Size(height, width);
            //shell.Thumbnail.CurrentSize = size;
            
            var bitmap = ResizeBitmap(shell.Thumbnail.ExtraLargeBitmap, width, height);

            //if (thumbType == Thumbtype.Home)
            //    bitmap = MergeVideoImage(bitmap);

            //shell.Thumbnail.Bitmap.Save(Path.Combine(thumbpath, ImageName), Format);
            bitmap.Save(Path.Combine(thumbpath, ImageName), Format);
        }

        //파라미터 간략화
        public static ThumbTypeClass SaveThumbnailIcon(string VideoPath, Thumbtype thumbType, string ImageName, string custompath = "")
        {
            if (!File.Exists(VideoPath))
                throw new InvalidOperationException("Video Not Exists!");

            string thumbpath = "c:\\";

            if (custompath.Equals(""))
            {
                if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                    thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];
            }
            //직접 경로 전달시
            else
            {
                if (!Directory.Exists(custompath))
                    Directory.CreateDirectory(custompath);

                thumbpath = custompath;
            }


            //기존 파일이 존재시 delete
            if (File.Exists(Path.Combine(thumbpath, ImageName)))
                File.Delete(Path.Combine(thumbpath, ImageName));


            var shell = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(VideoPath);

            //비트맵 사이즈냐, 원본 비디오 사이즈냐.. 결과는 같다.다만 type cast가 생기지 않는 비트맵 사이즈가 나은듯
//            var wsize = new System.Windows.Size(shell.Thumbnail.Bitmap.Size.Width, shell.Thumbnail.Bitmap.Size.Height);
            var wsize = new System.Windows.Size((double)shell.Properties.System.Video.FrameWidth.Value, (double)shell.Properties.System.Video.FrameHeight.Value);
            var thsize = new ThumbTypeClass(wsize, thumbType); //.ThumbSize
            //shell.Thumbnail.CurrentSize = thsize;

            //바로 저장시엔 shell.Thumbnail.Bitmap이 좋지만 리사이즈시엔 ExtraLargeBitmap을 해야 퀄리티가 유지된다.
            var bitmap = ResizeBitmap(shell.Thumbnail.ExtraLargeBitmap, thsize.Width, thsize.Height);

            //실제 이미지 사이즈 반환
            thsize.Width = bitmap.Width;
            thsize.Height = bitmap.Height;

            //if (thumbType == Thumbtype.Home)
            //    bitmap = MergeVideoImage(bitmap);

            //shell.Thumbnail.ExtraLargeBitmap.Save(Path.Combine(thumbpath, ImageName), Format);
            bitmap.Save(Path.Combine(thumbpath, ImageName), System.Drawing.Imaging.ImageFormat.Jpeg);

            return thsize;
        }
        /// <summary>
        ///home, list 한번 저장. imagenmae은 확장자 없이 순수 파일명만 넘긴다. 저장은  _list.jpg와 같이 저장된다. 
        /// </summary>
        /// <param name="VideoPath"></param>
        /// <param name="ImageName"></param>
        public static ThumbMakeResponse SaveThumbnailIconAll(string VideoPath, string ImageName, string custompath = "")
        {
            var list = new List<ThumbTypeClass>();

            list.Add(SaveThumbnailIcon(VideoPath, Thumbtype.Home, string.Format("{0}_home.jpg", ImageName), Path.Combine(custompath, Thumbtype.Home.ToString())));
            list.Add(SaveThumbnailIcon(VideoPath, Thumbtype.List, string.Format("{0}_list.jpg", ImageName), Path.Combine(custompath, Thumbtype.List.ToString())));
            //SaveThumbnailIcon(VideoPath, Thumbtype.Symbol, string.Format("{0}_symbol.jpg", ImageName));

            ThumbMakeResponse thlist = new ThumbMakeResponse();
            thlist.Thumblist = list;

            return thlist;
        }


        public static System.Windows.Size GetOriginalSizeRatio(string VideoPath)
        {
            var shell = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(VideoPath);

            return new System.Windows.Size(shell.Thumbnail.Bitmap.Size.Width, shell.Thumbnail.Bitmap.Size.Height);

        }
        public static Size GetVideoSize(string videoFullPath)
        {
            if (File.Exists(videoFullPath))
            {
                ShellFile shellFile = ShellFile.FromFilePath(videoFullPath);
                int videoWidth = (int)shellFile.Properties.System.Video.FrameWidth.Value;
                int videoHeight = (int)shellFile.Properties.System.Video.FrameHeight.Value;
                return new Size(videoWidth, videoHeight);
            }
            return Size.Empty;
        }

        private static Bitmap MergeVideoImage(Bitmap SourceImage)
        {
            string thumbpath = "c:\\";

            if (ConfigurationManager.AppSettings["ThumbnailPath_Play"] != null)
                thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_Play"];

            if (!File.Exists(thumbpath))
                throw new InvalidOperationException("playbutton Image file Not Exists!");


            Image playbutton = Image.FromFile(thumbpath);

            //애초에 아래와 같이 크기 비교없이 소스이미지가 더 커야 하는데 만약 버튼보다 작을때 예외 막기 위해 처리. 
            //int outputImageWidth = firstImage.Width > playbutton.Width ? firstImage.Width : playbutton.Width;
            //int outputImageHeight = firstImage.Height + playbutton.Height + 1;
            int Width = SourceImage.Width;
            int Height = SourceImage.Height;


            var outputImage = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(outputImage);
            //graphics.CompositingMode = CompositingMode.SourceOver; 
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //플레이버튼이 정중앙이 아니라 왼쪽부터 붙음
            graphics.DrawImage(SourceImage, 0, 0);
            graphics.DrawImage(playbutton, 0, 0);

            //정중앙에 붙음
            //graphics.DrawImage(SourceImage, 0, 0); //사이즈 변환이 없기 때문에
            //graphics.DrawImage(playbutton, (outputImage.Width / 2) - (playbutton.Width / 2 + 5), (outputImage.Height / 2) - (playbutton.Height / 2 + 5));

            
            //graphics.DrawImage(SourceImage, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, Width, Height), GraphicsUnit.Pixel);
            //graphics.Save();

            //?
            //graphics.DrawImage(SourceImage, new Rectangle(new System.Drawing.Point(), SourceImage.Size),
            //     new Rectangle(new System.Drawing.Point(), SourceImage.Size), GraphicsUnit.Pixel);
            //graphics.DrawImage(playbutton, new Rectangle(new System.Drawing.Point(0, SourceImage.Height + 1), playbutton.Size),
            //    new Rectangle(new System.Drawing.Point(), playbutton.Size), GraphicsUnit.Pixel);

            return outputImage;
        }

        //public void SaveThumbnailFF(string VideoPath, Thumbtype thumbType, string ImageName)
        //{
        //    if (!File.Exists(VideoPath))
        //        throw new InvalidOperationException("Video Not Exists!");

        //    string thumbpath = "c:\\";

        //    if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
        //        thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];

        //    var thsize = new ThumbTypeClass(thumbType).ThumbSizeToString;


        //    Process prcFFMPEG = new Process();

        //    string strFFMPEGOut;
        //    ProcessStartInfo psiProcInfo = new ProcessStartInfo();
        //    TimeSpan estimatedTime = TimeSpan.MaxValue;

        //    StreamReader srFFMPEG;
        //    //ffmpeg  -itsoffset -1  -i "J:\mp3\너랑 나-아이유-PC-FullHD.mp4" -vcodec mjpeg -vframes 1 -an -f rawvideo -ss 00:00:15 -s 74x49 "thumtest.jpeg"
        //    //string strFFMPEGCmd = " -i \"" + srcFile + "\" -ar 44100 " + videoRateOption + videoSizeOption + " -y \"" + dstFile + "\"";
        //    string strFFMPEGCmd = " -itsoffset -1  -i " + "\"" + VideoPath + "\"" + " -vcodec mjpeg -vframes 1 -an -f rawvideo -ss 00:00:15 -s " + thsize + " \"" + Path.Combine(thumbpath, ImageName) + "\"";

        //    //psiProcInfo.FileName = Application.StartupPath + ((IntPtr.Size == 8) ? "\\x64" : "\\x86") + "\\ffmpeg.exe";
        //    psiProcInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
        //    psiProcInfo.Arguments = strFFMPEGCmd;
        //    psiProcInfo.UseShellExecute = false;
        //    psiProcInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    psiProcInfo.RedirectStandardError = true;
        //    psiProcInfo.RedirectStandardOutput = true;
        //    psiProcInfo.CreateNoWindow = true;

        //    prcFFMPEG.StartInfo = psiProcInfo;

        //    prcFFMPEG.Start();

        //    srFFMPEG = prcFFMPEG.StandardError;

        //    do
        //    {
        //        strFFMPEGOut = srFFMPEG.ReadLine();

        //        string duration = "Duration";
        //        if (strFFMPEGOut.TrimStart().IndexOf(duration) == 0)
        //        {
        //            try
        //            {
        //                string text = strFFMPEGOut.TrimStart().Substring(duration.Length + 2);
        //                int pos = text.IndexOf(",");
        //                string estimated = text.Substring(0, pos);

        //                estimatedTime = TimeSpan.Parse(estimated);
        //            }
        //            catch
        //            {
        //            }
        //        }

        //        if (estimatedTime != TimeSpan.MaxValue)
        //        {
        //            // 예측 시간이 나왔으면.
        //            string time = "time=";
        //            int startPos = strFFMPEGOut.IndexOf(time);
        //            if (startPos != -1)
        //            {
        //                string text = strFFMPEGOut.Substring(startPos + time.Length);
        //                int pos = text.IndexOf(" ");
        //                string current = text.Substring(0, pos);

        //                TimeSpan currentTime = TimeSpan.Parse(current);

        //                //int progresss = (int)(currentTime.TotalMilliseconds * 100 / estimatedTime.TotalMilliseconds);
        //            }

        //        }

        //        Console.WriteLine(strFFMPEGOut);
        //    } while (prcFFMPEG.HasExited == false);

        //}




        //시간이 실제 동영상 길이를 초과시 캡쳐가 안되지만 에러를 뱉지는 않는다.
        public static void SaveThumbnailFF(string VideoPath, Thumbtype thumbType, string ImageName)
        {
            string thumbTime = "00:00:10";
            if (ConfigurationManager.AppSettings["ThumbnailTimeOff"] != null)
                thumbTime = ConfigurationManager.AppSettings["ThumbnailTimeOff"];

            SaveThumbnailFF(VideoPath, thumbType, ImageName, thumbTime);
        }
        //시간 소요가 큰것이 (ffmpeg이 느리다기 보다는 별도 프로세스를 호출하는 문제인듯) 문제이며 에러를 제대로 전달받아서 서버에서 로깅하는등의 문제가 있음
        //기본 속성은 overwrite이며 사이즈는 shell 보다 작은게 장점이다
        public static void SaveThumbnailFF(string VideoPath, Thumbtype thumbType, string ImageName, string timeoff)
        {
            if (!File.Exists(VideoPath))
                throw new InvalidOperationException("Video Not Exists!");

            string thumbpath = "c:\\";

            if (ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()] != null)
                thumbpath = ConfigurationManager.AppSettings["ThumbnailPath_" + thumbType.ToString()];

            //기존 파일이 존재시 delete
            if (File.Exists(Path.Combine(thumbpath, ImageName)))
                File.Delete(Path.Combine(thumbpath, ImageName));

            //사이즈 비율을 제대로 가져오고자 한다면 아래와 같이 shell을 이용해서....
            //var thsize = new ThumbTypeClass(GetOriginalSizeRatio(VideoPath), thumbType).ThumbSizeToString;
            //기본 생성자로 갈땐 무조건 16:9 비율로 가져온다.
            var thsize = new ThumbTypeClass(thumbType).ThumbSizeToString;

            //ffmpeg  -itsoffset -1  -i "J:\mp3\너랑 나-아이유-PC-FullHD.mp4" -vcodec mjpeg -vframes 1 -an -f rawvideo -ss 00:00:15 -s 74x49 "thumtest.jpeg"
            //var cmd = "ffmpeg  -itsoffset -1  -i " + '"' + video + '"' + " -vcodec mjpeg -vframes 1 -an -f rawvideo -s 320x240 " + '"' + thumbnail + '"';
            var ffcmd = "ffmpeg  -itsoffset -1  -i " + "\"" + VideoPath + "\"" + " -vcodec mjpeg -vframes 1 -an -f rawvideo -ss " + timeoff + " -s " + thsize + " \"" + Path.Combine(thumbpath, ImageName) + "\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + ffcmd,
                //RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            var process = new Process
            {
                StartInfo = startInfo
            };


            process.Start();
            //아래같이 보단 pipe가 나은듯
            //while (!process.StandardOutput.EndOfStream)
            //{
            //    string line = process.StandardOutput.ReadLine();
            //    MessageBox.Show(line);
            //}
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(5000);
            //            process.WaitForExit();
            process.Close();
        }

        static Bitmap LoadImage(string path)
        {
            var ms = new MemoryStream(File.ReadAllBytes(path));
            return (Bitmap)Image.FromStream(ms);
        }


        //list 쪼개고 할려다가.. 걍 귀찮아서
        public class ThumbTypeClass
        {
            private System.Windows.Size _size;
            private string _StrSize;
            private int _height;
            private int _width;
            private int _RatioWidth;
            private Thumbtype _thmtype;

            //아래 home 사이즈는 임의로 16:9 사이즈로 만든것이다.
            public ThumbTypeClass(Thumbtype thtype)
            {

                switch (thtype)
                {
                    case Thumbtype.Home:
                        _size = new System.Windows.Size(267, 150);
                        _StrSize = string.Format("{0}x{1}", 267, 150);
                        _width = 267;
                        _height = 150;
                        break;

                    case Thumbtype.List:
                        _size = new System.Windows.Size(49, 74);
                        _StrSize = string.Format("{0}x{1}", 74, 49);
                        _width = 74;
                        _height = 49;
                        break;
                }


            }
            public ThumbTypeClass(System.Windows.Size orSize, Thumbtype thtype)
            {
                thumbtype = thtype;
                switch (thtype)
                {
                    case Thumbtype.Home:
                        //높이 150. 넓이는 가변
                        GetWidthFromHeight(orSize, 150);
                        _size = new System.Windows.Size(_RatioWidth, 150);
                        _StrSize = string.Format("{0}x{1}", _RatioWidth, 150);
                        _width = _RatioWidth;
                        _height = 150;
                        break;

                    case Thumbtype.List:
                        _size = new System.Windows.Size(49, 74);
                        _StrSize = string.Format("{0}x{1}", 74, 49);
                        _width = 74;
                        _height = 49;
                        break;

                    case Thumbtype.Symbol:
                        GetWidthFromHeight(orSize, 190);
                        _size = new System.Windows.Size(_RatioWidth, 190);
                        _StrSize = string.Format("{0}x{1}", _RatioWidth, 190);
                        _width = _RatioWidth;
                        _height = 190;

                        break;
                }
            }
            
            //높이에 따른 넓이 추출 - 150px
            public int GetWidthFromHeight(System.Windows.Size orSize, double height)
            {
                //orSize.Width : orSize.Height = x : height;
                double x = (orSize.Width * height) / orSize.Height;

                _RatioWidth = Convert.ToInt32(x);
                return _RatioWidth;
            }

            public Thumbtype thumbtype
            {
                get { return _thmtype; }
                set { _thmtype = value; }
            }
            public int Height
            {
                get { return _height; }
                set { _height = value; }
            }
            public int Width
            {
                get { return _width; }
                set { _width = value; }
            }

            public System.Windows.Size ThumbSize
            {
                get { return _size; }
                set { _size = value; }
            }

            public string ThumbSizeToString
            {
                get { return _StrSize; }
                set { _StrSize = value; }
            }

        }

        public class ThumbMakeResponse
        {
            //public ThumbTypeClass thumbtypeclass { get; set; }

            //public Thumbtype thtype { get; set; }

            public IEnumerable<ThumbTypeClass> Thumblist
            {
                get;
                set;
            }
        }

        //public class ThumbMakeResponses
        //{
        //    public IEnumerable<ThumbMakeResponse> Thumblist
        //    {
        //        get;
        //        set;
        //    }
        //}


    }
}
