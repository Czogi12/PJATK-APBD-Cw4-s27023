using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface IPlanService
{
     SubscriptionPlan GetByCode(string code);
}