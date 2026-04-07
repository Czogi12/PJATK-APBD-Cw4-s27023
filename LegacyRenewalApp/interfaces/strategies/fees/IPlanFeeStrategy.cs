using LegacyRenewalApp.enums;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies.fees;

public interface IPlanFeeStrategy
{
    SubscriptionTotalModifier GetFee(PlanCode paymentMethod);
}