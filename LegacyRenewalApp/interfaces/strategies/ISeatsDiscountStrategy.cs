using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies;

public interface ISeatsDiscountStrategy
{
    // decimal GetDiscount(int seats);
    // string GetNotes(int seats);
    SubscriptionDiscount GetDiscount(int seatCount);
}