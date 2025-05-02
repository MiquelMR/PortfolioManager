using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Runtime.Intrinsics.Arm;

namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static string GetBase64String(byte[] image)
        {
            string extension = "";
            if (image.Length > 4)
            {
                if (image[0] == 0xFF && image[1] == 0xD8)
                    extension = "jpg";
                else if (image[0] == 0x89 && image[1] == 0x50 && image[2] == 0x4E && image[3] == 0x47)
                    extension = "png";
                else if (image.Length > 100)
                {
                    string header = System.Text.Encoding.UTF8.GetString(image[..100]).Trim();
                    if (header.StartsWith("<svg") || header.StartsWith("<?xml"))
                        extension = "svg+xml";
                }
            }

            if (string.IsNullOrEmpty(extension))
                extension = "octet-stream";
            return $"data:image/{extension};base64,{Convert.ToBase64String(image)}";
        }
    }
}
