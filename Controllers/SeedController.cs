using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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
    [Route("[controller]/[action]")]
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
        public async Task<ActionResult> Import()
        {
            var path = Path.Combine(_env.ContentRootPath, string.Format("Data/Source/worldcities.xlsx"));
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var ep = new ExcelPackage(stream);
            //get the first worksheet
            var ws = ep.Workbook.Worksheets[0];
            //initialize the record counters
            var nCountries = 0;
            var nCities = 0;

            #region Import all Countries
            //create a list containing all the countries already existing into the databse(it will be empty on first run)
            var lstCountries = _context.Countries.ToList();

            //iterates through all rows, skipping the first one
            for (int nRow = 2; nRow <= ws.Dimension.End.Row; nRow++)
            {
                var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];
                var name = row[nRow, 5].GetValue<string>();

                //does this country already exist in the database table?
                if (!lstCountries.Where(c => c.Name == name).Any())
                {
                    //create the country entity and fill it with data
                    var country = new Country
                    {
                        Name = name,
                        ISO2 = row[nRow, 6].GetValue<string>(),
                        ISO3 = row[nRow, 7].GetValue<string>()
                    };

                    // add the new country to the database DB context
                    _context.Countries.Add(country);

                    //store the country to retrieve its id later on
                    lstCountries.Add(country);
                    //increment the counter
                    nCountries++;
                }
            }
            //save all the countries into the database
            if (nCountries > 0)
            {
                await _context.SaveChangesAsync();

            }
            #endregion

            #region Import all cities                    
            //create a list containing all the cities already existing into the databse(it will be empty on first run)
            var lstCities = _context.Cities.ToList();

            //iterates through all rows, skipping the first one
            for (int nRow = 2; nRow <= ws.Dimension.End.Row; nRow++)
            {
                var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];
                var name = row[nRow, 1].GetValue<string>();
                var name_ASCII = row[nRow, 2].GetValue<string>();
                var countryName = row[nRow, 5].GetValue<string>();
                var lat = row[nRow, 3].GetValue<decimal>();
                var lon = row[nRow, 4].GetValue<decimal>();

                //retrieve country and countryId 
                var country = lstCountries.Where(c => c.Name == countryName).FirstOrDefault();
                var countryId = country.Id;
                //does this city already exist in the database table?
                if (!lstCities.Where(c => c.Name == name && c.Lat == lat && c.Lon == lon && c.CountryId == countryId).Any())
                {
                    //create the city entity and fill it with data
                    var city = new City
                    {
                        Name = name,
                        Name_ASCII = name_ASCII,
                        Lat = lat,
                        Lon = lon,
                        CountryId = countryId
                    };
                    // add the new city to the database DB context
                    _context.Cities.Add(city);

                    //increment the counter
                    nCities++;
                }
            }
            //save all the cities into the database
            if (nCities > 0)
            {
                await _context.SaveChangesAsync();
            }
            #endregion

            return new JsonResult(new { Cities = nCities, Countries = nCountries });
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
