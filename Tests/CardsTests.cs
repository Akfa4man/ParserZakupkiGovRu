using ParserZakupkiGovRu_with_ASP_VER_1._0.Models;
using Xunit;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Tests
{
    public class CardsFunctionalTests()
    {
        [Fact]
        public async Task GetHashCodeComparisonCorrect()
        {
            var card1 = new Card("32413748777", "Поставка сыра", "ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ СТАЦИОНАРНОЕ УЧРЕЖДЕНИЕ СОЦИАЛЬНОГО ОБСЛУЖИВАНИЯ \"КЕМЕРОВСКИЙ ДОМ-ИНТЕРНАТ ДЛЯ ПРЕСТАРЕЛЫХ И ИНВАЛИДОВ\"", "571 330,00 ₽", new DateTime(2024, 06, 26), new DateTime(2024, 06, 26), new DateTime(2024, 07, 04), "Подача заявок");
            var card2 = new Card("32413748777", "Поставка сыра", "ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ СТАЦИОНАРНОЕ УЧРЕЖДЕНИЕ СОЦИАЛЬНОГО ОБСЛУЖИВАНИЯ \"КЕМЕРОВСКИЙ ДОМ-ИНТЕРНАТ ДЛЯ ПРЕСТАРЕЛЫХ И ИНВАЛИДОВ\"", "571 330,00 ₽", new DateTime(2024, 06, 26), new DateTime(2024, 06, 26), new DateTime(2024, 07, 04), "Подача заявок");
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode());
        }

        [Fact]
        public async Task GetHashCodeComparisonNotCorrect()
        {
            //3241374877
            var card1 = new Card("32413748777", "Поставка сыра", "ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ СТАЦИОНАРНОЕ УЧРЕЖДЕНИЕ СОЦИАЛЬНОГО ОБСЛУЖИВАНИЯ \"КЕМЕРОВСКИЙ ДОМ-ИНТЕРНАТ ДЛЯ ПРЕСТАРЕЛЫХ И ИНВАЛИДОВ\"", "571 330,00 ₽", new DateTime(2024, 06, 26), new DateTime(2024, 06, 26), new DateTime(2024, 07, 04), "Подача заявок");

            //32413748778
            var card2 = new Card("32413748778", "Поставка сыра", "ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ СТАЦИОНАРНОЕ УЧРЕЖДЕНИЕ СОЦИАЛЬНОГО ОБСЛУЖИВАНИЯ \"КЕМЕРОВСКИЙ ДОМ-ИНТЕРНАТ ДЛЯ ПРЕСТАРЕЛЫХ И ИНВАЛИДОВ\"", "571 330,00 ₽", new DateTime(2024, 06, 26), new DateTime(2024, 06, 26), new DateTime(2024, 07, 04), "Подача заявок");
            Assert.NotEqual(card1.GetHashCode(), card2.GetHashCode());
        }
    }

}
