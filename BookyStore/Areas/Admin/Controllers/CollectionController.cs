using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
using Models;
using Utility;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CollectionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CollectionController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment) 
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Collection());
            }
            else
            {
                Collection collection = unitOfWork.CollectionRepo.GetFirstOrDefault(c => c.ID == id);
                return View(collection);

            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Collection collection, IFormFile file = null)
        {
            if (ModelState.IsValid)
            {
                if (file == null && string.IsNullOrEmpty(collection.ImageUrl))
                {
                    ModelState.AddModelError("ImageUrl", "Vui lòng chọn hình ảnh");
                    return View(collection);
                }
                if (collection.ID != 0 && collection.ID != null)
                {
                    if (string.IsNullOrEmpty(collection.ImageUrl)) collection.ImageUrl = "";
                    unitOfWork.CollectionRepo.Update(collection);
                    unitOfWork.Save();
                }
                else
                {
                    collection.ImageUrl = "";
                    unitOfWork.CollectionRepo.Add(collection);
                    unitOfWork.Save();
                }

                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    if(string.IsNullOrEmpty(collection.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath,collection.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string collectionPath = @"images\collection\collection-" + collection.ID;
                    string finalPath = Path.Combine(wwwRootPath, collectionPath);
                    if (!Directory.Exists(finalPath))
                    {
                        Directory.CreateDirectory(finalPath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    collection.ImageUrl = @"\" + collectionPath + @"\" + fileName;
                    unitOfWork.CollectionRepo.Update(collection);
                    unitOfWork.Save();
                }
                TempData["StatusMessage"] = "Thêm/Cập nhật thành công";
                return Redirect("Index");
            }
            return View(collection);
        }

        /* Ajax call */
        #region Api_call
        [HttpGet]
        public IActionResult GetAll()
        {
            var collections = unitOfWork.CollectionRepo.GetAll();
            return Json(new {data = collections});
        }
        public IActionResult Delete(int id)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;
            var collectionToDelete = unitOfWork.CollectionRepo.GetFirstOrDefault(c => c.ID == id);
            if (collectionToDelete == null)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra hoặc sản phẩm không tồn tại" });
            }
            string collectionPath = @"images\collection\collection-" + id;
            string finalPath = Path.Combine(wwwRootPath, collectionPath);
            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
            unitOfWork.CollectionRepo.Delete(collectionToDelete);
            unitOfWork.Save();
            return Json(new { success = true, message = "Xóa bộ sách thành công sản phẩm" });
        }
        #endregion
    }
}
