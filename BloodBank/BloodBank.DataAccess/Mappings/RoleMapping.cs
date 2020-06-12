using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess.Mappings
{
    public class RoleMapping:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.IsBloodDonator)
                .IsRequired();

            builder
                .Property(m => m.IsWorker)
                .IsRequired();
        }
    }
}
