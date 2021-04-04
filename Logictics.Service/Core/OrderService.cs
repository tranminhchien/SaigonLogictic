using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Entity.Models;
using Logictics.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logictics.Service.Core {

    public interface IOrderService {

        List<OrderViewModel> GetListActive();

        void CreateOrder(OrderCreateModel data);

        OrderEditViewModel GetDetail(string id);

        void UpdateOrder(OrderCreateModel data);
    }

    public class OrderService : IOrderService {
        private IOrderRepo orderRepo;
        private IStoreRepo storeRepo;
        private ICategoryProductRepo categoryProductRepo;
        private IUserRepo userRepo;
        private IOrderDetailRepo orderDetailRepo;

        public OrderService(IOrderRepo orderRepo, IStoreRepo storeRepo, ICategoryProductRepo categoryProductRepo, IUserRepo userRepo, IOrderDetailRepo orderDetailRepo) {
            this.orderRepo = orderRepo;
            this.storeRepo = storeRepo;
            this.categoryProductRepo = categoryProductRepo;
            this.userRepo = userRepo;
            this.orderDetailRepo = orderDetailRepo;
        }

        public List<OrderViewModel> GetListActive() {
            var result = new List<OrderViewModel>();
            var listOrder = orderRepo.GetAll().ToList();
            var listCategoryProduct = categoryProductRepo.GetAll().ToList();
            var listStore = storeRepo.GetAll().ToList();
            var listUser = userRepo.GetAll().ToList();
            var listOrderDetail = orderDetailRepo.GetAll().ToList();

            foreach (var item in listOrder) {
                //var category = listCategoryProduct.FirstOrDefault(c => c.Id == item.CategoryId);
                var store = listStore.FirstOrDefault(s => s.Id == item.StoreId);
                var sender = listUser.FirstOrDefault(u => u.Id == item.SenderId);
                var recipient = listUser.FirstOrDefault(u => u.Id == item.RecipientId);
                var orderDetail = listOrderDetail.Where(o => o.orderId == item.Id);
                var customerConfirm = listUser.FirstOrDefault(u => u.Id == item.CustomerConfirmId);

                var orderVM = new OrderViewModel();
                orderVM.MapOrderTblToOrderViewModel(item, store, sender, recipient, customerConfirm, orderDetail);

                result.Add(orderVM);
            }

            return result;
        }

        public void CreateOrder(OrderCreateModel data) {
            OrderTbl order = new OrderTbl();
            order.SenderId = data.SenderId;
            order.RecipientId = data.RecipientId;
            order.StoreId = data.StoreId;
            order.Notes = data.Note;
            order.TotalWeight = data.TotalWeight ?? 0;
            order.Status = "New";
            order.Shipment = data.Shipment;
            order.CreateDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
            order.ModifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
            order.PickupDate = TimestampStaicClass.ConvertTotimestamp(DateTime.Parse(data.PickupDate));
            order.Id = Guid.NewGuid().ToString();

            orderRepo.Create(order);

            var listOrderdetail = new List<OrderDetailTbl>();
            if (data.listOrdertail != null) {
                foreach (var item in data.listOrdertail) {
                    var orderdetail = new OrderDetailTbl();
                    orderdetail.createDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.modifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.status = "New";
                    orderdetail.description = item.description;
                    orderdetail.productCategoryId = item.productCategoryId;
                    orderdetail.id = Guid.NewGuid().ToString();
                    orderdetail.orderId = order.Id;
                    orderdetail.price = item.price;
                    orderdetail.productCode = item.productCode;
                    orderdetail.quality = item.quality;
                    orderdetail.weight = item.weight;
                    listOrderdetail.Add(orderdetail);
                }
            }

            orderDetailRepo.CreateMulti(listOrderdetail);
        }

        public OrderEditViewModel GetDetail(string id) {
            try {
                var order = orderRepo.Get(id);
                var ordertail = orderDetailRepo.GetListByOrderId(id).ToList();
                var store = storeRepo.GetAll().FirstOrDefault(c => c.Id == order.StoreId);
                var listuser = userRepo.GetAll().ToList();

                var result = new OrderEditViewModel();
                result.orderTbl = order;
                result.store = store;
                result.listDetailTbl = ordertail;
                result.Sender = listuser.FirstOrDefault(c => c.Id == order.SenderId);
                result.Recipient = listuser.FirstOrDefault(c => c.Id == order.RecipientId);

                return result;
            } catch (Exception e) {
                return null;
            }
        }

        public void UpdateOrder(OrderCreateModel data) {
            var order = orderRepo.Get(data.Id);
            if (order == null) {
                return;
            }

            order.SenderId = data.SenderId ?? "";
            order.RecipientId = data.RecipientId ?? "";
            order.StoreId = data.StoreId ?? "";
            order.Notes = data.Note ?? "";
            order.TotalWeight = data.TotalWeight ?? 0;
            order.Status = data.status ?? "New";
            order.Shipment = data.Shipment ?? "";
            order.ModifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
            //TODO Customer confirm
            orderRepo.SaveChanges();


            var listOldOrdertail = orderDetailRepo.GetListByOrderId(data.Id).ToList();
            orderDetailRepo.DeleteMulti(listOldOrdertail);
            var listOrderdetail = new List<OrderDetailTbl>();
            if (data.listOrdertail != null) {
                foreach (var item in data.listOrdertail) {
                    var orderdetail = new OrderDetailTbl();
                    orderdetail.createDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.modifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.status = "New";
                    orderdetail.description = item.description;
                    orderdetail.productCategoryId = item.productCategoryId;
                    orderdetail.id = String.IsNullOrEmpty(item.id) ? Guid.NewGuid().ToString() : item.id;
                    orderdetail.orderId = order.Id;
                    orderdetail.price = item.price;
                    orderdetail.productCode = item.productCode;
                    orderdetail.quality = item.quality;
                    orderdetail.weight = item.weight;
                    listOrderdetail.Add(orderdetail);
                }
            }

            orderDetailRepo.CreateMulti(listOrderdetail);
        }
    }
}