using System;
using Common.Utils;
using Logictics.Service.Core;
using Logictics.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Logictics.Web.Models;

namespace Logictics.Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IEncryptionUtil _encryptionUtil;

        public AuthController(IAuthService authService, IEncryptionUtil encryptionUtil)
        {
            _authService = authService;
            _encryptionUtil = encryptionUtil;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserViewModel model)
        {
            UserViewModel user = _authService.GetLoginUser(model.UserName.Trim(), _encryptionUtil.EncodeSHA1(model.Password));
            try
            {
                if (user == null)
                {
                    ModelState.AddModelError("", "UserName or Password not exist");
                    return View("Login", user);
                }
                _ = CreateAuthenticationTicket(user);
                return user.Role.ToUpper() switch
                {
                    Role.Admin => RedirectToAction("Index", "User", new { Area = "Admin" }),
                    //Role.Editor => RedirectToAction("Index", "Dashboard", new { Area = "Editor" }),
                    //Role.Sales => RedirectToAction("Index", "Dashboard", new { Area = "Sales" }),
                    //Role.Client => RedirectToAction("Index", "Dashboard", new { Area = "Client" }),
                    //Role.Manager => RedirectToAction("Index", "Dashboard", new { Area = "Manager" }),
                    _ => View("Login", user),
                };
            }
            catch (Exception)
            {
                return View("Login", user);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
