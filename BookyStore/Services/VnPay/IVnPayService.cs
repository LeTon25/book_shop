
using Models;

namespace VNPay.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(Order model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
    string GetOrderId(string orderDescription);
}