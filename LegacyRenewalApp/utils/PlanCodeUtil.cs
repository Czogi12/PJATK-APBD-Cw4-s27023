namespace LegacyRenewalApp.utils;

public static class PlanCodeUtil
{
    public static string Normalize(string code)
    {
        return code.Trim().ToUpperInvariant();
    }
}