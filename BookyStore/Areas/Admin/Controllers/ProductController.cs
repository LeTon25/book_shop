using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using Utility;

namespace BookyStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			this.unitOfWork = unitOfWork;
			this.webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			var productList = unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
			return View(productList);
		}
		[HttpGet]
		public IActionResult Upsert(int? id)
		{
			IEnumerable<SelectListItem> selectCategoryItems = unitOfWork.CategoryRepo
			 .GetAll().Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
			ProductVM productVM = new ProductVM
			{
				product = new Product(),
				selectCategoryItems = selectCategoryItems
			};
			if (id == null || id == 0)
			{
				//Đang tạo mới 
				return View(productVM);
			}
			else
			{
				productVM.product = unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id,includeProperties: "ProductImages");
			}
			return View(productVM);
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Upsert(ProductVM obj, List<IFormFile> files = null)
		{
			if (ModelState.IsValid)
			{
                if (obj.product.ID != 0)
                {
                    unitOfWork.ProductRepo.Update(obj.product);
                    unitOfWork.Save();
                }	
                else
                {
                    unitOfWork.ProductRepo.Add(obj.product);
                    unitOfWork.Save();
                }
             
				string wwwRootPath = webHostEnvironment.WebRootPath;
				List<ProductImage> productImages = new List<ProductImage>();
				if (files != null)
				{
					foreach (IFormFile file in files)
					{
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
						string productPath = @"images\product\product-" + obj.product.ID;
						string finalPath = Path.Combine(wwwRootPath, productPath);
						if (!Directory.Exists(finalPath))
						{
							Directory.CreateDirectory(finalPath);
						}
						using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
						{
							file.CopyTo(fileStream);
						}
						ProductImage productImage = new ProductImage()
						{
							ImageUrl = @"\" + productPath + @"\" + fileName,
							ProductID = obj.product.ID,
						};
						productImages.Add(productImage);
					}
					unitOfWork.ProductImageRepo.AddRange(productImages);
					unitOfWork.Save();
				}
                TempData["StatusMessage"] = "Thêm/Cập nhật thành công";
                return Redirect("Index");
            }
			obj.selectCategoryItems = unitOfWork.CategoryRepo
			 .GetAll().Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
			return View(obj);
		}

		public IActionResult DeleteImage(int? imageid)
		{
			var imageToBeDeleted = unitOfWork.ProductImageRepo.GetFirstOrDefault(c => c.ID == imageid);
			int productID = imageToBeDeleted.ProductID;

			if(imageToBeDeleted != null)
			{
				if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
				{
					var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}
				unitOfWork.ProductImageRepo.Delete(imageToBeDeleted);
				unitOfWork.Save();

				TempData["StatusMessage"] = "Xóa hình ảnh thành công";
			}
			return RedirectToAction(nameof(Upsert), new {id = productID });
		}
		#region Api_Call
		[HttpGet]
		public IActionResult GetAll()
		{
			var productList = unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = productList });
		}
		public IActionResult Delete(int id)
		{
			string wwwRootPath = webHostEnvironment.WebRootPath;
			var productToDelete = unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id);
			if (productToDelete == null)
			{
				return Json(new { success = false, message = "Có lỗi xảy ra hoặc sản phẩm không tồn tại" });
			}
            string productPath = @"images\product\product-" + id;
            string finalPath = Path.Combine(wwwRootPath, productPath);
			if (Directory.Exists(finalPath))
			{
				string[] filePaths = Directory.GetFiles(finalPath);
				foreach (string filePath in filePaths) 
				{
					System.IO.File.Delete(filePath);
				}
				Directory.Delete(finalPath);
			}	
            unitOfWork.ProductRepo.Delete(productToDelete);
			unitOfWork.Save();
			return Json(new { success = true, message = "Xóa thành công sản phẩm" });
		}
		#endregion
	}
}

