using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondTask.Models;

namespace SecondTask.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context;
        private readonly IPersonRepository repository;
        public HomeController(IPersonRepository repository, DataContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accountId = this.User.Claims.Single(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var name = this.User.Claims.Single(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
                var surname = this.User.Claims.Single(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value;
                var userInBase = await repository.FindByAccountIdAsync(accountId);

                ViewBag.UserName = name + " " + surname;
                if (userInBase == null)
                {
                    Person p = new Person()
                    {
                        AccountId = accountId,
                        AccountName = name + " " + surname,
                        IsBlocked = false,
                        LastIn = DateTime.Now
                    };
                    await repository.AddAsync(p);
                }
                else if (userInBase.IsBlocked)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Auth");
                }
                else await repository.LastInUpdate(accountId);
            }

            return View(await repository.GetAllPersons());
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string[] id)
        {
            try
            {
                await repository.DeleteUserAsync(id);
                var f = id.Single(x => x == User.Claims.Single(q =>
                q.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Auth");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser(string[] id)
        {
            await repository.BlockUserAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
