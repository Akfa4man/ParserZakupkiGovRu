using AngleSharp.Dom;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Services
{
    public class PageParser: Interfaces.IPageParser
    {
        public int GetTotalPages([NotNull] IDocument document)
        {
            //if (document == null) throw new ArgumentNullException(nameof(document));

            var pageElements = document.QuerySelectorAll(".page");
            int totalPages = 1;

            if (pageElements.Length > 0)
            {
                var lastPageElement = pageElements.LastOrDefault(e => e.QuerySelector("a") != null);
                if (lastPageElement != null)
                {
                    int.TryParse(lastPageElement.TextContent.Trim(), out totalPages);
                }
            }

            return totalPages;
        }

        public List<Card> ParseAndRecordToClassCard([NotNull] IDocument document, int maxCards)
        {
            //if (document == null) throw new ArgumentNullException(nameof(document));

            var cards = new List<Card>();
            var cardElements = document.QuerySelectorAll(".search-registry-entry-block");

            foreach (var cardElement in cardElements)
            {
                if (cards.Count >= maxCards) break;

                var numberElement = cardElement.QuerySelector("div.registry-entry__header-mid__number a");
                var nameElement = cardElement.QuerySelector("div.registry-entry__body-value");
                var organizationElement = cardElement.QuerySelector("div.registry-entry__body-href a");
                var priceElement = cardElement.QuerySelector("div.price-block__value");
                var statusElement = cardElement.QuerySelector("div.registry-entry__header-mid__title");

                var placementDateElement = cardElement.QuerySelector("div.data-block .col-6:nth-of-type(1) .data-block__value");
                var updateDateElement = cardElement.QuerySelector("div.data-block .col-6:nth-of-type(2) .data-block__value");
                var applicationDeadlineElement = cardElement.QuerySelector("div.data-block .data-block__value:nth-of-type(3)");

                string number = numberElement?.TextContent.Trim().Remove(0,2);
                string name = nameElement?.TextContent.Trim();
                string organization = organizationElement?.TextContent.Trim();
                string price = priceElement?.TextContent.Trim();
                string status = statusElement?.TextContent.Trim();
                DateTime? placementDate = ParseDate(placementDateElement?.TextContent.Trim());
                DateTime? updateDate = ParseDate(updateDateElement?.TextContent.Trim());
                DateTime? applicationDeadline = ParseDate(applicationDeadlineElement?.TextContent.Trim());

               
                var card = new Card(
                    number,
                    name,
                    organization,
                    price,
                    placementDate,
                    updateDate,
                    applicationDeadline,
                    status
                    );

                cards.Add(card);
            }

            return cards;
        }

        private DateTime? ParseDate(string date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }
    }
}
