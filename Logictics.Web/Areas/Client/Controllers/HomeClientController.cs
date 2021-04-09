using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logictics.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace Logictics.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeClientController : Controller
    {
        private IClientService clientService;

        public HomeClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Edit/5
        public IActionResult Search(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return RedirectToAction("Error400", "Error");
            }
            return View(clientService.Clientlist(phone));
        }

        // GET: OrderController/Edit/5
        public IActionResult OrderDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error400", "Error");
            }
            return View(clientService.GetDetail(id));
        }
    }
}
