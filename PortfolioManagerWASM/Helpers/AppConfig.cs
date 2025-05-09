namespace PortfolioManagerWASM.Helpers
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; set; }

        public static string GetResourcePath(string resource)
        {
            return Configuration["ResourcesPaths:" + resource];
        }
    }

}
