using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public PublisherController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                return View(new Publisher());
            }
            else
            {
                Publisher publisher = unitOfWork.PublisherRepo.GetFirstOrDefault(c => c.ID == id);
                return View(publisher);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {

                if (publisher.ID != 0 && publisher.ID != null)
                {
                    unitOfWork.PublisherRepo.Update(publisher);
                    unitOfWork.Save();
                }
                else
                {
                    unitOfWork.PublisherRepo.Add(publisher);
                    unitOfWork.Save();
                }
                TempData["StatusMessage"] = "Thêm/Cập nhật thành công";
                return Redirect("Index");
            }
            return View(publisher);

        }
        #region api_call
        public IActionResult GetAll()
        {
            var publishers = unitOfWork.PublisherRepo.GetAll();
            return Json(new { data = publishers });
        }
        public IActionResult Delete(int id)
        {
            var publisherToDelete = unitOfWork.PublisherRepo.GetFirstOrDefault(c => c.ID == id);
            if (publisherToDelete == null)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra hoặc nhà xuất bản không tồn tại" });
            }
            unitOfWork.PublisherRepo.Delete(publisherToDelete);
            unitOfWork.Save();
            return Json(new { success = true, message = "Xóa nhà xuất bản thành công sản phẩm" });
        }
        #endregion
    }
}
