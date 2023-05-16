using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Data;
using OnlineAuction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SeedController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public SeedController(DataContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManger, IWebHostEnvironment env)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManger;
            _env = env;
        }         

           

        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            //setup the default role names
            string role_RegisteredUser = "RegisteredUser";
            string role_Admin = "Admin";

            //create the default roles(if they doesnot exist)
            if (await _roleManager.FindByNameAsync(role_RegisteredUser) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
            }

            if (await _roleManager.FindByNameAsync(role_Admin) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role_Admin));
            }

            //create list to track the newly added users
            var addedUserList = new List<ApplicationUser>();

            //check if the admin user already exist
            var email_Admin = "admin@email.com";
            if (await _userManager.FindByNameAsync(email_Admin) == null)
            {
                //create a new admin ApplicationUser account
                var user_Admin = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin
                };

                //insert the admin user into the DB
                await _userManager.CreateAsync(user_Admin, "MySecr3t$");

                //assign the "RegisteredUser" and "Admin" roles
                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await _userManager.AddToRoleAsync(user_Admin, role_Admin);

                //confirm the e-mail and remove lockout
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;

                //add the admin user to the added user list
                addedUserList.Add(user_Admin);
            }

            //check if the standard user already exist
            var email_User = "user@email.com";
            if (await _userManager.FindByNameAsync(email_User) == null)
            {
                //create a new standard ApplicationUser account
                var user_User = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };

                //insert the standard user into the DB
                await _userManager.CreateAsync(user_User, "MySecr3t$");

                //assign the "RegisteredUser"
                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);

                //confirm the e-mail and remove lockout
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;

                //add the admin user to the added user list
                addedUserList.Add(user_User);
            }

            //if we added at least one user, persist changes into the DB
            if (addedUserList.Count > 0)
            {
                await _context.SaveChangesAsync();
            }
            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList
            });
        }
    }
}
