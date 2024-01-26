using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;
using System.Diagnostics;
using System.Security.Claims;

namespace BookyStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
		{
			_logger = logger;
			this._unitOfWork = unitOfWork;
		}
		
		public IActionResult Index()
		{
			IEnumerable<Product> products = _unitOfWork.ProductRepo.GetAll();	
			return View(products);
		}
		[HttpGet]
		public IActionResult Details(int? id) 
		{
			if (id == null)
			{
				return NotFound();	
			}
			ShoppingCart sp = new ShoppingCart()
			{
				Product = _unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id, includeProperties: "Category"),
				ProductId = id.Value,
				Count = 1,
				ID = 0

			};
			Console.WriteLine(sp.ID);
			return View(sp);
		}
		[HttpPost]
		[Authorize]
        public IActionResult Details([Bind("ProductId","Count")]ShoppingCart shopping)
        {
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userID = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			shopping.ApplicationUserId = userID;
			ShoppingCart cartFromDb = _unitOfWork.ShoppingCartRepo.GetFirstOrDefault(x => x.ProductId == shopping.ProductId && x.ApplicationUserId == shopping.ApplicationUserId);
			if (cartFromDb == null) 
			{ 
				_unitOfWork.ShoppingCartRepo.Add(shopping);
			}
			else
			{
				cartFromDb.Count += shopping.Count;
				_unitOfWork.ShoppingCartRepo.Update(cartFromDb);
			}
            _unitOfWork.Save();
			TempData["StatusMessage"] = "Thêm sản phẩm vào giỏ hàng thành công";
			return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}