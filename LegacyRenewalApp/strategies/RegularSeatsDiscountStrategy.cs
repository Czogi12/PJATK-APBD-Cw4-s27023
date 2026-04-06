using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.strategies;

public class RegularSeatsDiscountStrategy : ISeatsDiscountStrategy
{
    public SubscriptionDiscount GetDiscount(int seatCount)
    {
        return seatCount switch
        {
            >= 50 => new SubscriptionDiscount("large team discount", 0.12m),
            >= 20 => new SubscriptionDiscount("medium team discount", 0.08m),
            >= 10 => new SubscriptionDiscount("small team discount", 0.04m),
            _ => new SubscriptionDiscount()
        };
    }
}