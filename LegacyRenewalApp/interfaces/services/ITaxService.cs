namespace LegacyRenewalApp.interfaces.services;

public interface ITaxService
{
    decimal GetTaxRate(string country);
}