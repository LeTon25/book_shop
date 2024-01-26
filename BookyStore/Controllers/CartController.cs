using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using System.Security.Claims;
using Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookyStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        [FromForm]
        public ShoppingCartVM shoppingCartVM { get; set; }  
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCartVM = new()
            {
                ShoppingCarts = _unitOfWork.ShoppingCartRepo.GetAll(x=> x.ApplicationUserId == userId,includeProperties: "Product").ToList(),    
                Order = new()
            };
            foreach(ShoppingCart shoppingCart in shoppingCartVM.ShoppingCarts)
            {
                shoppingCartVM.Order.OrderTotal += (double)shoppingCart.Product.Price * shoppingCart.Count;
            }
            return View(shoppingCartVM);
        }
        [HttpGet]
        public IActionResult Plus(int? cartID)
        {
            if (cartID == null)
            {
                return NotFound();
            }
            var cartFromDB = _unitOfWork.ShoppingCartRepo.GetFirstOrDefault(x => x.ID == cartID);
            if(cartFromDB == null)
            {
                return NotFound();
            }
            cartFromDB.Count += 1;
            _unitOfWork.ShoppingCartRepo.Update(cartFromDB);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Minus(int? cartID)
        {
            if (cartID == null)
            {
                return NotFound();
            }
            var cartFromDB = _unitOfWork.ShoppingCartRepo.GetFirstOrDefault(x => x.ID == cartID);
            if (cartFromDB == null)
            {
                return NotFound();
            }
            if (cartFromDB.Count <= 1)
            {
                _unitOfWork.ShoppingCartRepo.Delete(cartFromDB);
            }
            else
            {
                cartFromDB.Count -= 1;
                _unitOfWork.ShoppingCartRepo.Update(cartFromDB);
            }
           
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Remove(int? cartID)
        {
            if (cartID == null)
            {
                return NotFound();
            }
            var cartFromDB = _unitOfWork.ShoppingCartRepo.GetFirstOrDefault(x => x.ID == cartID);
            if (cartFromDB == null)
            {
                return NotFound();
            }
            _unitOfWork.ShoppingCartRepo.Delete(cartFromDB);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult StartOrder()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCartVM = new()
            {
                ShoppingCarts = _unitOfWork.ShoppingCartRepo.GetAll(x => x.ApplicationUserId == userId, includeProperties: "Product").ToList(),
                Order = new Order()
            };
            shoppingCartVM.Order.ApplicationUser = _unitOfWork.ApplicationUserRepo.GetFirstOrDefault(x => x.Id == userId);
            var currentUser = shoppingCartVM.Order.ApplicationUser;
            shoppingCartVM.Order.PhoneNumber = currentUser.PhoneNumber;
            shoppingCartVM.Order.City = currentUser.City;
            shoppingCartVM.Order.State = currentUser.State;
            shoppingCartVM.Order.PostalCode = currentUser.PostalCode;
            shoppingCartVM.Order.StreetAddress = currentUser.StreetAddress;
            shoppingCartVM.Order.Name = currentUser.Name;
            shoppingCartVM.Order.ApplicationUserID = userId;
            foreach (ShoppingCart shoppingCart in shoppingCartVM.ShoppingCarts)
            {
                shoppingCartVM.Order.OrderTotal += (double)shoppingCart.Product.Price * shoppingCart.Count;
            }
            return View(shoppingCartVM);
        }
		[HttpPost]
        [ActionName("StartOrder")]
		public IActionResult StartOrderPOST()
		{
			if (ModelState.IsValid)
            {
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
				shoppingCartVM.ShoppingCarts = _unitOfWork.ShoppingCartRepo.GetAll(x => x.ApplicationUserId == userId, includeProperties: "Product").ToList();
				shoppingCartVM.Order.ApplicationUserID = userId;
				shoppingCartVM.Order.OrderDate = DateTime.Now;
				ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepo.GetFirstOrDefault(x => x.Id == userId);
                shoppingCartVM.Order.ApplicationUserID = userId;
				foreach (ShoppingCart shoppingCart in shoppingCartVM.ShoppingCarts)
				{
					shoppingCartVM.Order.OrderTotal += (double)shoppingCart.Product.Price * shoppingCart.Count;
				}
				if (applicationUser.CompanyID.GetValueOrDefault() == 0)
				{
					//REGULAR CUSTOMER
					shoppingCartVM.Order.OrderStatus = SD.StatusPending;
					shoppingCartVM.Order.PaymentStatus = SD.PaymentStatusPending;
				}
				else
				{
					//COMPANY USER
					shoppingCartVM.Order.OrderStatus = SD.StatusApproved;
					shoppingCartVM.Order.PaymentStatus = SD.PaymentStatusDelayed;

				}
				_unitOfWork.OrderRepo.Add(shoppingCartVM.Order);
				_unitOfWork.Save();

				foreach (var item in shoppingCartVM.ShoppingCarts)
				{
					OrderDetail orderDetail = new OrderDetail()
					{
						ProductID = item.ProductId,
						OrderID = shoppingCartVM.Order.ID,
						Price = (double)item.Product.Price,
						Count = item.Count
					};
					_unitOfWork.OrderDetailRepo.Add(orderDetail);
					_unitOfWork.Save();
				}
                //Xử lý thanh toán
				if (applicationUser.CompanyID.GetValueOrDefault() == 0)
				{
					//REGULAR CUSTOMER
				}
				else
				{
					//COMPANY USER
				}

				return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCartVM.Order.ID });
			}
           
			return View("StartOrder",shoppingCartVM);
		}
        [HttpGet]
        public IActionResult OrderConfirmation(int id)
        {
            return View();
        }
	}
}
