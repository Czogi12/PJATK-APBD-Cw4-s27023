using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.repositories;

public interface ISubscriptionPlanRepository
{
    SubscriptionPlan GetByCode(string code);
}