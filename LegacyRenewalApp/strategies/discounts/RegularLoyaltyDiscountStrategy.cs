using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts;

namespace LegacyRenewalApp.strategies;

public class RegularLoyaltyDiscountStrategy : ILoyaltyDiscountStrategy
{
    public SubscriptionTotalModifier GetDiscount(int yearsWithCompany)
    {
        return yearsWithCompany switch
        {
            >= 5 => new SubscriptionTotalPercentDiscount("long-term loyalty discount", 0.07m),
            >= 2 => new SubscriptionTotalPercentDiscount("basic loyalty discount", 0.03m),
            _ => new SubscriptionTotalPercentDiscount("", 0)
        };
    }
}