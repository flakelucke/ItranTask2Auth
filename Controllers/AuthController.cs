using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondTask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SecondTask.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationSchemeProvider  authProvider;

        public AuthController (IAuthenticationSchemeProvider authProvider){
           this.authProvider=authProvider; 
        }
        public async Task<IActionResult> Login()
        {   
            var allSchemeProvider = (await authProvider.GetAllSchemesAsync())
                .Select(x=>x.DisplayName).Where(x=>!String.IsNullOrEmpty(x));
            return View(allSchemeProvider);
        }

        public IActionResult SignIn (String provider)
        {   AuthenticationProperties auth = new AuthenticationProperties {RedirectUri = "/"};
            return Challenge(auth,provider);
        }
         public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Auth");
        }
    }
}
