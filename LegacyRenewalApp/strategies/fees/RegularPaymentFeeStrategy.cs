using System;
using LegacyRenewalApp.enums;
using LegacyRenewalApp.interfaces.strategies.fees;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts.fees;

namespace LegacyRenewalApp.strategies.fees;

public class RegularPaymentFeeStrategy : IPaymentMethodFeeStrategy
{
    public SubscriptionTotalModifier GetFee(PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.Card => new SubscriptionTotalPercentFee("card payment fee", 0.02m),
            PaymentMethod.BankTransfer => new SubscriptionTotalPercentFee("bank transfer fee", 0.01m),
            PaymentMethod.PayPal => new SubscriptionTotalPercentFee("paypal fee", 0.035m),
            PaymentMethod.Invoice => new SubscriptionTotalPercentFee("invoice payment", 0),
            _ => throw new ArgumentException("Unsupported payment method")
        };
        // if (normalizedPaymentMethod == "CARD")
        // {
        //     paymentFee = (subtotalAfterDiscount.Amount + supportFee) * 0.02m;
        //     notes += "card payment fee; ";
        // }
        // else if (normalizedPaymentMethod == "BANK_TRANSFER")
        // {
        //     paymentFee = (subtotalAfterDiscount.Amount + supportFee) * 0.01m;
        //     notes += "bank transfer fee; ";
        // }
        // else if (normalizedPaymentMethod == "PAYPAL")
        // {
        //     paymentFee = (subtotalAfterDiscount.Amount + supportFee) * 0.035m;
        //     notes += "paypal fee; ";
        // }
        // else if (normalizedPaymentMethod == "INVOICE")
        // {
        //     paymentFee = 0m;
        //     notes += "invoice payment; ";
        // }
        // else
        // {
        //     throw new ArgumentException("Unsupported payment method");
        // }
    }
}