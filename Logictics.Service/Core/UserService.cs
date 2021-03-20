using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Logictics.Service.Core
{

    public interface IUserService
    {
        List<UserViewModel> Clientlist();
        int UpdateStatus(string id);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITimestampUtil timestampUtil;

        public UserService(IUserRepo userRepo, ITimestampUtil timestampUtil)
        {
            _userRepo = userRepo;
            this.timestampUtil = timestampUtil;
        }

        public List<UserViewModel> Clientlist()
        {
            List<UserViewModel> ClientList = new List<UserViewModel>();
            var clientlist = _userRepo.GetAll().Where(x => x.Role == "CLIENT").ToList();
            foreach (var item in clientlist)
            {
                var model = new UserViewModel();
                model.Id = item.Id;
                model.UserName = item.UserName;
                model.Status = item.Status;
                model.CreateDate = timestampUtil.ConvertToDatetime((Convert.ToDouble(item.CreateDate)));

                ClientList.Add(model);
            }
            return ClientList;
        }

        public int UpdateStatus(string id)
        {
            try
            {
                var Entity = _userRepo.Get(id);
                if (Entity == null)
                {
                    return 404;
                }
                if(Entity.Status == "INACTIVE")
                {
                    Entity.Status = "ACTIVE";
                }
                else
                {
                    Entity.Status = "INACTIVE";
                }

                _userRepo.Update(Entity);
                return 200;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return 500;
            }
        }
    }
}
