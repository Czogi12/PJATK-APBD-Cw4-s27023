using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.services;

public interface ICustomerService
{
    public Customer GetById(int customerId);
}