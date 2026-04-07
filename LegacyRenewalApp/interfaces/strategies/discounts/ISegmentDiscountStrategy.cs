using LegacyRenewalApp.enums;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies;

public interface ISegmentDiscountStrategy
{
    // decimal GetDiscount(CustomerSegment segment);
    // string GetNotes(CustomerSegment segment);
    SubscriptionTotalModifier GetDiscount(CustomerSegment segment);
}