using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies;

public interface IPointsDiscountStrategy
{
    // decimal GetDiscount(int loyaltyPoints);
    // string GetNotes(int loyaltyPoints);
    SubscriptionTotalModifier GetDiscount(int loyaltyPoints);
}