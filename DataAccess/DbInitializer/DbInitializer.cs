using BookyStore.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeAsync()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                {
                    _db.Database.Migrate();
                }
                
                // Initialize roles and admin user if they don't exist
                if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin123@gmail.com",
                        Name = "Admin",
                        PhoneNumber = "1434567890",
                        StreetAddress = "Đào Sư Tích",
                        PostalCode = "12345",
                        City = "TPHCM",
                        State = "Nhà Bè",
                    },"Admin@1234").GetAwaiter().GetResult();

                    var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin123@gmail.com");
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex) 
            {
                // Log exception
            }
        }
  
    }
}
