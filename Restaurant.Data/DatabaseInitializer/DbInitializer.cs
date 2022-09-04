using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(DatabaseContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (! _roleManager.RoleExistsAsync(StaticDetail.KitchenRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.KitchenRole));
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.ManagerRole));
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.FrontDeskRole));
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.CustomerRole));

                _userManager.CreateAsync(new UserInfo
                {
                    UserName = "noobmaster69@gmail.com",
                    Email = "noobmaster69@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Remy",
                    LastName = "Hardus"
                }, "Noob_master69").GetAwaiter().GetResult();

                UserInfo user = _db.UserInfos.FirstOrDefault(u => u.Email == "noobmaster69@gmail.com");

                _userManager.AddToRoleAsync(user, StaticDetail.ManagerRole).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
