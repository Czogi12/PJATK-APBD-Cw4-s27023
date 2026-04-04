using System;
using LegacyRenewalApp.enums;

namespace LegacyRenewalApp.strategies;

public class RegularSegmentDiscountStrategy : ISegmentDiscountStrategy
{
    public decimal GetDiscount(CustomerSegment segment)
    {
        return segment switch
        {
            CustomerSegment.Silver => 0.05m,
            CustomerSegment.Gold => 0.1m,
            CustomerSegment.Platinum => 0.15m,
            CustomerSegment.Education => 0.2m,
            _ => throw new ArgumentOutOfRangeException(nameof(segment), segment, null)
        };
    }

    public string GetNotes(CustomerSegment segment)
    {
        return $"{Enum.GetName(segment)} discount; ";
    }
}