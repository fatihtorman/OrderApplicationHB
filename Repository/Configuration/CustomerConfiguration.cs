using Pomelo.EntityFrameworkCore.MySql.Design;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(a => a.Id);

            
                builder.Property(c => c.Id).UseMySqlIdentityColumn();
             

        }
    }
}
