namespace LegacyRenewalApp.strategies;

public interface ILoyaltyDiscountStrategy
{
    decimal GetDiscount(int yearsWithCompany);
    string GetNotes(int yearsWithCompany);
}