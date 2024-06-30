using AngleSharp.Dom;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Interfaces
{
    public interface IPageLoader
    {
        public Task<IDocument> LoadPageAsync(string url);
    }
}
