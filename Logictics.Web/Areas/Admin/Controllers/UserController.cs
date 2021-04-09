
using Logictics.Service.Core;
using Logictics.Service.ViewModel;
using Logictics.Web.Auth;
using Logictics.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logictics.Web.Areas.Admin.Controllers
{
    [Area(Role.Admin)]
    [LogicticsAuthorize(Role.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View(userService.Clientlist());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdateStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error400", "Error");
            }

            userService.UpdateStatus(id);
            
            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(string id)
        {
            return View(userService.GetUserById(id));
        }

        [HttpPost]
        public JsonResult Edit(UserViewModel data)
        {
            try
            {
                userService.UpdateUser(data);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult Add(UserViewModel data)
        {
            try
            {
                userService.CreateUser(data);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}
