using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
using Xunit;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Tests
{
    public class PageLoaderFunctionalTests
    {
        [Fact]//Проверка на корректную загрузку HTML-документ с заданного URL и содержании заголовока загруженной страницы ожидаемую строку "Example Domain"
        public async Task LoadPageAsync_ReturnsDocument_WhenUrlIsValid()
        {
            var pageLoader = new PageLoader();
            var validUrl = "https://example.com/";

            var document = await pageLoader.LoadPageAsync(validUrl);

            Assert.NotNull(document);
            Assert.Contains("Example Domain", document.Title);
        }

        [Fact]//Проверка, что страница содержит заданный элемент (h1 с текстом "Example Domain").
        public async Task LoadPageAsync_ReturnsDocumentWithSpecificElement_WhenUrlIsValid()
        {
            var pageLoader = new PageLoader();
            var validUrl = "https://example.com/";

            var document = await pageLoader.LoadPageAsync(validUrl);

            Assert.NotNull(document);
            var element = document.QuerySelector("h1");
            Assert.NotNull(element);
            Assert.Equal("Example Domain", element.TextContent.Trim());
        }

        [Fact]//Проверка, что на странице отсутствует элемент с классом .non-existent-class.
        public async Task LoadPageAsync_ReturnsDocumentWithoutSpecificElement_WhenUrlIsValid()
        {
            var pageLoader = new PageLoader();
            var validUrl = "https://example.com/";

            var document = await pageLoader.LoadPageAsync(validUrl);

            Assert.NotNull(document);
            var nonExistentElement = document.QuerySelector(".non-existent-class");
            Assert.Null(nonExistentElement);
        }

        [Fact]//Проверка, что на странице есть элемент с тегом title
        public async Task LoadPageAsync_ReturnsDocument_WhenUrlIsDataUri()
        {
            var pageLoader = new PageLoader();
            var dataUri = File.ReadAllText("../../../Tests/PATH3.html");

            var document = await pageLoader.LoadPageAsync(dataUri);

            Assert.NotNull(document);
            var titleElement = document.QuerySelector("title");
            Assert.NotNull(titleElement);
            Assert.Equal("Закупки", titleElement.TextContent.Trim());
        }
    }
}
