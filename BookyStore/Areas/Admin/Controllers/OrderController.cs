using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using Utility;

namespace BookyStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		[BindProperty]
		public OrderVM OrderVM { get; set; }
		public OrderController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var orderList = unitOfWork.OrderRepo.GetAll(includeProperties: "ApplicationUser").ToList();
			return View(orderList);
		}
		public IActionResult ViewDetail(int orderID) 
		{
			OrderVM = new OrderVM()
			{
				Order = unitOfWork.OrderRepo.GetFirstOrDefault(c=>c.ID == orderID,includeProperties: "ApplicationUser"),
				OrderDetails = unitOfWork.OrderDetailRepo.GetAll(c=>c.Order.ID == orderID,includeProperties:"Product").ToList()
			};	
			return View(OrderVM);
		}
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [AutoValidateAntiforgeryToken]
        public IActionResult ApproveOrder()
		{
            unitOfWork.OrderRepo.UpdateStatus(OrderVM.Order.ID, orderStatus:SD.StatusApproved);
            unitOfWork.Save();
            // xử lí thông báo
            TempData["StatusMessage"] = "";
            return RedirectToAction(nameof(ViewDetail), new { orderID = OrderVM.Order.ID });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [AutoValidateAntiforgeryToken]
        public IActionResult ConfirmPayment()
		{
            unitOfWork.OrderRepo.UpdateStatus(OrderVM.Order.ID, paymentStatus: SD.PaymentStatusApproved);
            unitOfWork.Save();
            // xử lí thông báo
            TempData["StatusMessage"] = "";
            return RedirectToAction(nameof(ViewDetail), new { orderID = OrderVM.Order.ID });
        }
		[HttpPost]
		[Authorize(Roles =SD.Role_Admin + "," + SD.Role_Employee)]
		[AutoValidateAntiforgeryToken]
		public IActionResult UpdateOrderDetail()
		{
			Order orderFromDB = unitOfWork.OrderRepo.GetFirstOrDefault(c => c.ID == OrderVM.Order.ID);
			orderFromDB.Name = OrderVM.Order.Name;
            orderFromDB.PhoneNumber = OrderVM.Order.PhoneNumber;
			orderFromDB.StreetAddress =OrderVM.Order.StreetAddress;	
			orderFromDB.City = OrderVM.Order.City;
			orderFromDB.State = OrderVM.Order.State;	
			orderFromDB.PostalCode = OrderVM.Order.PostalCode;	
			if (!string.IsNullOrEmpty(OrderVM.Order.Carrier))
			{
				orderFromDB.Carrier = OrderVM.Order.Carrier;
			}
            if (!string.IsNullOrEmpty(OrderVM.Order.TrackingNumber))
            {
                orderFromDB.TrackingNumber = OrderVM.Order.TrackingNumber;
            }
			unitOfWork.OrderRepo.Update(orderFromDB);
			unitOfWork.Save();
            return RedirectToAction(nameof(ViewDetail),new {orderID = orderFromDB.ID});
		}
		[HttpPost]
		[Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
		[AutoValidateAntiforgeryToken]
		public IActionResult StartProcess()
		{
			unitOfWork.OrderRepo.UpdateStatus(OrderVM.Order.ID,SD.StatusInProcess);
			unitOfWork.Save();
			// xử lí thông báo
			TempData["StatusMessage"] = "";
			return RedirectToAction(nameof(ViewDetail),new {orderID = OrderVM.Order.ID});
		}
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [AutoValidateAntiforgeryToken]
        public IActionResult Shipped()
        {
			Order order = unitOfWork.OrderRepo.GetFirstOrDefault(C => C.ID == OrderVM.Order.ID);
			order.TrackingNumber = OrderVM.Order.TrackingNumber;
			order.Carrier = OrderVM.Order.Carrier;
			order.ShippingDate = DateTime.Now;
			order.OrderStatus = SD.StatusShipped;
	
            unitOfWork.OrderRepo.Update(order);
            unitOfWork.Save();
            // xử lí thông báo
            TempData["StatusMessage"] = "Cập nhật trạng thái đã giao hàng";
            return RedirectToAction(nameof(ViewDetail), new { orderID = OrderVM.Order.ID });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [AutoValidateAntiforgeryToken]
        public IActionResult Cancel()
        {

			unitOfWork.OrderRepo.UpdateStatus(OrderVM.Order.ID, orderStatus: SD.StatusCancelled);
            unitOfWork.Save();

            // TO DO : xử lí thông báo
            TempData["StatusMessage"] = "Cập nhật trạng thái hủy đơn hàn";

			// TO DO : XỬ LÍ hoàn tiền
            return RedirectToAction(nameof(ViewDetail), new { orderID = OrderVM.Order.ID });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [AutoValidateAntiforgeryToken]
        public IActionResult PayNow()
        {

			// TO DO : Xử lí thanh toan

            // TO DO : xử lí thông báo
            TempData["StatusMessage"] = "Thanh toán thành công";

            // TO DO : XỬ LÍ hoàn tiền
            return RedirectToAction(nameof(ViewDetail), new { orderID = OrderVM.Order.ID });
        }

        #region Api_Call
        [HttpGet]
		[Authorize]
		public IActionResult GetAll(string status)
		{
            var orderList = new List<Order>();
			if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
			{
                orderList = unitOfWork.OrderRepo.GetAll( includeProperties: "ApplicationUser").ToList();
			}
			else
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                orderList = unitOfWork.OrderRepo.GetAll(c=> c.ApplicationUserID == userID,includeProperties: "ApplicationUser").ToList();
            }
            switch (status)
            {
                case "inprocess":
					orderList = orderList.Where(c => c.OrderStatus == SD.StatusInProcess).ToList();
                    break;
                case "shipped":
                    orderList = orderList.Where(c => c.OrderStatus == SD.StatusShipped).ToList();
                    break;
                case "paymentapproved":
                    orderList = orderList.Where(c => c.OrderStatus == SD.PaymentStatusApproved).ToList();
                    break;
                default:
                    break;
            }
			return Json(new { data = orderList });
		}
		#endregion
	}
}

