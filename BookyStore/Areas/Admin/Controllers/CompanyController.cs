using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
using Models;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var companyList = unitOfWork.CompanyRepo.GetAll().ToList();
            return View(companyList);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> selectCategoryItems = unitOfWork.CategoryRepo
             .GetAll().Select(c => new SelectListItem(text: c.Name, value: c.ID.ToString()));
            if (id == null || id == 0)
            {
                //Đang tạo mới 
                return View(new Company());
            }
            else
            {
                Company companyObj = unitOfWork.CompanyRepo.GetFirstOrDefault(c => c.ID == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.ID != 0)
                {
                    unitOfWork.CompanyRepo.Update(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    unitOfWork.CompanyRepo.Add(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }
        #region Api_Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = unitOfWork.CompanyRepo.GetAll().ToList();
            return Json(new { data = companyList });
        }
        public IActionResult Delete(int id)
        {
            var companyToDelete = unitOfWork.CompanyRepo.GetFirstOrDefault(c => c.ID == id);
            if (companyToDelete == null)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra hoặc công ty không tồn tại" });
            }
            unitOfWork.CompanyRepo.Delete(companyToDelete);
            unitOfWork.Save();
            return Json(new { success = true, message = "Xóa công ty sản phẩm" });
        }
        #endregion

    }
}
