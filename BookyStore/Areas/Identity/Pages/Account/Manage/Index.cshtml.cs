// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace BookyStore.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        public string Username { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "SĐT")]
            public string PhoneNumber { get; set; }
            [DisplayName("Thành phố")]
            public string? City { get; set; }
            [DisplayName("Huyện")]
            public string? State { get; set; }
            [DisplayName("Tên đường")]
            public string? StreetAddress { get; set; }
            [DisplayName("Mã bưu điện")]
            public string? PostalCode { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            Username = userName;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                City = user.City,
                State = user.State,
                StreetAddress = user.StreetAddress, 
                PostalCode = user.PostalCode,  
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
           
            if (user == null)
            {
                return NotFound($"Không thể tải dữ liệu người dùng có ID '{_userManager.GetUserId(User)}'.");
            }
            var applicationUser = _unitOfWork.ApplicationUserRepo.GetFirstOrDefault(c=> c.Id == user.Id, tracked: false);
            if (applicationUser == null)
            {
                return NotFound($"Không thể tải dữ liệu người dùng có ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(applicationUser);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không thể tải dữ liệu người dùng có ID '{_userManager.GetUserId(User)}'.");
            }
            var applicationUser = _unitOfWork.ApplicationUserRepo.GetFirstOrDefault(c => c.UserName == user.UserName,tracked:false);
            if (applicationUser == null)
            {
                return NotFound($"Không thể tải dữ liệu người dùng có ID '{_userManager.GetUserId(User)}'.");
            }
            if (!ModelState.IsValid)
            {
                await LoadAsync(applicationUser);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Có lỗi xảy ra khi cập nhật SĐT";
                    return RedirectToPage();
                }
            }
            await _userManager.UpdateAsync(applicationUser);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Thông tin của bạn đã được cập nhật";
            return RedirectToPage();
        }
    }
}
