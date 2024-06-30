using AngleSharp.Dom;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Interfaces
{
    public interface IPageParser
    {
        public int GetTotalPages(IDocument document);
        public List<Card> ParseAndRecordToClassCard(IDocument document, int maxCards);
    }
}
