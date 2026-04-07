namespace LegacyRenewalApp.models.discounts.fees;

public class SubscriptionTotalPercentFee(string note, decimal amount) : SubscriptionTotalModifier(note, amount)
{
    public override decimal CalculateDiscount(decimal totalCost)
    {
        return totalCost * Amount;
    }
}