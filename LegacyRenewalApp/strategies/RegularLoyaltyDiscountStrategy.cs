using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.strategies;

public class RegularLoyaltyDiscountStrategy : ILoyaltyDiscountStrategy
{
    public SubscriptionDiscount GetDiscount(int yearsWithCompany)
    {
        return yearsWithCompany switch
        {
            >= 5 => new SubscriptionDiscount("long-term loyalty discount", 0.07m),
            >= 2 => new SubscriptionDiscount("basic loyalty discount", 0.03m),
            _ => new SubscriptionDiscount("", 0)
        };
    }
}