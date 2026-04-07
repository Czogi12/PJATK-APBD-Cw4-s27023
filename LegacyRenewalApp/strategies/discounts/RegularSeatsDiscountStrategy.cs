using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts;

namespace LegacyRenewalApp.strategies;

public class RegularSeatsDiscountStrategy : ISeatsDiscountStrategy
{
    public SubscriptionTotalModifier GetDiscount(int seatCount)
    {
        return seatCount switch
        {
            >= 50 => new SubscriptionTotalPercentDiscount("large team discount", 0.12m),
            >= 20 => new SubscriptionTotalPercentDiscount("medium team discount", 0.08m),
            >= 10 => new SubscriptionTotalPercentDiscount("small team discount", 0.04m),
            _ => new SubscriptionTotalPercentDiscount("", 0)
        };
    }
}