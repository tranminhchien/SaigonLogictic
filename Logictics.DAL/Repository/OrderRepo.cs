using Logictics.DAL.EFContext;
using Logictics.DAL.Infrastructure;
using Logictics.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logictics.DAL.Repository
{

    public interface IOrderRepo : IRepo<OrderTbl>
    { 
    }
    public class OrderRepo : Repo<OrderTbl>, IOrderRepo
    {

        public OrderRepo(DbContextOptions<LogicticsDbContext> options) : base(options)
        {

        }

    }
}
