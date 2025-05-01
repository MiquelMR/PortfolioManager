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
                // Check JPG (Magic Number: FF D8)
                if (image[0] == 0xFF && image[1] == 0xD8)
                    extension = "jpg";

                // Check PNG (Magic Number: 89 50 4E 47)
                if (image[0] == 0x89 && image[1] == 0x50 && image[2] == 0x4E && image[3] == 0x47)
                    extension = "png";

                // Check SVG (XML-based, not binary)
                if (System.Text.Encoding.UTF8.GetString(image).StartsWith("<?xml") && image.Contains((byte)'<'))
                    extension = "svg+xml";
            }
            return $"data:image/{extension};base64,{Convert.ToBase64String(image)}";
        }
    }
}
