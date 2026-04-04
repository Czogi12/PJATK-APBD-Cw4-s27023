using LegacyRenewalApp.enums;

namespace LegacyRenewalApp.strategies;

public interface ISegmentDiscountStrategy
{
    decimal GetDiscount(CustomerSegment segment);
}