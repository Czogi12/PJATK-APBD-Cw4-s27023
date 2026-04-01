namespace LegacyRenewalApp.interfaces.validators;

public interface IRenewalRequestValidator
{
    void Validate(int seatCount, string paymentMethod);
}