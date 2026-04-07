using System;
using LegacyRenewalApp.enums;
using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts;

namespace LegacyRenewalApp.strategies.discounts;

public class RegularSegmentDiscountStrategy : ISegmentDiscountStrategy
{
    public SubscriptionTotalModifier GetDiscount(CustomerSegment segment)
    {
        var note = $"{Enum.GetName(segment)?.ToLowerInvariant()} discount";
        return segment switch
        {
            CustomerSegment.Silver => new SubscriptionTotalPercentDiscount(note, 0.05m),
            CustomerSegment.Gold => new SubscriptionTotalPercentDiscount(note, 0.1m),
            CustomerSegment.Platinum => new SubscriptionTotalPercentDiscount(note, 0.15m),
            CustomerSegment.Education => new SubscriptionTotalPercentDiscount(note, 0.2m),
            _ => throw new ArgumentOutOfRangeException(nameof(segment))
        };
    }
}