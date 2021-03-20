using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Entity.Models;
using Logictics.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logictics.Service.Core
{
    public interface IOrderService
    {
        List<OrderViewModel> GetListActive();

        void CreateOrder(OrderCreateModel data);
    }

    public class OrderService : IOrderService
    {
        private IOrderRepo orderRepo;
        private IStoreRepo storeRepo;
        private ICategoryProductRepo categoryProductRepo;
        private IUserRepo userRepo;
        private IOrderDetailRepo orderDetailRepo;

        public OrderService(IOrderRepo orderRepo, IStoreRepo storeRepo, ICategoryProductRepo categoryProductRepo, IUserRepo userRepo, IOrderDetailRepo orderDetailRepo)
        {
            this.orderRepo = orderRepo;
            this.storeRepo = storeRepo;
            this.categoryProductRepo = categoryProductRepo;
            this.userRepo = userRepo;
            this.orderDetailRepo = orderDetailRepo;
        }

        public List<OrderViewModel> GetListActive()
        {
            var result = new List<OrderViewModel>();
            var listOrder = orderRepo.GetAll().ToList();
            var listCategoryProduct = categoryProductRepo.GetAll().ToList();
            var listStore = storeRepo.GetAll().ToList();
            var listUser = userRepo.GetAll().ToList();
            var listOrderDetail = orderDetailRepo.GetAll().ToList();

            foreach(var item in listOrder)
            {
                //var category = listCategoryProduct.FirstOrDefault(c => c.Id == item.CategoryId);
                var store = listStore.FirstOrDefault(s => s.Id == item.StoreId);
                var sender = listUser.FirstOrDefault(u => u.Id == item.SenderId);
                var recipient = listUser.FirstOrDefault(u => u.Id == item.RecipientId);
                var orderDetail = listOrderDetail.Where(o => o.OrderId == item.Id);
                var customerConfirm = listUser.FirstOrDefault(u => u.Id == item.CustomerConfirmId);

                var orderVM = new OrderViewModel();
                orderVM.MapOrderTblToOrderViewModel(item, store, sender, recipient, customerConfirm, orderDetail);

                result.Add(orderVM);
            }

            return result;
        }

        public void CreateOrder(OrderCreateModel data)
        {
            OrderTbl order = new OrderTbl();
            order.SenderId = data.SenderId;
            order.RecipientId = data.RecipientId;
            order.StoreId = data.StoreId;
            order.Notes = data.Note;
            order.TotalWeight = data.TotalWeight ?? 0;
            order.Status = "ACTIVE";
            order.Shipment = data.Shipment;
            order.CreateDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
            order.ModifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
            order.Id = Guid.NewGuid().ToString();

            orderRepo.Create(order);

            var listOrderdetail = new List<OrderDetailTbl>();
            if(data.listOrdertail != null)
            {
                foreach( var item in data.listOrdertail)
                {
                    var orderdetail = new OrderDetailTbl();
                    orderdetail.CreateDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.ModifyDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow);
                    orderdetail.Status = "ACTIVE";
                    orderdetail.Description = item.description;
                    orderdetail.ProductCategoryId = item.categoryId;
                    orderdetail.Id = Guid.NewGuid().ToString();
                    orderdetail.OrderId = order.Id;
                    orderdetail.Price = item.price;
                    orderdetail.ProductCode = item.productCode;
                    orderdetail.Quality = item.quality;
                    orderdetail.Weight = item.quality;
                    listOrderdetail.Add(orderdetail);
                }
            }

            orderDetailRepo.CreateMulti(listOrderdetail);
        }
    }
}
