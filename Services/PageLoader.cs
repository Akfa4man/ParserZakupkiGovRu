using AngleSharp.Dom;
using AngleSharp;
using System.Diagnostics.CodeAnalysis;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Services
{
    public class PageLoader: Interfaces.IPageLoader
    {
        public async Task<IDocument> LoadPageAsync([NotNull]string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url) ?? await context.OpenAsync(req => req.Content(url)) ?? throw new FormatException("The non-transformable format of the input string");
            return document;
        }
    }
}
