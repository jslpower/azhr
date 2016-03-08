using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
namespace EyouSoft.Common.Function
{
	/// <summary>
	/// ��������ͼ
	/// </summary>
	public class Thumbnail
	{
		/// <summary>
		/// 
		/// </summary>
		public Thumbnail(){}
		/// <summary>
		/// ��������ͼ
		/// </summary>
		/// <param name="originalImagePath">Դͼ·��������·����</param>
		/// <param name="thumbnailPath">����ͼ·��������·����</param>
		/// <param name="width">����ͼ���</param>
		/// <param name="height">����ͼ�߶�</param>
		/// <param name="mode">��������ͼ�ķ�ʽ</param>    
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
            MakeThumbnail(System.Drawing.Image.FromFile(originalImagePath), thumbnailPath, width, height, mode);
		}

        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImage">Դͼ</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ</param>    
        public static void MakeThumbnail(System.IO.Stream originalImage, string thumbnailPath, int width, int height, string mode)
        {
            MakeThumbnail(System.Drawing.Image.FromStream(originalImage), thumbnailPath, width, height, mode);
        }

        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImage">Դͼ</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ</param>    
        public static void MakeThumbnail(System.Drawing.Image originalImage, string thumbnailPath, int width, int height, string mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://ָ���߿����ţ����ܱ��Σ�                
                    break;
                case "W"://ָ�����߰�����                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://ָ���߿�ü��������Σ�                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //�½�һ��bmpͼƬ
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch { }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// ��ָ���ķֱ���ѹ��ͼƬ������ָ����·�����档
        /// ע�����Դͼ�ķֱ���С��ָ���ķֱ��ʣ��򲻽�������
        /// </summary>
        /// <param name="stream">Դͼ</param>
        /// <param name="desImagePath">ѹ�����ͼƬ�ı���·��������·����</param>
        /// <param name="width">ѹ����ͼƬ�Ŀ��</param>
        /// <param name="height">ѹ����ͼƬ�ĸ߶�</param>
        public static void CompressionImage(System.IO.Stream stream, string desImagePath, int width, int height)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(stream);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (ow < towidth)
            {
                towidth = ow;
            }
            if (oh < toheight)
            {
                toheight = oh;
            }


            //�½�һ��bmpͼƬ
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight, PixelFormat.Format32bppArgb);

            //�½�һ������
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            try
            {
                //��jpeg��ʽ��������ͼ
                FileInfo f = new FileInfo(desImagePath);
                if (!Directory.Exists(f.DirectoryName))
                {
                    Directory.CreateDirectory(f.DirectoryName);
                }
                
                //bitmap.Save(desImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                CompressAsJPG((Bitmap)bitmap, desImagePath, 80);
            }
            catch { }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo ici in CodecInfo)
            {

                if (ici.MimeType == mimeType) return ici;

            }

            return null;

        }

        private static Bitmap GetBitmapFromStream(Stream inputStream)
        {

            Bitmap bitmap = new Bitmap(inputStream);

            return bitmap;

        }

        public static void CompressAsJPG(Bitmap bmp, string saveFilePath, int quality)
        {

            EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality); ;

            EncoderParameters ps = new EncoderParameters(1);

            ps.Param[0] = p;

            bmp.Save(saveFilePath, GetImageCodecInfo("image/jpeg"), ps);

            bmp.Dispose();

        }
	}
}
