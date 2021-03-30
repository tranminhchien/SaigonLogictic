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

    public interface IOrderDetailRepo : IRepo<OrderDetailTbl>
    {
        IQueryable<OrderDetailTbl> GetListByOrderId(string orderId);
    }
    public class OrderDetailRepo : Repo<OrderDetailTbl>, IOrderDetailRepo
    {

        public OrderDetailRepo(DbContextOptions<LogicticsDbContext> options) : base(options)
        {

        }

        public IQueryable<OrderDetailTbl> GetListByOrderId(string orderId)
        {
            return Table.Where(x => x.orderId == orderId);
        }
    }
}
