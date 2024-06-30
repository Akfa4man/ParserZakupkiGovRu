using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ParserZakupkiGovRu_with_ASP_VER_1._0.Models
{
    public class Card
    {
        [Required(ErrorMessage ="У заказа должен быть номер!")]
        //[RegularExpression(@"^\d{1,20}$", ErrorMessage = "Номер заказа должен содержать только цифры и не превышать более 20 цифр!")]
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
            Number = number;
            ObjectOfPurchase = objectOfPurchase;
            Customer = customer;
            StartingPrice = startingPrice;
            PostingDate = postingDate;
            UpdateDate = updateDate;
            ApplicationDeadline = applicationDeadline;
            Status = status;
        }

        public override bool Equals(object? obj)
        {
            if(obj==null || !(obj is Card)) return false;
            Card card = obj as Card;
            return Number==card.Number &&
                ObjectOfPurchase==card.ObjectOfPurchase &&
                Customer==card.Customer &&
                StartingPrice==card.StartingPrice &&
                PostingDate==card.PostingDate &&
                UpdateDate==card.UpdateDate &&
                ApplicationDeadline==card.ApplicationDeadline &&
                Status==card.Status;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Number.GetHashCode();
            hash = hash * 31 + (ObjectOfPurchase?.GetHashCode() ?? 0);
            hash = hash * 31 + (Customer?.GetHashCode() ?? 0);
            hash = hash * 31 + (StartingPrice?.GetHashCode() ?? 0);
            hash = hash * 31 + (PostingDate?.GetHashCode() ?? 0);
            hash = hash * 31 + (UpdateDate?.GetHashCode() ?? 0);
            hash = hash * 31 + (ApplicationDeadline?.GetHashCode() ?? 0);
            hash = hash * 31 + (Status?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
