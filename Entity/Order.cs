using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Order
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string Status { get; set; }

        public Address Address { get; set; }

        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
