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
using Microsoft.AspNetCore.Authorization;

namespace BookyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var userList = unitOfWork.ApplicationUserRepo.GetAll().ToList();
            return View(userList);
        }
        public IActionResult RoleManagement(string userId)
        {
            RoleManagementVM roleManagementVM = new RoleManagementVM()
            {
                ApplicationUser = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == userId),
                RoleList = _roleManager.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
            };
            roleManagementVM.ApplicationUser.Role = _userManager.GetRolesAsync(unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u=>u.Id==userId)).GetAwaiter().GetResult().FirstOrDefault();
            return View(roleManagementVM);
        }
        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string oldRole = _userManager.GetRolesAsync(unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id)).GetAwaiter().GetResult().FirstOrDefault();
            ApplicationUser applicationUser = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id, tracked: true);

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                unitOfWork.ApplicationUserRepo.Update(applicationUser);
                unitOfWork.Save();
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            return Redirect("Index");
        }
        #region Api_Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = unitOfWork.ApplicationUserRepo.GetAll().ToList();
            foreach (var user in userList) 
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            }
            return Json(new { data = userList });
        }
        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody]string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
            {
                return  Json(new { success = false,message = "Có lỗi xảy ra khi Khóa/Gỡ khóa người dùng" });
            }
            user.LockoutEnd =  (user.LockoutEnd == null) ?  user.LockoutEnd=DateTime.Now.AddDays(1) : null;
            await _userManager.UpdateAsync(user); 
            return Json(new { success = true, message = "Khóa/Gỡ khóa thành công" });
        }
        #endregion

    }
}
