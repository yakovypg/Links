using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Links.Data.Imaging
{
    internal static class ImageTransformer
    {
        public static Size Fit(Size sourceSize, Size frame)
        {
            if (sourceSize.Width <= frame.Width && sourceSize.Height <= frame.Height)
                return sourceSize;

            double coefH = frame.Height / (double)sourceSize.Height;
            double coefW = frame.Width / (double)sourceSize.Width;

            int width, height;

            if (coefW >= coefH)
            {
                height = (int)(sourceSize.Height * coefH);
                width = (int)(sourceSize.Width * coefH);
            }
            else
            {
                height = (int)(sourceSize.Height * coefW);
                width = (int)(sourceSize.Width * coefW);
            }

            return new Size(width, height);
        }

        public static BitmapImage ToBitmapImage(Image image)
        {
            var bmpImage = new BitmapImage();

            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                bmpImage.BeginInit();
                bmpImage.StreamSource = memory;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();
                bmpImage.Freeze();
            }

            return bmpImage;
        }

        public static BitmapImage ToBitmapImage(byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes)
            {
                Position = 0
            };

            var bmpImage = new BitmapImage();

            bmpImage.BeginInit();
            bmpImage.StreamSource = memoryStream;
            bmpImage.EndInit();

            return bmpImage;
        }

        public static BitmapImage GetBitmapImage(string path, Size size)
        {
            BitmapImage output = null;

            using (var sourceImage = new Bitmap(path))
            {
                Size fittedSize = Fit(sourceImage.Size, size);

                using (var fittedImage = new Bitmap(sourceImage, fittedSize))
                {
                    output = ToBitmapImage(fittedImage);
                }
            }

            return output;
        }

        public static BitmapImage GetBitmapImage(string path)
        {
            var bmpImage = new BitmapImage();

            bmpImage.BeginInit();
            bmpImage.CacheOption = BitmapCacheOption.OnLoad;
            bmpImage.UriSource = new Uri(path);
            bmpImage.EndInit();

            return bmpImage;
        }

        public static bool TryGetBitmapImage(string path, out BitmapImage bmpImage)
        {
            try
            {
                bmpImage = GetBitmapImage(path);
                return true;
            }
            catch
            {
                bmpImage = null;
                return false;
            }
        }

        public static bool TryGetBitmapImage(string path, Size size, out BitmapImage bmpImage)
        {
            try
            {
                bmpImage = GetBitmapImage(path, size);
                return true;
            }
            catch
            {
                bmpImage = null;
                return false;
            }
        }

        public static byte[] GetBytes(BitmapImage bmpImage)
        {
            byte[] buffer = null;

            using (var memoryStream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmpImage));
                encoder.Save(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);
                buffer = memoryStream.ToArray();
            }

            return buffer;
        }
    }
}