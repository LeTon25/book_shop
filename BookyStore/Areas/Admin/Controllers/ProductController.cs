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
                productVM.product = unitOfWork.ProductRepo.GetFirstOrDefault(c => c.ID == id);
            }
            return View(productVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file = null)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(obj.product.ImageUrl))
                    {
                        string oldImageFilePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImageFilePath))
                        {
                            System.IO.File.Delete(oldImageFilePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\images\product\" + fileName;
                }
                if (obj.product.ID != 0)
                {
                    unitOfWork.ProductRepo.Update(obj.product);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    unitOfWork.ProductRepo.Add(obj.product);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            obj.selectCategoryItems = unitOfWork.CategoryRepo
             .GetAll().Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
            return View(obj);
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
            if (!string.IsNullOrEmpty(productToDelete.ImageUrl))
            {
                string oldImageFilePath = Path.Combine(wwwRootPath, productToDelete.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImageFilePath))
                {
                    System.IO.File.Delete(oldImageFilePath);
                }
            }
            unitOfWork.ProductRepo.Delete(productToDelete);
            unitOfWork.Save();
            return Json(new { success = true, message = "Xóa thành công sản phẩm" });
        }
        #endregion
    }
}
