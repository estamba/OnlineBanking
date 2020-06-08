using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Domain;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using OnlineBanking.Web.Models;

namespace OnlineBanking.Web.Controllers
{
  
    public class AccountController : Controller
    {
       
        private readonly IAdminService adminService;
        private readonly OnlineBankingDbContext dbContext;
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public AccountController(IAdminService adminService, OnlineBankingDbContext dbContext, IAccountService accountService, IUserService userService)
        {
            this.adminService = adminService;
            this.dbContext = dbContext;
            this.accountService = accountService;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateAccountStatus(Guid AccountId)
        {
            var account = dbContext.Accounts.FirstOrDefault(a => a.Id == AccountId);
            if (account.status == AccountStatus.Active)
                account.status = AccountStatus.Suspended;
            else
                account.status = AccountStatus.Active;

            dbContext.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(AddUserModel addUser)
        {
            if (!ModelState.IsValid) return View(addUser);

            UserAddModel user = new UserAddModel()
            {
                IntialBalance = addUser.IntialBalance,
                Name = addUser.Name,
                Password = addUser.Password,
                UserName = addUser.UserName,
                AdminUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

          var result=   userService.AddUser(user);
            if(!result.IsSuccessFul)
            {
                ModelState.AddModelError("", result.Data);
                return View(addUser);
            }
            return RedirectToAction("UserList", "Account");
            
        }

        public IActionResult UserList()
        {
           var users =  dbContext.Users.Include(u => u.Account).Select(u => new UserModel()
            {
                AccountNumber = u.Account.Number,
                Balance = u.Account.Balance,
                Name = u.Name,
                UserName = u.UserName,
                AccountStatus =u.Account.status,
                AccountId = u.Account.Id,
                Id = u.Id
            }).ToList();
            return View(new UserListViewModel() { Users = users });
        }
        [HttpPost]
        public IActionResult AddAdmin([FromBody]AdminAddModel model)
        {
            adminService.AddAdmin(model);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) return View(loginVm);
            if(accountService.Login(loginVm.UserName, loginVm.Password))
            {
                var user = dbContext.Users.FirstOrDefault(u => u.UserName == loginVm.UserName);
              await SetAuthenticationCookie(user);
            }

            return RedirectToAction("Index", "Home");
        }
       
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        private async Task SetAuthenticationCookie(User user)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", user.Name),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(6),


                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTime.Now,
                // The time at which the authentication ticket was issued.

                RedirectUri = "account/login"

            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}