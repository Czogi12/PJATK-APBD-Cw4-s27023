namespace LegacyRenewalApp.models;

public abstract class SubscriptionTotalModifier(string notes, decimal amount)
{
    public readonly decimal Amount = amount;
    public readonly string Notes = notes;

    public SubscriptionTotalModifier() : this("", 0)
    {
    }

    public abstract decimal CalculateDiscount(decimal totalCost);
}