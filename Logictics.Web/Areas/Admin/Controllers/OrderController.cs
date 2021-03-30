using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Service.Core;
using Logictics.Service.ViewModel;
using Logictics.Web.Auth;
using Logictics.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logictics.Web.Areas.Admin.Controllers {

    [Area(Role.Admin)]
    //[LogicticsAuthorize(Role.Admin)]
    public class OrderController : Controller {

        #region DI

        private IOrderService orderService;
        private IOrderDetailService orderDetailService;
        private IStoreRepo storeRepo;
        private ICategoryProductRepo categoryProductRepo;
        private IUserRepo userRepo;

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IStoreRepo storeRepo, ICategoryProductRepo categoryProductRepo, IUserRepo userRepo) {
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.storeRepo = storeRepo;
            this.categoryProductRepo = categoryProductRepo;
            this.userRepo = userRepo;
        }

        #endregion DI

        // GET: OrderController
        public ActionResult Index() {
            var model = orderService.GetListActive();
            return View(model);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create() {
            var listCategory = categoryProductRepo.GetAll().ToList();
            var listStore = storeRepo.GetAll().ToList();
            var listUser = userRepo.GetAll().ToList();

            ViewBag.ListCategory = new SelectList(listCategory, "Id", "Name");
            ViewBag.ListStore = new SelectList(listStore, "Id", "Name");
            ViewBag.ListSender = new SelectList(listUser, "Id", "UserName");
            ViewBag.ListRecipient = new SelectList(listUser, "Id", "UserName");
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        public JsonResult Create(OrderCreateModel data) {
            try {
                orderService.CreateOrder(data);
                return Json(true);
            } catch {
                return Json(false);
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(string id) {
            try {
                var result = orderService.GetDetail(id);
                if (result == null) {
                    return Redirect("404NotFound");
                }
                var listUser = userRepo.GetAll().ToList();
                var listCategory = categoryProductRepo.GetAll().ToList();
                var listStore = storeRepo.GetAll().ToList();

                ViewBag.ListCategory = new SelectList(listCategory, "Id", "Name");
                ViewBag.ListStore = new SelectList(listStore, "Id", "Name", result.store != null ? result.store.Id : "");
                ViewBag.ListSender = new SelectList(listUser, "Id", "UserName", result.Sender != null ? result.Sender.Id : "");
                ViewBag.ListRecipient = new SelectList(listUser, "Id", "UserName", result.Recipient != null ? result.Recipient.Id : "");
                ViewBag.ListStatus = new ListSelectOptionModel().CreateListSelectStatusOrder(result.orderTbl.Status);

                result.listCategoryTbl = listCategory;
                return View(result);
            } catch (Exception e) {
                return View("404NotFound");
            }
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        public JsonResult Edit(OrderCreateModel data) {
            try {
                orderService.UpdateOrder(data);
                return Json(true);
            } catch {
                return Json(false);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}