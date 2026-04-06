using System;
using LegacyRenewalApp.interfaces.services;
using LegacyRenewalApp.interfaces.validators;
using LegacyRenewalApp.libs;
using LegacyRenewalApp.models;
using LegacyRenewalApp.repositories;
using LegacyRenewalApp.strategies;
using LegacyRenewalApp.utils;
using LegacyRenewalApp.validators;

namespace LegacyRenewalApp.services;

public class SubscriptionRenewalService(
    ICustomerService customerService,
    IPlanService planService,
    IRenewalRequestValidator renewalRequestValidator,
    ISubscriptionDiscountService subscriptionDiscountService
) : ISubscriptionRenewalService
{
    public SubscriptionRenewalService() :
        this(new CustomerService(new CustomerRepository()), new PlanService(new SubscriptionPlanRepository()),
            new RenewalRequestValidator(), new SubscriptionDiscountService(
                new RegularSegmentDiscountStrategy(),
                new RegularLoyaltyDiscountStrategy(),
                new RegularSeatsDiscountStrategy(),
                new RegularPointsDiscountStrategy()
            )
        )
    {
    }

    public RenewalInvoice CreateRenewalInvoice(
        int customerId,
        string planCode,
        int seatCount,
        string paymentMethod,
        bool includePremiumSupport,
        bool useLoyaltyPoints)
    {
        renewalRequestValidator.Validate(seatCount, paymentMethod);

        var normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

        var customer = customerService.GetById(customerId);
        var plan = planService.GetByCode(planCode);

        if (!customer.IsActive) throw new InvalidOperationException("Inactive customers cannot renew subscriptions");


        var baseAmount = plan.MonthlyPricePerSeat * seatCount * 12m + plan.SetupFee;
        var discount = subscriptionDiscountService.CalculateDiscount(baseAmount, customer, seatCount, useLoyaltyPoints);
        var notes = discount.Notes + "; ";

        var subtotalAfterDiscount = baseAmount - discount.DiscountAmount;
        if (subtotalAfterDiscount < 300m)
        {
            subtotalAfterDiscount = 300m;
            notes += "minimum discounted subtotal applied; ";
        }

        var supportFee = 0m;
        var normalizedPlanCode = PlanCodeUtil.Normalize(planCode);
        if (includePremiumSupport)
        {
            if (normalizedPlanCode == "START")
                supportFee = 250m;
            else if (normalizedPlanCode == "PRO")
                supportFee = 400m;
            else if (normalizedPlanCode == "ENTERPRISE") supportFee = 700m;

            notes += "premium support included; ";
        }

        var paymentFee = 0m;
        if (normalizedPaymentMethod == "CARD")
        {
            paymentFee = (subtotalAfterDiscount + supportFee) * 0.02m;
            notes += "card payment fee; ";
        }
        else if (normalizedPaymentMethod == "BANK_TRANSFER")
        {
            paymentFee = (subtotalAfterDiscount + supportFee) * 0.01m;
            notes += "bank transfer fee; ";
        }
        else if (normalizedPaymentMethod == "PAYPAL")
        {
            paymentFee = (subtotalAfterDiscount + supportFee) * 0.035m;
            notes += "paypal fee; ";
        }
        else if (normalizedPaymentMethod == "INVOICE")
        {
            paymentFee = 0m;
            notes += "invoice payment; ";
        }
        else
        {
            throw new ArgumentException("Unsupported payment method");
        }

        var taxRate = 0.20m;
        if (customer.Country == "Poland")
            taxRate = 0.23m;
        else if (customer.Country == "Germany")
            taxRate = 0.19m;
        else if (customer.Country == "Czech Republic")
            taxRate = 0.21m;
        else if (customer.Country == "Norway") taxRate = 0.25m;

        var taxBase = subtotalAfterDiscount + supportFee + paymentFee;
        var taxAmount = taxBase * taxRate;
        var finalAmount = taxBase + taxAmount;

        if (finalAmount < 500m)
        {
            finalAmount = 500m;
            notes += "minimum invoice amount applied; ";
        }

        var invoice = new RenewalInvoice
        {
            InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
            CustomerName = customer.FullName,
            PlanCode = normalizedPlanCode,
            PaymentMethod = normalizedPaymentMethod,
            SeatCount = seatCount,
            BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
            DiscountAmount = Math.Round(discount.DiscountAmount, 2, MidpointRounding.AwayFromZero),
            SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
            PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
            TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
            FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
            Notes = notes.Trim(),
            GeneratedAt = DateTime.UtcNow
        };

        LegacyBillingGateway.SaveInvoice(invoice);

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            var subject = "Subscription renewal invoice";
            var body =
                $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

            LegacyBillingGateway.SendEmail(customer.Email, subject, body);
        }

        return invoice;
    }
}