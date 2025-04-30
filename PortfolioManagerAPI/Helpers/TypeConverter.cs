
using System.IO;

namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static byte[] PortfolioIconPathToIcon(string path)
        {
            string fullPath = Path.Combine("Resources/PortfolioIcons/", path + ".svg");
            return PathToByteArray(fullPath);
        }
        public static byte[] AssetIconPathToIcon(string path)
        {
            string fullPath = Path.Combine("Resources/AssetIcons/", path + ".svg");
            return PathToByteArray(fullPath);
        }
        public static byte[] UserAvatarPathToAvatar(string path)
        {
            string fullPath = Path.Combine("Resources/UserAvatars/", path + ".svg");
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
