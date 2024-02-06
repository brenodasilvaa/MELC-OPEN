using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELC.Core.Commons.FileHelper
{
    public static class FileHelper
    {
        public static async Task<string> CopyStreamToFileAsync(string pathDesenho, Stream file, string filePath)
        {
            if (!Directory.Exists(pathDesenho))
                Directory.CreateDirectory(pathDesenho);

            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);
                var dateTimeName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                var fileNameFormatted = $"({dateTimeName})-{fileName}";

                filePath = Path.Combine(pathDesenho, fileNameFormatted);
            }

            using (var fileStream = File.Create(filePath))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        public static string GetPathArquivos(string diretorio = null)
        {
            if (diretorio != null)
                return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "MELC", diretorio);

            return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "MELC");
        }

        public static string ConvertPdfToBase64(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);

            return Convert.ToBase64String(bytes);
        }

        public static string ChangeImageDpi(string imagePath, float xDpi, float yDpi)
        {
            var folderFile = Path.GetDirectoryName(imagePath);
            var imageReducedPath = Path.Combine(folderFile, $"{Path.GetRandomFileName()}.jpeg");

            using (Bitmap bitmap = (Bitmap)Image.FromFile(imagePath))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {
                    newBitmap.SetResolution(xDpi, yDpi);
                    newBitmap.Save(imageReducedPath, ImageFormat.Jpeg);
                }
            }

            return imageReducedPath;
        }
    }
}
