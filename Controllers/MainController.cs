using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
using System.Text;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Controllers
{
    /// <summary>
    /// MainController class handles parsing requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly PageLoader _pageLoader;
        private readonly PageParser _pageParser;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for MainController.
        /// </summary>
        /// <param name="pageLoader">PageLoader service.</param>
        /// <param name="pageParser">PageParser service.</param>
        /// <param name="configuration">Configuration settings.</param>
        public MainController(PageLoader pageLoader, PageParser pageParser, IConfiguration configuration)
        {
            _pageLoader = pageLoader;
            _pageParser = pageParser;
            _configuration = configuration;
        }


        /// <summary>
        /// Parses the given query and returns the parsed cards.
        /// </summary>
        /// <param name="request">Parse request containing query and max cards.</param>
        /// <returns>Parsed cards as JSON string.</returns>
        [HttpPost("parse/{query}/{maxCards}")]
        public async Task<IActionResult> Parse([FromRoute] int query, [FromRoute] int? maxCards)
        {
            if (maxCards < 0) throw new ArgumentOutOfRangeException("The value should not be negative");

            int maxCardValue = maxCards ?? int.MaxValue;

            string baseUrl = _configuration["BaseUrl"];
            string initialUrl = baseUrl.Replace("{query}", query.ToString()).Replace("{page}", "1");
            var initialDocument = await _pageLoader.LoadPageAsync(initialUrl);

            int totalPages = _pageParser.GetTotalPages(initialDocument);
            var allCards = new List<Card>();

            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                if (allCards.Count >= maxCardValue) break;

                string url = baseUrl.Replace("{query}", query.ToString()).Replace("{page}", pageNumber.ToString());
                var document = await _pageLoader.LoadPageAsync(url);
                var cards = _pageParser.ParseAndRecordToClassCard(document, maxCardValue - allCards.Count);
                allCards.AddRange(cards);
            }

            string jsonString = SerializeCardsToJson(allCards);

            return Ok(jsonString);
        }
        public static string SerializeCardsToJson(List<Card> cards)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Card card in cards)
            {
                string str = JsonConvert.SerializeObject(card, Formatting.Indented);
                stringBuilder.AppendLine(str);
            }

            return stringBuilder.ToString();
        }
    }
}










//Правильная версия ВОЗМОЖНО
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
//using System.Text;

//namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Controllers
//{
//    /// <summary>
//    /// MainController class handles parsing requests.
//    /// </summary>
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MainController : ControllerBase
//    {
//        private readonly PageLoader _pageLoader;
//        private readonly PageParser _pageParser;
//        private readonly IConfiguration _configuration;

//        /// <summary>
//        /// Constructor for MainController.
//        /// </summary>
//        /// <param name="pageLoader">PageLoader service.</param>
//        /// <param name="pageParser">PageParser service.</param>
//        /// <param name="configuration">Configuration settings.</param>
//        public MainController(PageLoader pageLoader, PageParser pageParser, IConfiguration configuration)
//        {
//            _pageLoader = pageLoader;
//            _pageParser = pageParser;
//            _configuration = configuration;
//        }

//        public class ParseRequest
//        {
//            public int Query { get; set; }
//            private int? maxCards;
//            public int MaxCards
//            {
//                get { return maxCards ?? int.MaxValue; }
//                set
//                {
//                    if (value < 0) throw new ArgumentOutOfRangeException("The value should not be negative");
//                    maxCards = value;
//                }
//            }
//        }

//        /// <summary>
//        /// Parses the given query and returns the parsed cards.
//        /// </summary>
//        /// <param name="request">Parse request containing query and max cards.</param>
//        /// <returns>Parsed cards as JSON string.</returns>
//        [HttpPost("parse")]
//        public async Task<IActionResult> Parse([FromBody] ParseRequest parseRequest)
//        {
//            string baseUrl = _configuration["BaseUrl"];
//            string initialUrl = baseUrl.Replace("{query}", parseRequest.Query.ToString()).Replace("{page}", "1");
//            var initialDocument = await _pageLoader.LoadPageAsync(initialUrl);

//            int totalPages = _pageParser.GetTotalPages(initialDocument);
//            var allCards = new List<Card>();

//            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
//            {
//                if (allCards.Count >= parseRequest.MaxCards) break;

//                string url = baseUrl.Replace("{query}", parseRequest.Query.ToString()).Replace("{page}", pageNumber.ToString());
//                var document = await _pageLoader.LoadPageAsync(url);
//                var cards = _pageParser.ParseAndRecordToClassCard(document, parseRequest.MaxCards - allCards.Count);
//                allCards.AddRange(cards);
//            }

//            string jsonString = SerializeCardsToJson(allCards);

//            return Ok(jsonString);
//        }
//        public static string SerializeCardsToJson(List<Card> cards)
//        {
//            StringBuilder stringBuilder = new StringBuilder();

//            foreach (Card card in cards)
//            {
//                string str = JsonConvert.SerializeObject(card, Formatting.Indented);
//                stringBuilder.AppendLine(str);
//            }

//            return stringBuilder.ToString();
//        }
//    }
//}