using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Controllers;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
using static ParserZakupkiGovRu_with_ASP_VER_1._0.Controllers.MainController;
using System.Text;
using Moq;
using Xunit;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Tests
{
    public class MainControllerFunctionalTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;

        public MainControllerFunctionalTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
        }

        private MainController CreateController()
        {
            return new MainController(new PageLoader(), new PageParser(), _mockConfiguration.Object);
        }

        public string DecodeBase64Html(string base64Data)//Он пока не нужен, но может понадобится)
        {
            // Проверка на корректность префикса строки
            const string prefix = "data:text/html;base64,";
            if (!base64Data.StartsWith(prefix))
            {
                throw new ArgumentException("Invalid base64 data format");
            }

            // Удаление префикса и декодирование base64 данных
            var base64EncodedString = base64Data.Substring(prefix.Length);
            var htmlContent = Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedString));

            return htmlContent;
        }

        [Fact]
        public async Task Parse_ReturnsCorrectResult_PATH1()
        {
            // Чтение URL из файла
            string url = File.ReadAllText("../../../Tests/PATH1.html");
            //string url = File.ReadAllText("../../../Zakupki.html");

            // Настройка mock объектов
            _mockConfiguration.Setup(config => config["BaseUrl"]).Returns(url);

            // Создание тестового HTML-документа
            //var testHtmlContent = "<html><body><div id='test'>Test Content</div></body></html>"; // Ваше тестовое содержимое HTML

            // Использование AngleSharp для создания IDocument
            //var context = BrowsingContext.New(Configuration.Default);
            //var document = await context.OpenAsync(req => req.Content(testHtmlContent));

            //_mockPageLoader.Setup(loader => loader.LoadPageAsync(It.IsAny<string>()))
            //    .ReturnsAsync(document);

            //_mockPageParser.Setup(parser => parser.GetTotalPages(It.IsAny<IDocument>()))
            //    .Returns(1); // Предположим, что у нас всего 1 страница

            //_mockPageParser.Setup(parser => parser.ParseAndRecordToClassCard(It.IsAny<IDocument>(), It.IsAny<int>()))
            //    .Returns(new List<Card>
            //    {
            //    new Card("12345", "Test Purchase", "Test Customer", "1000",
            //        new DateTime(2023, 1, 1), new DateTime(2023, 1, 2),
            //        new DateTime(2023, 1, 3), "Open")
            //    });

            // Создание контроллера
            //var controller = CreateController();

            // Создание запроса
            //var parseRequest = new MainController.ParseRequest { Query = 856445, MaxCards = 10 };

            // Выполнение метода Parse
            var result = await CreateController().Parse(new ParseRequest { Query = "856445", MaxCards = 10 }) as OkObjectResult;

            // Проверка результата
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            // Преобразование результата в List<Card>
            var jsonResponse = result.Value as string;
            Assert.NotNull(jsonResponse);
            var cards = JsonConvert.DeserializeObject<List<Card>>(jsonResponse);

            Assert.Equal(10, cards.Count);

            Card[] expectedCards =
            [
                new Card("0860300000217000006", "Услуги органов охраны правопорядка", "МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"", "372 687,00 ₽", new DateTime(2017, 01, 23), new DateTime(2017, 01, 23), null, "Определение поставщика завершено"),
                new Card("0860300000217000003", "Вода питьевая", "МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"", "64 512,54 ₽", new DateTime(2017, 01, 23), new DateTime(2017, 01, 23), null, "Определение поставщика завершено"),
                new Card("31603856445", "Поставка товара согласно спецификации (опора переносная, мотопомпа, насос, панель потолочная, минимойка, кабель, хомут, дюбель гвоздь, выключатель)", "ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ГОРОДА МОСКВЫ \"ЖИЛИЩНИК РАЙОНА ТВЕРСКОЙ\"", "299 980,52 ₽", new DateTime(2016, 07, 05), new DateTime(2016, 07, 05), null, "Закупка завершена"),
                new Card("31502856445", "Поставка транспортных средств ISUZU ELF 3.5 NMR85H-416", "Открытое акционерное общество \"Учебно-опытный молочный завод\" Вологодской государственной молочнохозяйственной академии имени Н.В. Верещагина\"", "7 500 000,00 ₽", new DateTime(2015, 10, 15), new DateTime(2015, 10, 20), new DateTime(2015, 10, 26), "Закупка завершена"),
                new Card("31401856445", "на поставку \n\nмешков бумажных битумированных\n\nдля нужд ФКП «Завод имени Я.М. Свердлова»", "Федеральное казенное предприятие \"Завод имени Я.М.Свердлова\"", "1 191 000,00 ₽", new DateTime(2014, 12, 23), new DateTime(2014, 12, 23), new DateTime(2014, 12, 29), "Закупка завершена"),
                new Card("31400856445", "выполнение работ по реконструкции артезианской скважины при ОДП-356 в части усиления конструкций оголовка артезианской скважины.", "Санкт-Петербургское государственное унитарное предприятие \"Петербургский метрополитен \"", "3 477 037,41 ₽", new DateTime(2014, 01, 31), new DateTime(2014, 01, 31), new DateTime(2014, 02, 10), "Закупка завершена"),
                new Card("0860300000217000006", "Услуги органов охраны правопорядка", "МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"", "372 687,00 ₽", new DateTime(2017, 01, 23), new DateTime(2017, 01, 23), null, "Определение поставщика завершено"),
                new Card("0860300000217000003", "Вода питьевая", "МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"", "64 512,54 ₽", new DateTime(2017, 01, 23), new DateTime(2017, 01, 23), null, "Определение поставщика завершено"),
                new Card("31603856445", "Поставка товара согласно спецификации (опора переносная, мотопомпа, насос, панель потолочная, минимойка, кабель, хомут, дюбель гвоздь, выключатель)", "ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ГОРОДА МОСКВЫ \"ЖИЛИЩНИК РАЙОНА ТВЕРСКОЙ\"", "299 980,52 ₽", new DateTime(2016, 07, 05), new DateTime(2016, 07, 05), null, "Закупка завершена"),
                new Card("31502856445", "Поставка транспортных средств ISUZU ELF 3.5 NMR85H-416", "Открытое акционерное общество \"Учебно-опытный молочный завод\" Вологодской государственной молочнохозяйственной академии имени Н.В. Верещагина\"", "7 500 000,00 ₽", new DateTime(2015, 10, 15), new DateTime(2015, 10, 20), new DateTime(2015, 10, 26), "Закупка завершена"),
            ];

            //for (int i = 0; i < 10; i++) Assert.Equal(cards[i].Equals(expectedCards[i]), true);

            Assert.Equal(true, cards[0].Equals(expectedCards[0]));
            Assert.Equal(true, cards[1].Equals(expectedCards[1]));
            Assert.Equal(true, cards[2].Equals(expectedCards[2]));
            Assert.Equal(true, cards[3].Equals(expectedCards[3]));
            Assert.Equal(true, cards[4].Equals(expectedCards[4]));
            Assert.Equal(true, cards[5].Equals(expectedCards[5]));
            Assert.Equal(true, cards[6].Equals(expectedCards[6]));
            Assert.Equal(true, cards[7].Equals(expectedCards[7]));
            Assert.Equal(true, cards[8].Equals(expectedCards[8]));
            Assert.Equal(true, cards[9].Equals(expectedCards[9]));
        }

        [Fact]
        public async Task Parse_ReturnsCorrectResult_PATH2()
        {
            string url = File.ReadAllText("../../../Tests/PATH2.html");

            _mockConfiguration.Setup(config => config["BaseUrl"]).Returns(url);

            var result = await CreateController().Parse(new ParseRequest { Query = "0860300000217000", MaxCards = 10 }) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            // Преобразование результата в List<Card>
            var jsonResponse = result.Value as string;
            Assert.NotNull(jsonResponse);
            var cards = JsonConvert.DeserializeObject<List<Card>>(jsonResponse);

            Assert.Equal(6, cards.Count);

            Card[] expectedCards =
            [
                new Card("0860300000217000002","Газ горючий природный (газ естественный)","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","213 100,00 ₽",new DateTime(2017,01,31),new DateTime(2017,01,31),null,"Определение поставщика завершено"),
                new Card("0860300000217000010","газ горючий природный (газ естественный)","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","213 100,00 ₽",new DateTime(2017,01,30),new DateTime(2017,01,30),null,"Определение поставщика завершено"),
                new Card("0860300000217000008","Энергия тепловая, отпущенная котельными","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","412 665,00 ₽",new DateTime(2017,01,23),new DateTime(2017,01,23),null,"Определение поставщика завершено"),
                new Card("0860300000217000007","Энергия тепловая, отпущенная котельными","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","550 135,00 ₽",new DateTime(2017,01,23),new DateTime(2017,01,23),null,"Определение поставщика завершено"),
                new Card("0860300000217000006","Услуги органов охраны правопорядка","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","372 687,00 ₽",new DateTime(2017,01,23),new DateTime(2017,01,23),null,"Определение поставщика завершено"),
                new Card("0860300000217000003","Вода питьевая","МУНИЦИПАЛЬНОЕ  КАЗЕННОЕ  УЧРЕЖДЕНИЕ  \"АДМИНИСТРАТИВНО-ХОЗЯЙСТВЕННАЯ   СЛУЖБА  АДМИНИСТРАЦИИ ПУГАЧЕВСКОГО МУНИЦИПАЛЬНОГО РАЙОНА САРАТОВСКОЙ ОБЛАСТИ\"","64 512,54 ₽",new DateTime(2017,01,23),new DateTime(2017,01,23),null,"Определение поставщика завершено")
            ];

            Assert.Equal(true, cards[0].Equals(expectedCards[0]));
            Assert.Equal(true, cards[1].Equals(expectedCards[1]));
            Assert.Equal(true, cards[2].Equals(expectedCards[2]));
            Assert.Equal(true, cards[3].Equals(expectedCards[3]));
            Assert.Equal(true, cards[4].Equals(expectedCards[4]));
            Assert.Equal(true, cards[5].Equals(expectedCards[5]));
        }

        [Fact]
        public async Task Parse_ReturnsCorrectResult_PATH3()
        {
            string url = File.ReadAllText("../../../Tests/PATH3.html");

            _mockConfiguration.Setup(config => config["BaseUrl"]).Returns(url);

            var result = await CreateController().Parse(new ParseRequest { Query = "", MaxCards = 4 }) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            // Преобразование результата в List<Card>
            var jsonResponse = result.Value as string;
            Assert.NotNull(jsonResponse);
            var cards = JsonConvert.DeserializeObject<List<Card>>(jsonResponse);

            Assert.Equal(4, cards.Count);

            Card[] expectedCards =
            [
                new Card("32413748793","Выполнение строительно-монтажных работ по строительству газопроводов в Новосибирской области","ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ \" НОВОСИБИРСКОБЛГАЗ\"","2 983 185,63 ₽",new DateTime(2024,06,26),new DateTime(2024,06,27),new DateTime(2024,07,05),"Подача заявок"),
                new Card("32413748632","Поставка компримированного природного газа для автомобильного транспорта","МУНИЦИПАЛЬНОЕ УНИТАРНОЕ ПРЕДПРИЯТИЕ \"ЯКУТСКАЯ ПАССАЖИРСКАЯ АВТОТРАНСПОРТНАЯ КОМПАНИЯ\" ГОРОДСКОГО ОКРУГА \"ГОРОД ЯКУТСК\"","10 000 000,00 ₽",new DateTime(2024,06,26),new DateTime(2024,06,27),new DateTime(2024,07,04),"Подача заявок"),
                new Card("32413748779","Выполнение строительно-монтажных работ по строительству газопроводов в Новосибирской области","ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ \" НОВОСИБИРСКОБЛГАЗ\"","914 752,19 ₽",new DateTime(2024,06,26),new DateTime(2024,06,26),new DateTime(2024,07,04),"Подача заявок"),
                new Card("32413748777","Поставка сыра","ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ СТАЦИОНАРНОЕ УЧРЕЖДЕНИЕ СОЦИАЛЬНОГО ОБСЛУЖИВАНИЯ \"КЕМЕРОВСКИЙ ДОМ-ИНТЕРНАТ ДЛЯ ПРЕСТАРЕЛЫХ И ИНВАЛИДОВ\"","571 330,00 ₽",new DateTime(2024,06,26),new DateTime(2024,06,26),new DateTime(2024,07,04),"Подача заявок")
            ];

            Assert.Equal(true, cards[0].Equals(expectedCards[0]));
            Assert.Equal(true, cards[1].Equals(expectedCards[1]));
            Assert.Equal(true, cards[2].Equals(expectedCards[2]));
            Assert.Equal(true, cards[3].Equals(expectedCards[3]));
        }
    }

}
