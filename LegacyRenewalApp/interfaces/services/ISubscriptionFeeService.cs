using LegacyRenewalApp.enums;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface ISubscriptionFeeService
{
    SubscriptionTotalModifier CalculatePaymentFee(PaymentMethod paymentMethod, decimal baseAmount);
    SubscriptionTotalModifier CalculatePlanFee(bool includePremiumSupport, PlanCode planCode);
}