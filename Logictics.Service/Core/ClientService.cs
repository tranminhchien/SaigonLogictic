using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logictics.Service.Core
{
    public interface IClientService
    {
        List<OrderClientReponse> Clientlist(string phone);
        OrderClientItemReponse GetDetail(string id);

    }
    public class ClientService : IClientService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITimestampUtil timestampUtil;
        private IOrderRepo _orderRepo;

        public ClientService(IOrderRepo orderRepo, IUserRepo userRepo)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
        }

        public List<OrderClientReponse> Clientlist(string phone)
        {
            List<OrderClientReponse> OrderList = new List<OrderClientReponse>();
            var Entity = _userRepo.GetAll().Where(x => x.Phone == phone).FirstOrDefault();
            if (Entity != null)
            {
                var userId = Entity.Id;
                var orderList = _orderRepo.GetAll().Where(x => x.SenderId == userId || x.RecipientId == userId).ToList();
                foreach (var item in orderList)
                {
                    var model = new OrderClientReponse();
                    model.orderID = item.Id;

                    model.orderStatus = item.Status;

                    model.createDate = TimestampStaicClass.ConvertToDatetime(item.CreateDate);


                    OrderList.Add(model);
                }
            }

            return OrderList;
        }


        OrderClientItemReponse IClientService.GetDetail(string id)
        {
            try
            {
                var order = _orderRepo.Get(id);
                if(order == null)
                {
                    var item = new OrderClientItemReponse();
                    return item;
                }
                var sender = _userRepo.Get(order.SenderId);
                var recipient = _userRepo.Get(order.RecipientId);

                var result = new OrderClientItemReponse
                {

                    SenderName = sender.FullName,
                    SenderAddress = sender.Address,
                    SenderPhone = sender.Phone,
                    RecipientName = recipient.FullName,
                    RecipientAddress = recipient.Address,
                    RecipientPhone = recipient.Phone
                };




                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
