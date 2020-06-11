using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess.Mappings
{
    public class BloodStorageMapping:IEntityTypeConfiguration<BloodStorage>
    {
        public void Configure(EntityTypeBuilder<BloodStorage> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.StoredAmmount)
                .IsRequired();
        }
    }
}
