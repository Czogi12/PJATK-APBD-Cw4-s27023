namespace LegacyRenewalApp.interfaces.validators;

public interface IRenewalRequestValidator
{
    void Validate(int customerId, string planCode, int seatCount, string paymentMethod);
}