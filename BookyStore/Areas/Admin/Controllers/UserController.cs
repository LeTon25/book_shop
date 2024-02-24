using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
using Models;
using BookyStore.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager; 
        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,AppDbContext db,UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this._db = db;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var userList = unitOfWork.ApplicationUserRepo.GetAll(includeProperties: "Company").ToList();
            return View(userList);
        }
        public IActionResult RoleManagement(string userId)
        {
            var userRole = _db.UserRoles.FirstOrDefault(x => x.UserId == userId);

            RoleManagementVM roleManagementVM = new RoleManagementVM()
            {
                ApplicationUser = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == userId,includeProperties: "Company"),
                RoleList = _db.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = unitOfWork.CompanyRepo.GetAll().Select(c=>new SelectListItem
                {
                    Text = c.Name,
                    Value = c.ID.ToString()
                })
            };
            roleManagementVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u=>u.Id == userRole.RoleId).Name;
            return View(roleManagementVM);
        }
        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string roleId = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagementVM.ApplicationUser.Id).RoleId;
            string oldRole = _db.Roles.FirstOrDefault(u => u.Id == roleId).Name;

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                ApplicationUser applicationUser = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id,tracked:true);
                if (roleManagementVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyID = roleManagementVM.ApplicationUser.CompanyID;
                }
                if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyID = null;
                }
                _db.SaveChanges();
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }    
            return Redirect("Index");
        }
        #region Api_Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = unitOfWork.ApplicationUserRepo.GetAll(includeProperties:"Company").ToList();
            foreach (var item in userList) 
            {
                string userRoleID = _db.UserRoles.FirstOrDefault(c => c.UserId == item.Id).RoleId;
                string role = _db.Roles.FirstOrDefault(c => c.Id == userRoleID).Name;
                item.Role = role;
                if(item.Company == null)
                {
                    item.Company = new Company()
                    {
                        Name = ""
                    };   
                }
            }
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string? id)
        {
            var user = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == id,tracked:true);
            if (user == null) 
            {
                return  Json(new { success = false,message = "Có lỗi xảy ra khi Khóa/Gỡ khóa người dùng" });
            }
            user.LockoutEnd =  (user.LockoutEnd == null) ?  user.LockoutEnd=DateTime.Now.AddDays(1) : null;
            unitOfWork.Save();
            return Json(new { success = true, message = "Khóa/Gỡ khóa thành công" });

        }
        #endregion

    }
}
