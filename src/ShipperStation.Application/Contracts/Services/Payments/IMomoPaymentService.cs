using ShipperStation.Application.Models.Payments;

namespace ShipperStation.Application.Contracts.Services.Payments;

public interface IMomoPaymentService
{
    public Task<string> CreatePaymentAsync(MomoPayment payment);
}