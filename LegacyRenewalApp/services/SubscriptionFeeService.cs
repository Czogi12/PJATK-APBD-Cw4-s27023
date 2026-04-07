using LegacyRenewalApp.enums;
using LegacyRenewalApp.interfaces.services;
using LegacyRenewalApp.interfaces.strategies.fees;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts.fees;

namespace LegacyRenewalApp.services;

public class SubscriptionFeeService(
    IPlanFeeStrategy planFeeStrategy,
    IPaymentMethodFeeStrategy paymentMethodFeeStrategy) : ISubscriptionFeeService
{
    public SubscriptionTotalModifier CalculatePaymentFee(PaymentMethod paymentMethod, decimal baseAmount)
    {
        var fee = paymentMethodFeeStrategy.GetFee(paymentMethod);
        return new SubscriptionTotalFixedFee(fee.Notes, fee.CalculateDiscount(baseAmount));
    }

    public SubscriptionTotalModifier CalculatePlanFee(bool includePremiumSupport, PlanCode planCode)
    {
        return !includePremiumSupport
            ? new SubscriptionTotalFixedFee("", 0)
            : planFeeStrategy.GetFee(planCode);
    }
}