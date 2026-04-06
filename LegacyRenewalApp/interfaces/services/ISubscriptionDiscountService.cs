using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface ISubscriptionDiscountService
{
    public SubscriptionDiscount CalculateDiscount(decimal totalAmount, Customer customer, int seatCount,
        bool useLoyaltyPoints);
}