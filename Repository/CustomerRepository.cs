using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderAppDbContext dataContext) : base(dataContext)
        {

        }

        public Customer GetById(int id)
        {
            return Context.Set<Customer>()
                          .Include(x => x.Address)
                          .FirstOrDefault(x => x.Id == id);
        }
    }
}
