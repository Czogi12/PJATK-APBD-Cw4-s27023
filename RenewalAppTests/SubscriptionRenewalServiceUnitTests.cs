using LegacyRenewalApp;
using LegacyRenewalApp.services;

namespace RenewalAppTests;

public class SubscriptionRenewalServiceUnitTests
{
    [Fact]
    public void CreateRenewalInvoiceShouldBeSuccesfullAndReturnCorrectInvoice()
    {
        var renewalService = new SubscriptionRenewalService();

        var invoice = renewalService.CreateRenewalInvoice(
            customerId: 3,
            planCode: "PRO",
            seatCount: 18,
            paymentMethod: "CARD",
            includePremiumSupport: true,
            useLoyaltyPoints: true);
        
        
        Assert.NotNull(invoice);
        Assert.Equal("InvoiceNumber=INV-20260401-3-PRO, Customer=John Smith, Plan=PRO, Seats=18, FinalAmount=17671,67, Notes=platinum discount; long-term loyalty discount; small team discount; loyalty points used: 200; premium support included; card payment fee;", invoice.ToString());
        Assert.Equal("17671,67", $"{invoice.FinalAmount:F2}");
    }
}