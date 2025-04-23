namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static byte[] pathToIcon(string path)
        {
            string fullPath = Path.Combine("Resources/PortfolioIcons/", path + ".svg");

            byte[] fileBytes;
            if (File.Exists(fullPath))
            {
                fileBytes = File.ReadAllBytes(fullPath);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {fullPath}");
            }
            return fileBytes;
        }
    }
}
