using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using NuGet.Packaging.Core;
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
			var productList = unitOfWork.ProductRepo.GetAll().ToList();
			return View(productList);
		}
		[HttpGet]
		public IActionResult Upsert(int? id)
		{
			IEnumerable<Category> selectCategoryItems = unitOfWork.CategoryRepo
			 .GetAll();
			IEnumerable<SelectListItem> selectCollectionItems = unitOfWork.CollectionRepo.GetAll()
						.Select(c => new SelectListItem(text:c.Name,value: c.ID.ToString()));
			IEnumerable<SelectListItem> selectPublisherItems = unitOfWork.PublisherRepo.GetAll()
				.Select(c=>new SelectListItem(text:c.Name,value:c.ID.ToString()));
			ProductVM productVM = new ProductVM
			{
				product = new Product(),
				selectCategoryItems = selectCategoryItems,
				selectCollectionItems = selectCollectionItems,	
				selectPublisherItems = selectPublisherItems
			};
			if (id == null || id == 0)
			{
				//Đang tạo mới 
				return View(productVM);
			}
			else
			{
				productVM.product = unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id,includeProperties: "ProductImages,ProductCategories");
			}
			return View(productVM);
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Upsert(ProductVM obj, List<IFormFile> files = null,List<int> categoryIds = null)
		{
			if (ModelState.IsValid && categoryIds.Count() != 0)
			{
                if (obj.product.ID != 0 && obj.product.ID != null)
                {
                    unitOfWork.ProductRepo.Update(obj.product);
                    unitOfWork.Save();
                }	
                else
                {
                    unitOfWork.ProductRepo.Add(obj.product);
                    unitOfWork.Save();
                }
				/* Xử lí thêm hình ảnh */
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
				/* Xử lí thể loại */
				obj.product.ProductCategories = unitOfWork.ProductCategoryRepo.GetAll(c=>c.ProductID == obj.product.ID).ToList();
				// kiểm tra nếu sản phẩm chưa có id thể loại nào thì thêm vào 
				List<ProductCategory> productCategories = new List<ProductCategory>();
				foreach(int categoryId in categoryIds)
				{
					if(obj.product.ProductCategories.FirstOrDefault(c=>c.CategoryID == categoryId) == null)
					{
						ProductCategory productCategory = new ProductCategory() { CategoryID = categoryId ,ProductID = obj.product.ID };
						productCategories.Add(productCategory);
					}
				}
				unitOfWork.ProductCategoryRepo.AddRange(productCategories);
				unitOfWork.Save();
				// Kiểm tra xóa những id thể loại cũ (không được chọn)
				List<ProductCategory> oldProductCategory = obj.product.ProductCategories.ToList();

                foreach (var category in oldProductCategory)
				{
					if(categoryIds.FirstOrDefault(c=>c == category.CategoryID)==0)
					{
						unitOfWork.ProductCategoryRepo.Delete(category);
						unitOfWork.Save();
					}	
				}	
                return Redirect("Index");
            }
			if(categoryIds.Count() == 0)
			{
				ModelState.AddModelError("product.ProductCategories","Vui lòng chọn thể loại");
			}	
			obj.product.ProductImages = unitOfWork.ProductImageRepo.GetAll(c=>c.ProductID == obj.product.ID).ToList();
			obj.selectCategoryItems = unitOfWork.CategoryRepo
			 .GetAll();
			obj.selectCollectionItems = unitOfWork.CollectionRepo.GetAll()
                .Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
            obj.selectPublisherItems = unitOfWork.PublisherRepo.GetAll()
                .Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
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
			var productList = unitOfWork.ProductRepo.GetAll(includeProperties: "Publisher,ProductCategories").ToList();
			foreach (var product in productList)
			{
				foreach (var category in product.ProductCategories)
				{
					category.Category = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.ID == category.CategoryID);
					category.Product = null;
				}
			}
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

