using LegacyRenewalApp.models;

namespace LegacyRenewalApp.interfaces.repositories;

public interface ICustomerRepository
{
    Customer GetById(int customerId);
}