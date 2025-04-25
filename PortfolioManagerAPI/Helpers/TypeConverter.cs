namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static byte[] portfolioIconPathToIcon(string path)
        {
            string fullPath = Path.Combine("Resources/PortfolioIcons/", path + ".svg");
            return PathToByteArray(fullPath);
        }
        public static byte[] assetIconPathToIcon(string path)
        {
            string fullPath = Path.Combine("Resources/AssetIcons/", path + ".svg");
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
