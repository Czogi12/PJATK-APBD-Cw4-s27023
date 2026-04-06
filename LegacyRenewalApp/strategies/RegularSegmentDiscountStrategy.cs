using System;
using LegacyRenewalApp.enums;
using LegacyRenewalApp.interfaces.strategies;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.strategies;

public class RegularSegmentDiscountStrategy : ISegmentDiscountStrategy
{
    public SubscriptionDiscount GetDiscount(CustomerSegment segment)
    {
        var note = $"{Enum.GetName(segment)?.ToLowerInvariant()} discount";
        return segment switch
        {
            CustomerSegment.Silver => new SubscriptionDiscount(note, 0.05m),
            CustomerSegment.Gold => new SubscriptionDiscount(note, 0.1m),
            CustomerSegment.Platinum => new SubscriptionDiscount(note, 0.15m),
            CustomerSegment.Education => new SubscriptionDiscount(note, 0.2m),
            _ => throw new ArgumentOutOfRangeException(nameof(segment))
        };
    }
}