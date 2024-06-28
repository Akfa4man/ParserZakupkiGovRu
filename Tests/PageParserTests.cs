using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
using Xunit;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Tests
{
    public class PageParserFunctionalTests()
    {
        [Fact]
        public async Task GetTotalPages_PATH1()
        {
            var dataUri = File.ReadAllText("../../../Tests/PATH1.html");
            var pageLoader = new PageLoader();

            var document = await pageLoader.LoadPageAsync(dataUri);

            Assert.NotNull(document);

            var totalpage = new PageParser().GetTotalPages(document);

            Assert.Equal(totalpage, 10);
        }

        [Fact]
        public async Task GetTotalPages_PATH2()
        {
            var dataUri = File.ReadAllText("../../../Tests/PATH2.html");
            var pageLoader = new PageLoader();

            var document = await pageLoader.LoadPageAsync(dataUri);

            Assert.NotNull(document);

            var totalpage = new PageParser().GetTotalPages(document);

            Assert.Equal(totalpage, 1);
        }

        [Fact]
        public async Task GetTotalPages_PATH3()
        {
            var dataUri = File.ReadAllText("../../../Tests/PATH3.html");
            var pageLoader = new PageLoader();

            var document = await pageLoader.LoadPageAsync(dataUri);

            Assert.NotNull(document);

            var totalpage = new PageParser().GetTotalPages(document);

            Assert.Equal(totalpage, 100);
        }

        [Fact]
        public void GetTotalPages_WhereIDocumentEqualNull()
        {
            var parser = new PageParser();

            var exception = Assert.Throws<ArgumentNullException>(() => parser.GetTotalPages(null));
            Assert.Equal("document", exception.ParamName);
        }
    }
}
