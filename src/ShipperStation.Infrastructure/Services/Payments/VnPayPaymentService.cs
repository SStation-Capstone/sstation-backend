using Microsoft.Extensions.Options;
using ShipperStation.Application.Contracts.Payments;
using ShipperStation.Application.Interfaces.Services.Payments;
using ShipperStation.Infrastructure.Settings;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Infrastructure.Services.Payments;

public class VnPayPaymentService : IVnPayPaymentService
{
    private readonly VnPaySettings _vnPaySettings;

    private const string DefaultPaymentInfo = "Thanh toán với VnPay";

    public VnPayPaymentService(IOptions<VnPaySettings> vnPaySettings)
    {
        _vnPaySettings = vnPaySettings.Value;
    }

    public async Task<string> CreatePaymentAsync(VnPayPayment payment)
    {

        var pay = new VnPayLibrary();
        var urlCallBack = $"{_vnPaySettings.CallbackUrl}/{payment.PaymentReferenceId}";

        pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
        pay.AddRequestData("vnp_Version", _vnPaySettings.Version);
        pay.AddRequestData("vnp_Command", _vnPaySettings.Command);
        pay.AddRequestData("vnp_TmnCode", _vnPaySettings.TmnCode);
        pay.AddRequestData("vnp_CurrCode", _vnPaySettings.CurrCode);

        pay.AddRequestData("vnp_Amount", ((int)payment.Amount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", payment.Time.ToString("yyyyMMddHHmmss"));

        pay.AddRequestData("vnp_IpAddr", UtilitiesExtensions.GetIpAddress());
        pay.AddRequestData("vnp_Locale", _vnPaySettings.Locale);
        pay.AddRequestData("vnp_OrderInfo", payment.Info ?? DefaultPaymentInfo);
        pay.AddRequestData("vnp_OrderType", payment.OrderType.ToString());

        pay.AddRequestData("vnp_TxnRef", payment.PaymentReferenceId);
        pay.AddRequestData("vnp_BankCode", string.Empty);

        var paymentUrl =
            pay.CreateRequestUrl(_vnPaySettings.PaymentEndpoint, _vnPaySettings.HashSecret);

        //var pm = new Payment()
        //{
        //    Method = PaymentMethod.VnPay,
        //    Amount = payment.Amount,
        //    Content = payment.Info,
        //    //ReferenceId = payment.PaymentReferenceId,
        //    TransactionUrl = paymentUrl,
        //    Qr = paymentUrl
        //};

        return await Task.FromResult(paymentUrl);
    }
}