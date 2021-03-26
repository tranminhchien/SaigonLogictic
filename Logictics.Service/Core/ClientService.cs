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
            var Entity = _userRepo.GetAll().Where(x => x.Phone == phone).FirstOrDefault() ;
            if(Entity != null)
            {
                var userId = Entity.Id;
                var orderList = _orderRepo.GetAll().Where(x => x.SenderId == userId || x.RecipientId == userId).ToList();
                foreach (var item in orderList)
                {
                    var model = new OrderClientReponse();
                    model.orderID = item.Id;
                    
                    model.orderStatus = item.Status;
                   

                    OrderList.Add(model);
                }
            }
           
            return OrderList;
        }
    }
}
