using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.VNPay;
using System.Security.Cryptography.X509Certificates;
using Utility;


namespace BookyStore.Controllers
{
	public class PaymentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public PaymentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[Authorize]
		public IActionResult Index(int? id)
		{
			if (id == null)
			{
				return NotFound();	
			}	
			Order order = _unitOfWork.OrderRepo.GetFirstOrDefault(c=> c.ID == id);
			List<OrderDetail> details = _unitOfWork.OrderDetailRepo.GetAll(c => c.OrderID == order.ID).ToList();
			Payment payment = new Payment()
			{
				Id = Guid.NewGuid().ToString(),
				RequiredAmount= (decimal)order.OrderTotal,
				PaymentCurrency = "VND",
				PaymentLanguage ="vn",
				PaymentDestinationId = "VNPAY",
				PaymentDate = DateTime.Now,	
				ExpireDate = DateTime.Now.AddMinutes(15),
				OrderID = id,
				PaymentContent = $"Thanh toán hóa đơn {id}"

			};
			_unitOfWork.PaymentRepo.Add(payment);
			_unitOfWork.Save();
			var paymentUrl = string.Empty;
			switch (payment.PaymentDestinationId)
			{
				case "VNPAY":
					var vnPayRequest = new VnPayRequest()
					{
						vnp_Version = VNPayConfig.Version,
						vnp_TmnCode = VNPayConfig.TmnCode,	
						vnp_CreateDate = payment.PaymentDate?.ToString("yyyyMMddHHmmss"),
						vnp_IpAddr = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty,
						vnp_Amount = (int)payment.RequiredAmount,
						vnp_CurrCode = payment.PaymentCurrency ?? string.Empty,
						vnp_OrderType = "other",
						vnp_OrderInfo = payment.PaymentContent.ToString() ?? string.Empty,
						vnp_ReturnUrl = VNPayConfig.ReturnUrl,
						vnp_TxnRef = id.ToString(),
						vnp_Locale = "vn",
						vnp_Command = "pay"
					};
					paymentUrl = vnPayRequest.GetLink(VNPayConfig.PaymentUrl, VNPayConfig.HashSecret);
					break;
				default:
					break;
			}
			Console.WriteLine(paymentUrl);
			return View();
		}
		public IActionResult HandleResponseVNPay([FromQuery] VNPayResponse vNPayResponse)
		{
			if(vNPayResponse.vnp_TxnRef == null )
			{
				//thêm xử lý lỗi
				return NotFound();
			}
			Payment payment = _unitOfWork.PaymentRepo.GetFirstOrDefault(c => c.Id == vNPayResponse.vnp_TxnRef,includeProperties:"Order");
			if (vNPayResponse.IsValidSignature(VNPayConfig.HashSecret))
			{
				if (vNPayResponse.vnp_ResponseCode == "00" && vNPayResponse.vnp_TransactionStatus == "00")
				{
					// thanh toán thành công
					payment.PaymentStatus = SD.PaymentStatusApproved;
					payment.Order.PaymentStatus = SD.PaymentStatusApproved;

				}
				else
				{
					// thanh toán không thành công
					payment.PaymentStatus= SD.PaymentStatusDelayed;
				}
			}
			else
			{
				
				//xử lí lỗi 
				return NotFound();
			}
			return View();
		}
	}
}
