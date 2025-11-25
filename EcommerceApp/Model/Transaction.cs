using EcommerceApp.Enum;

namespace EcommerceApp.Model
{
    public class Transaction :Base
    {
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public PaymentMethod Method { get; set; } = PaymentMethod.CreditCard;

        public string? Remarks { get; set; }
    }
}
