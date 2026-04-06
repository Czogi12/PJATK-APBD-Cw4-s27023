using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.strategies;

public class RegularPointsDiscountStrategy : IPointsDiscountStrategy
{
    public SubscriptionDiscount GetDiscount(int loyaltyPoints)
    {
        if (loyaltyPoints <= 0) return new SubscriptionDiscount();
        var pointsToUse = loyaltyPoints > 200 ? 200 : loyaltyPoints;
        return new SubscriptionDiscount($"loyalty points used: {pointsToUse}", pointsToUse);
    }
}