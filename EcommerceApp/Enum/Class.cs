namespace EcommerceApp.Enum
{
   public enum OrderStatus
   {
       Pending,
       Processing,
       Shipped,
       Delivered,
       Cancelled,
       Returned
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        PayPal,
        BankTransfer,
        CashOnDelivery
    }

    public enum TransactionStatus
    {
        Success,
        Failed,
        Pending,
        Refunded
    }

    public enum ItemCategory
    {
        Electronics,
        Clothing,
        HomeAppliances,
        Books,
        Toys,
        Sports,
        Beauty,
        General
    }

    public enum UserRole
    {
        Customer,
        Admin,
        Seller,
        Guest
    }

    public enum AddressType
    {
        Shipping,
        Billing,
        Both
    }

    public enum StockStatus
    {
        InStock,
        OutOfStock,
        PreOrder,
        Discontinued
    }

    public enum DiscountType
    {
        Percentage,
        FixedAmount,
        BuyOneGetOne
    }

    public enum NotificationType
    {
        Email,
        SMS,
        PushNotification
    }

    public enum ReviewRating
    {
        OneStar = 1,
        TwoStars,
        ThreeStars,
        FourStars,
        FiveStars
    }

    public enum ShipmentMethod
    {
        Standard,
        Express,
        Overnight,
        International
    }

    public enum Currency
    {
        USD,
        EUR,
        GBP,
        JPY,
        AUD,
        CAD
    }

    public enum TaxType
    {
        VAT,
        GST,
        SalesTax,
        ServiceTax
    }

    public enum CartStatus
    {
        Active,
        SavedForLater,
        Purchased,
        Abandoned
    }


    public enum WishlistStatus
    {
        Active,
        Inactive,
        Shared
    }


    public enum ReturnReason
    {
        Defective,
        WrongItem,
        ChangedMind,
        BetterPriceElsewhere,
        Other
    }
}
