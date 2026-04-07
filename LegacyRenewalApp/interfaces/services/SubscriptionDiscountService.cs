using System.Collections.Generic;
using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts;

namespace LegacyRenewalApp.interfaces.services;

public class SubscriptionDiscountService(
    ISegmentDiscountStrategy segmentDiscountStrategy,
    ILoyaltyDiscountStrategy loyaltyDiscountStrategy,
    ISeatsDiscountStrategy seatsDiscountStrategy,
    IPointsDiscountStrategy pointsDiscountStrategy
) : ISubscriptionDiscountService
{
    public SubscriptionTotalModifier CalculateDiscount(decimal totalAbount, Customer customer, int seatCount,
        bool useLoyaltyPoints)
    {
        List<string> notes = [];
        var discountAmount = 0m;
        List<SubscriptionTotalModifier> subscriptionDiscounts =
        [
            segmentDiscountStrategy.GetDiscount(customer.CustomerSegment),
            loyaltyDiscountStrategy.GetDiscount(customer.YearsWithCompany),
            seatsDiscountStrategy.GetDiscount(seatCount)
        ];

        subscriptionDiscounts.ForEach(subscriptionDiscount =>
        {
            notes.Add(subscriptionDiscount.Notes);
            discountAmount += totalAbount - subscriptionDiscount.CalculateDiscount(totalAbount);
        });

        if (useLoyaltyPoints)
        {
            var pointsDiscount = pointsDiscountStrategy.GetDiscount(customer.LoyaltyPoints);
            notes.Add(pointsDiscount.Notes);
            discountAmount += pointsDiscount.Amount;
        }

        return new SubscriptionTotalFixedDiscount(string.Join("; ", notes), discountAmount);
    }
}