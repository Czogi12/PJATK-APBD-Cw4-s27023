using LegacyRenewalApp.enums;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.strategies.fees;

public interface IPaymentMethodFeeStrategy
{
    SubscriptionTotalModifier GetFee(PaymentMethod paymentMethod);
}