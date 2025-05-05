
using System.IO;

namespace PortfolioManagerAPI.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ImagePathToImage(string fullPath)
        {
            return PathToByteArray(fullPath);
        }

        public static bool SaveImage(string fullPath, byte[] image)
        {
            try
            {
                File.WriteAllBytes(fullPath, image);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetImageExtension(byte[] image)
        {
            try
            {
                string extension = "";
                if (image.Length > 4)
                {
                    if (image[0] == 0xFF && image[1] == 0xD8)
                        extension = ".jpg";
                    else if (image[0] == 0x89 && image[1] == 0x50 && image[2] == 0x4E && image[3] == 0x47)
                        extension = ".png";
                    else if (image.Length > 100)
                    {
                        string header = System.Text.Encoding.UTF8.GetString(image[..100]).Trim();
                        if (header.StartsWith("<svg") || header.StartsWith("<?xml"))
                            extension = ".svg";
                    }
                }
                if (string.IsNullOrEmpty(extension))
                    throw new Exception();
                return extension;
            }
            catch { throw new Exception(); }
        }

        private static byte[] PathToByteArray(string path)
        {
            byte[] fileBytes;
            if (File.Exists(path))
            {
                fileBytes = File.ReadAllBytes(path);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {path}");
            }
            return fileBytes;
        }
    }
}
