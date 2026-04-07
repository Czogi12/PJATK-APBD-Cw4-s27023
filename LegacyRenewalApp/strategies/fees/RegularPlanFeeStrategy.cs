using System;
using LegacyRenewalApp.enums;
using LegacyRenewalApp.interfaces.strategies.fees;
using LegacyRenewalApp.models;
using LegacyRenewalApp.models.discounts.fees;

namespace LegacyRenewalApp.strategies.fees;

public class RegularPlanFeeStrategy : IPlanFeeStrategy
{
    private readonly string Notes = "premium support included";

    public SubscriptionTotalModifier GetFee(PlanCode paymentMethod)
    {
        return paymentMethod switch
        {
            PlanCode.Start => new SubscriptionTotalFixedFee(Notes, 250m),
            PlanCode.Pro => new SubscriptionTotalFixedFee(Notes, 400m),
            PlanCode.Enterprise => new SubscriptionTotalFixedFee(Notes, 700m),
            _ => throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null)
        };

        // if (normalizedPlanCode == "START")
        //     supportFee = 250m;
        // else if (normalizedPlanCode == "PRO")
        //     supportFee = 400m;
        // else if (normalizedPlanCode == "ENTERPRISE") supportFee = 700m;
        //
        // notes += "premium support included; ";
    }
}