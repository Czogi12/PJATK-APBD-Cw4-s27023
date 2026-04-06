using System.Collections.Generic;
using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public class SubscriptionDiscountService(
    ISegmentDiscountStrategy segmentDiscountStrategy,
    ILoyaltyDiscountStrategy loyaltyDiscountStrategy,
    ISeatsDiscountStrategy seatsDiscountStrategy,
    IPointsDiscountStrategy pointsDiscountStrategy
) : ISubscriptionDiscountService
{
    public SubscriptionDiscount CalculateDiscount(decimal totalAbount, Customer customer, int seatCount,
        bool useLoyaltyPoints)
    {
        List<string> notes = [];
        var discountAmount = 0m;
        List<SubscriptionDiscount> subscriptionDiscounts =
        [
            segmentDiscountStrategy.GetDiscount(customer.CustomerSegment),
            loyaltyDiscountStrategy.GetDiscount(customer.YearsWithCompany),
            seatsDiscountStrategy.GetDiscount(seatCount)
        ];

        subscriptionDiscounts.ForEach(subscriptionDiscount =>
        {
            notes.Add(subscriptionDiscount.Notes);
            discountAmount += subscriptionDiscount.DiscountAmount * totalAbount;
        });

        if (useLoyaltyPoints)
        {
            var pointsDiscount = pointsDiscountStrategy.GetDiscount(customer.LoyaltyPoints);
            notes.Add(pointsDiscount.Notes);
            discountAmount += pointsDiscount.DiscountAmount;
        }

        return new SubscriptionDiscount(string.Join("; ", notes), discountAmount);
    }
}