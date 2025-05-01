
using System.IO;

namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static byte[] PortfolioIconPathToIcon(string fileName)
        {
            string fullPath = Path.Combine("Resources/PortfolioIcons/", fileName);
            return PathToByteArray(fullPath);
        }
        public static byte[] AssetIconPathToIcon(string fileName)
        {
            string fullPath = Path.Combine("Resources/AssetIcons/", fileName);
            return PathToByteArray(fullPath);
        }
        public static byte[] UserAvatarPathToAvatar(string fileName)
        {
            var fullPath = Path.Combine("Resources/UserAvatars/", fileName);

            return PathToByteArray(fullPath);
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
