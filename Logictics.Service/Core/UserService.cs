using Common.Utils;
using Logictics.DAL.Repository;
using Logictics.Entity.Models;
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
        UserViewModel GetUserById(string id);
        int UpdateStatus(string id);
        void CreateUser(UserViewModel data);
        int UpdateUser(UserViewModel data);
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
                model.Address = item.Address;
                model.Phone = item.Phone;
                ClientList.Add(model);
            }
            return ClientList;
        }

        public void CreateUser(UserViewModel data)
        {
            var user = new UserAdmin {
                Id = Guid.NewGuid().ToString(),
                UserName = data.UserName,
                Role = "CLIENT",
                Status = "ACTIVE",
                FullName = data.FullName,
                Phone = data.Phone,
                Address = data.Address,
                CreateDate = TimestampStaicClass.ConvertTotimestamp(DateTime.UtcNow)
            };

            _userRepo.Create(user);

        }

        public UserViewModel GetUserById(string id)
        {
            var user = _userRepo.Get(id);
            var result = new UserViewModel
            {
                Id = user.Id,
                Address = user.Address,
                Phone = user.Phone,
                UserName = user.UserName,
                FullName = user.FullName
            };

            return result;
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

        public int UpdateUser(UserViewModel data)
        {
            try
            {
                var Entity = _userRepo.Get(data.Id);
                if (Entity == null)
                {
                    return 404;
                }
                if (!String.IsNullOrEmpty(data.FullName))
                {
                    Entity.FullName = data.FullName;
                }
                if (!String.IsNullOrEmpty(data.Phone))
                {
                    Entity.Phone = data.Phone;
                }
                if (!String.IsNullOrEmpty(data.Address))
                {
                    Entity.Address = data.Address;
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
