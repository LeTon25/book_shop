using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categoryList = unitOfWork.CategoryRepo.GetAll().ToList();
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                bool isCategoryExist = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.Name.ToLower().Equals(obj.Name.ToLower())) != null;
                if (isCategoryExist)
                {
                    ModelState.AddModelError("TongHop", "Tên đã có trong hệ thống");
                }
                else
                {
                    unitOfWork.CategoryRepo.Add(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                bool isCategoryExist = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.Name.ToLower().Equals(obj.Name.ToLower()) && c.ID != obj.ID) != null;
                if (isCategoryExist)
                {
                    ModelState.AddModelError("TongHop", "Tên đã có trong hệ thống");
                }
                else
                {
                    unitOfWork.CategoryRepo.Update(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = unitOfWork.CategoryRepo.GetFirstOrDefault(c => c.ID == id);
            if (obj == null)
            {
                return NotFound();
            }
            unitOfWork.CategoryRepo.Delete(obj);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
