using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts;

namespace LegacyRenewalApp.strategies;

public class RegularPointsDiscountStrategy : IPointsDiscountStrategy
{
    public SubscriptionTotalModifier GetDiscount(int loyaltyPoints)
    {
        if (loyaltyPoints <= 0) return new SubscriptionTotalFixedDiscount("", 0);
        var pointsToUse = loyaltyPoints > 200 ? 200 : loyaltyPoints;
        return new SubscriptionTotalFixedDiscount($"loyalty points used: {pointsToUse}", pointsToUse);
    }
}