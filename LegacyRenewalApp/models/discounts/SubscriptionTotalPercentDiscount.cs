namespace LegacyRenewalApp.models.discounts;

public class SubscriptionTotalPercentDiscount(string note, decimal amount) : SubscriptionTotalModifier(note, amount)
{
    public override decimal CalculateDiscount(decimal totalCost)
    {
        return totalCost - totalCost * Amount;
    }
}