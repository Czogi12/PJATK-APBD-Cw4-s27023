using System;
using LegacyRenewalApp.interfaces.validators;

namespace LegacyRenewalApp.validators;

public class RenewalRequestValidator : IRenewalRequestValidator
{
    public void Validate(int seatCount, string paymentMethod)
    {
        if (seatCount <= 0)
        {
            throw new ArgumentException("Seat count must be positive");
        }

        if (string.IsNullOrWhiteSpace(paymentMethod))
        {
            throw new ArgumentException("Payment method is required");
        }
    }
}