namespace LegacyRenewalApp.models.discounts;

public class SubscriptionTotalFixedDiscount(string note, decimal amount) : SubscriptionTotalModifier(note, amount)
{
    public override decimal CalculateDiscount(decimal totalCost)
    {
        return totalCost - Amount;
    }
}