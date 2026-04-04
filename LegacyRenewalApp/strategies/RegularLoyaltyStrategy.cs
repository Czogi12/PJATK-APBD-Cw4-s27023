namespace LegacyRenewalApp.strategies;

public class RegularLoyaltyStrategy : ILoyaltyDiscountStrategy
{
    public decimal GetDiscount(int yearsWithCompany)
    {
        return yearsWithCompany switch
        {
            >= 5 => 0.07m,
            >= 2 => 0.03m,
            _ => 0
        };
    }

    public string GetNotes(int yearsWithCompany)
    {
        return yearsWithCompany switch
        {
            >= 5 => "long-term loyalty discount; ",
            >= 2 => "basic loyalty discount; ",
            _ => ""
        };
    }
}