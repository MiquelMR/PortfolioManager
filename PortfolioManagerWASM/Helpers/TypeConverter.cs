namespace PortfolioManagerAPI.Helpers
{
    public static class TypeConverter
    {
        public static string GetBase64String(byte[] icon)
        {
            return $"data:image/svg+xml;base64,{Convert.ToBase64String(icon)}";
        }
    }
}
