using System;
using LegacyRenewalApp.adapters;
using LegacyRenewalApp.extensions;
using LegacyRenewalApp.interfaces;
using LegacyRenewalApp.interfaces.services;
using LegacyRenewalApp.interfaces.validators;
using LegacyRenewalApp.models;
using LegacyRenewalApp.repositories;
using LegacyRenewalApp.strategies;
using LegacyRenewalApp.strategies.discounts;
using LegacyRenewalApp.strategies.fees;
using LegacyRenewalApp.utils;
using LegacyRenewalApp.validators;

namespace LegacyRenewalApp.services;

public class SubscriptionRenewalService(
    ICustomerService customerService,
    IPlanService planService,
    IRenewalRequestValidator renewalRequestValidator,
    ISubscriptionDiscountService subscriptionDiscountService,
    ISubscriptionFeeService subscriptionFeeService,
    ITaxService taxService,
    IBillingGateway billingGateway
) : ISubscriptionRenewalService
{
    public SubscriptionRenewalService() :
        this(new CustomerService(new CustomerRepository()), new PlanService(new SubscriptionPlanRepository()),
            new RenewalRequestValidator(), new SubscriptionDiscountService(
                new RegularSegmentDiscountStrategy(),
                new RegularLoyaltyDiscountStrategy(),
                new RegularSeatsDiscountStrategy(),
                new RegularPointsDiscountStrategy()
            ),
            new SubscriptionFeeService(
                new RegularPlanFeeStrategy(),
                new RegularPaymentFeeStrategy()
            ),
            new TaxService(),
            new BillingGatewayAdapter()
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

        var discounts =
            subscriptionDiscountService.CalculateDiscount(baseAmount, customer, seatCount, useLoyaltyPoints);

        var subtotalAfterDiscount =
            subscriptionDiscountService.CalculateSubTotal(discounts.CalculateDiscount(baseAmount));

        var planFee = subscriptionFeeService.CalculatePlanFee(includePremiumSupport, planCode.ToPlanCode());
        var paymentFee =
            subscriptionFeeService.CalculatePaymentFee(paymentMethod.ToPaymentMethod(),
                planFee.CalculateDiscount(subtotalAfterDiscount.Amount));

        var notes = discounts.Notes + "; " + planFee.Notes + "; " + paymentFee.Notes + "; ";

        var taxRate = taxService.GetTaxRate(customer.Country);

        var taxBase = subtotalAfterDiscount.Amount + planFee.Amount + paymentFee.Amount;
        var taxAmount = taxBase * taxRate;
        var finalAmount = taxBase + taxAmount;

        if (finalAmount < 500m)
        {
            finalAmount = 500m;
            notes += "minimum invoice amount applied; ";
        }

        var invoice = new RenewalInvoice
        {
            InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{PlanCodeUtil.Normalize(planCode)}",
            CustomerName = customer.FullName,
            PlanCode = PlanCodeUtil.Normalize(planCode),
            PaymentMethod = normalizedPaymentMethod,
            SeatCount = seatCount,
            BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
            DiscountAmount = Math.Round(discounts.Amount, 2, MidpointRounding.AwayFromZero),
            SupportFee = Math.Round(planFee.Amount, 2, MidpointRounding.AwayFromZero),
            PaymentFee = Math.Round(paymentFee.Amount, 2, MidpointRounding.AwayFromZero),
            TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
            FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
            Notes = notes.Trim(),
            GeneratedAt = DateTime.UtcNow
        };

        billingGateway.SaveInvoice(invoice);

        if (!string.IsNullOrWhiteSpace(customer.Email))
            billingGateway.SendEmail(customer.Email, "Subscription renewal invoice",
                $"Hello {customer.FullName}, your renewal for plan {PlanCodeUtil.Normalize(planCode)} " +
                $"has been prepared. Final amount: {invoice.FinalAmount:F2}.");

        return invoice;
    }
}