namespace DotNetECoremmerce.ProductCatalogue.API.Configuration
{
    public class AppSettings
    {
        public static AppSettings Current;

        public AppSettings()
        {
            Current = this;
        }

        public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
        public ApiSettings ApiSettings { get; set; } = new ApiSettings();
    }
}