using AngleSharp.Dom;
using AngleSharp;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Services
{
    public class PageLoader
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public PageLoader(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDocument> LoadPageAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            return document;
        }
    }
}
