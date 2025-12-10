using EcommerceApp.Enum;
using System.Text.Json.Serialization;

namespace EcommerceApp.Model
{
    public class Transaction :Base
    {
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        public string RazorPageTransactionID { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public PaymentMethod Method { get; set; } = PaymentMethod.CreditCard;

        public string? Remarks { get; set; }
    }
}
