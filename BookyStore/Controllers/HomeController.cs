using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;
using Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using Utility;

namespace BookyStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private const int ITEM_PER_PAGE = 8;

		public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
		{
			_logger = logger;
			this._unitOfWork = unitOfWork;
		}
		
		public IActionResult Index(string? search,[FromQuery]int? page)
		{
			
			IEnumerable<Product> products = _unitOfWork.ProductRepo.GetAll(includeProperties:"ProductImages").Skip(2);	
			int totalProducts = products.Count();
			int totalPages =(int) Math.Ceiling((double)totalProducts / ITEM_PER_PAGE);
			if (page == null || page < 1)
				page = 1;
			if  (page > totalPages)
			{
				page = totalPages;
			}
			products = products.Skip(ITEM_PER_PAGE * (page.Value-1)).Take(ITEM_PER_PAGE);	
			HomeModel homeModel = new HomeModel
			{
				Products = products,	
				Paging = new PagingModel
				{
					totalPages = totalPages,
					currentPage = page.Value,
					urlGenerator = (int? p) => Url.Action("Index","Home",new {page = p})
				}
			};
			return View(homeModel);
		}
		[HttpGet]
		public IActionResult Details(int? id) 
		{
			if (id == null)
			{
				return NotFound();	
			}
			Product currentProduct = _unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id, includeProperties: "ProductImages,ProductCategories");
			foreach(var productCategory in currentProduct.ProductCategories)
			{
				productCategory.Category = _unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.ID == productCategory.CategoryID);
			}	

            ShoppingCart sp = new ShoppingCart()
			{
				Product = currentProduct,
				ProductId = id.Value,
				Count = 1,
				ID = 0

			};
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
			string count = _unitOfWork.ShoppingCartRepo.GetAll(c => c.ApplicationUserId == userID).Count().ToString();
			HttpContext.Session.SetString(SD.CART_KEY,count);
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