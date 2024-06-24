namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Models
{
    public class Card
    {
        public readonly string Number;

        public readonly string ObjectOfPurchase;

        public readonly string Customer;

        public readonly string StartingPrice;


        public readonly DateTime? PostingDate;

        public readonly DateTime? UpdateDate;

        public readonly DateTime? ApplicationDeadline;

        public readonly string Status;

        public Card(string number,
            string objectOfPurchase,
            string customer,
            string startingPrice,
            DateTime? postingDate,
            DateTime? updateDate,
            DateTime? applicationDeadline,
            string status)
        {
            if (!IsCorrectNumber(number))
                throw new ArgumentException("The number must be a sequence of digits");

            Number = number;
            ObjectOfPurchase = objectOfPurchase;
            Customer = customer;
            StartingPrice = startingPrice;
            PostingDate = postingDate;
            UpdateDate = updateDate;
            ApplicationDeadline = applicationDeadline;
            Status = status;
        }

        private bool IsCorrectNumber(string number) =>
            number.All(ch => char.IsDigit(ch));
    }
}
