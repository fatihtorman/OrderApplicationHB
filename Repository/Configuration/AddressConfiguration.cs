using Pomelo.EntityFrameworkCore.MySql.Design;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

                    builder
            .HasKey(a => a.Id);


            builder.Property(c => c.Id).UseMySqlIdentityColumn();

        }
    }
}
