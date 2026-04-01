using System;
using LegacyRenewalApp.interfaces.repositories;
using LegacyRenewalApp.interfaces.services;
using LegacyRenewalApp.models;

namespace LegacyRenewalApp.services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public Customer GetById(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException("Customer id must be positive");
        }
        
        return customerRepository.GetById(customerId);
    }
}