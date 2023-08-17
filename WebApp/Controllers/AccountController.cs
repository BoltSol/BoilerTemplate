using AppDomain.Common;
using AppService.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserLoggedinAppService _userLoggedinAppService;

        #region Parameters
        public AccountController(IUserLoggedinAppService userLoggedinAppService)
        {
            this._userLoggedinAppService = userLoggedinAppService;
        }
        #endregion

        #region SigIn

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoggedinInputDto input, string returnUrl = "")
        {
            var userLoggedin = await _userLoggedinAppService.UserAuthenticate(input);
            if (userLoggedin is null) return RedirectToAction("SignIn");

            var claimsIdentity = new ClaimsIdentity(userLoggedin.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7),
                    IsPersistent = input.IsPersistent,
                    RedirectUri = Constants.BaseUrl + "Account/Logout"
                });

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }

        #endregion
    }
}
