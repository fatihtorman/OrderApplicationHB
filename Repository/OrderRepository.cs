using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderAppDbContext dataContext) : base(dataContext)
        {

        }

        public Order GetById(int id)
        {
            return Context.Set<Order>()
                          .Include(x => x.Address)
                          .Include(x => x.Customer)
                          .Include(x => x.Product)
                          .FirstOrDefault(x => x.Id == id);
        }
    }
}
