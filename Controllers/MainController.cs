//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;

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


//        /// <summary>
//        /// Parses the given query and returns the parsed cards.
//        /// </summary>
//        /// <param name="request">Parse request containing query and max cards.</param>
//        /// <returns>Parsed cards as JSON string.</returns>
//        [HttpPost("parse/{query}/{maxCards}")]
//        public async Task<IActionResult> Parse([FromRoute] int query, [FromRoute] int? maxCards)
//        {
//            if (maxCards < 0) throw new ArgumentOutOfRangeException("The value should not be negative");

//            int maxCardValue = maxCards ?? int.MaxValue;

//            string baseUrl = _configuration["BaseUrl"];
//            string initialUrl = baseUrl.Replace("{query}", query.ToString()).Replace("{page}", "1");
//            var initialDocument = await _pageLoader.LoadPageAsync(initialUrl);

//            int totalPages = _pageParser.GetTotalPages(initialDocument);
//            var allCards = new List<Card>();

//            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
//            {
//                if (allCards.Count >= maxCardValue) break;

//                string url = baseUrl.Replace("{query}", query.ToString()).Replace("{page}", pageNumber.ToString());
//                var document = await _pageLoader.LoadPageAsync(url);
//                var cards = _pageParser.ParseAndRecordToClassCard(document, maxCardValue - allCards.Count);
//                allCards.AddRange(cards);
//            }

//            return Ok(JsonConvert.SerializeObject(allCards, Formatting.Indented));
//        }
//    }
//}










//Правильная версия ВОЗМОЖНО
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
using System.ComponentModel.DataAnnotations;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Controllers
{
    /// <summary>
    /// MainController class handles parsing requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly Interfaces.IPageLoader _pageLoader;
        private readonly Interfaces.IPageParser _pageParser;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for MainController.
        /// </summary>
        /// <param name="pageLoader">PageLoader service.</param>
        /// <param name="pageParser">PageParser service.</param>
        /// <param name="configuration">Configuration settings.</param>
        public MainController(Interfaces.IPageLoader pageLoader, Interfaces.IPageParser pageParser, IConfiguration configuration)
        {
            _pageLoader = pageLoader;
            _pageParser = pageParser;
            _configuration = configuration;
        }

        public class ParseRequest
        {
            [Required(ErrorMessage = "Укажите номер заказа!")]
            [StringLength(20, MinimumLength = 1, ErrorMessage = "Номер заказа не должен быть пустым/превышать 20 символов!")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Нельзя вводить ничего кроме цифр!")]
            public string Query { get; set; }

            [Required(ErrorMessage = "Укажите ограничение по количеству карт!")]
            [Range(0, int.MaxValue, ErrorMessage = "Количество карт должно быть неотрицательным и не должно превышать 2 147 483 647!")]
            public int MaxCards { get; set; }
        }

        /// <summary>
        /// Parses the given query and returns the parsed cards.
        /// </summary>
        /// <param name="request">Parse request containing query and max cards.</param>
        /// <returns>Parsed cards as JSON string.</returns>
        [HttpPost("parse")]
        public async Task<IActionResult> Parse([FromBody] ParseRequest parseRequest)
        {
            string baseUrl = _configuration["BaseUrl"];
            string initialUrl = baseUrl.Replace("{query}", parseRequest.Query).Replace("{page}", "1");
            var initialDocument = await _pageLoader.LoadPageAsync(initialUrl);

            int totalPages = _pageParser.GetTotalPages(initialDocument);
            var allCards = new List<Card>();

            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                if (allCards.Count >= parseRequest.MaxCards) break;

                string url = baseUrl.Replace("{query}", parseRequest.Query).Replace("{page}", pageNumber.ToString());
                var document = await _pageLoader.LoadPageAsync(url);
                var cards = _pageParser.ParseAndRecordToClassCard(document, parseRequest.MaxCards - allCards.Count);
                allCards.AddRange(cards);
            }

            return Ok(JsonConvert.SerializeObject(allCards, Formatting.Indented));
        }
    }
}