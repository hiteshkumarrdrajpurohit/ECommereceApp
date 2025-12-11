using EcommerceApp.Enum;
using System.Collections.Generic;

namespace EcommerceApp.Model
{
    public class Order : Base
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int ShippingAddressId { get; set; }
        public Address? ShippingAddress { get; set; }
        public int? TransactionId { get; set; }

        public Transaction? Transaction { get; set; }
    }
}