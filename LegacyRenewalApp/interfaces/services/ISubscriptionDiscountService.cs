using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface ISubscriptionDiscountService
{
    public SubscriptionTotalModifier CalculateDiscount(decimal totalAmount, Customer customer, int seatCount,
        bool useLoyaltyPoints);

    public SubscriptionTotalModifier CalculateSubTotal(decimal baseAmount);
}