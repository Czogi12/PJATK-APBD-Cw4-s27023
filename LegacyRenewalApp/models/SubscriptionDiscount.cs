namespace LegacyRenewalApp.models;

public class SubscriptionDiscount(string notes, decimal discountAmount)
{
    public SubscriptionDiscount() : this("", 0)
    {
    }

    public string Notes { get; set; } = notes;
    public decimal DiscountAmount { get; set; } = discountAmount;
}