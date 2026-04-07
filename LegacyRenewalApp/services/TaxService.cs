using LegacyRenewalApp.interfaces.services;

namespace LegacyRenewalApp.services;

public class TaxService : ITaxService
{
    public decimal GetTaxRate(string country)
    {
        return country.ToLowerInvariant() switch
        {
            "poland" => 0.23m,
            "germany" => 0.19m,
            "czech republic" => 0.21m,
            "norway" => 0.25m,
            _ => 0.2m
        };

        // var taxRate = 0.20m;
        // if (customer.Country == "Poland")
        //     taxRate = 0.23m;
        // else if (customer.Country == "Germany")
        //     taxRate = 0.19m;
        // else if (customer.Country == "Czech Republic")
        //     taxRate = 0.21m;
        // else if (customer.Country == "Norway") taxRate = 0.25m;
    }
}