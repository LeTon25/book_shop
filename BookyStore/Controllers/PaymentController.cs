using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using Utility;
using VNPay.Services;

namespace BookyStore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IUnitOfWork _unitOfWork;  
        public PaymentController(IVnPayService vnPayService,IUnitOfWork unitOfWork)
        {
            this._vnPayService = vnPayService;
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePaymentUrl(int? orderID = null) 
        {
            if(orderID == null)
            {
                return NotFound();  
            }    
            var model = _unitOfWork.OrderRepo.GetFirstOrDefault(c=>c.ID == orderID);
            if(model == null)
            {
                return NotFound();
            }    
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            string orderID = _vnPayService.GetOrderId(response.OrderDescription);
            var orderFromDB = _unitOfWork.OrderRepo.GetFirstOrDefault(c => c.ID == int.Parse(orderID));
            if(orderFromDB == null)
            {
                return NotFound();
            }
            if(response.VnPayResponseCode == "00")
            {
                _unitOfWork.OrderRepo.UpdateStatus(int.Parse(orderID),SD.StatusApproved,SD.PaymentStatusApproved);
                _unitOfWork.Save();
            }
            else
            {
                response.Success = false;
            }

            return View(response);
        }

        public IActionResult Ipn()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Ok();
        }
    }
}
