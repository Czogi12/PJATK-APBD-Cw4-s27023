using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface ISubscriptionRenewalService
{
    RenewalInvoice CreateRenewalInvoice(
        int customerId,
        string planCode,
        int seatCount,
        string paymentMethod,
        bool includePremiumSupport,
        bool useLoyaltyPoints);
}