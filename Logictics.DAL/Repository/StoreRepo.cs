using Logictics.DAL.EFContext;
using Logictics.DAL.Infrastructure;
using Logictics.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.DAL.Repository
{
    public interface IStoreRepo : IRepo<StoreTbl>
    {

    }
    public class StoreRepo : Repo<StoreTbl>, IStoreRepo
    {
        public StoreRepo(DbContextOptions<LogicticsDbContext> options) : base(options)
        {

        }
    }
}
