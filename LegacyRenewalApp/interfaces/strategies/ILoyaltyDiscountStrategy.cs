using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies;

public interface ILoyaltyDiscountStrategy
{
    // decimal GetDiscount(int yearsWithCompany);
    // string GetNotes(int yearsWithCompany);
    SubscriptionDiscount GetDiscount(int yearsWithCompany);
}