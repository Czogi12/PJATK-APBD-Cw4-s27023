using System;
using LegacyRenewalApp.enums;

namespace LegacyRenewalApp.extensions;

public static class StringExtensions
{
    public static PlanCode ToPlanCode(this string planCode)
    {
        return planCode.Trim().ToUpperInvariant() switch
        {
            "START" => PlanCode.Start,
            "PRO" => PlanCode.Pro,
            "ENTERPRISE" => PlanCode.Enterprise,
            _ => throw new ArgumentException($"Unsupported planCode: {planCode}")
        };
    }

    public static PaymentMethod ToPaymentMethod(this string paymentMethod)
    {
        return paymentMethod.Trim().ToUpperInvariant() switch
        {
            "CARD" => PaymentMethod.Card,
            "BANK_TRANSFER" => PaymentMethod.BankTransfer,
            "PAYPAL" => PaymentMethod.PayPal,
            "INVOICE" => PaymentMethod.Invoice,
            _ => throw new ArgumentException($"Unsupported paymentMethod: {paymentMethod}")
        };
    }
}