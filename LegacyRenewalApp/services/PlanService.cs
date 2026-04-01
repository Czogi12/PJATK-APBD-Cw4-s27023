using System;
using LegacyRenewalApp.interfaces.repositories;
using LegacyRenewalApp.interfaces.services;
using LegacyRenewalApp.models;
using LegacyRenewalApp.utils;

namespace LegacyRenewalApp.services;

public class PlanService(ISubscriptionPlanRepository planRepository) : IPlanService
{
    public SubscriptionPlan GetByCode(string planCode)
    {
        if (string.IsNullOrWhiteSpace(planCode))
        {
            throw new ArgumentException("Plan code is required");
        }
        
        return planRepository.GetByCode(PlanCodeUtil.Normalize(planCode));
    }
}