using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess.Mappings
{
    public class BloodTypeMapping:IEntityTypeConfiguration<BloodType>
    {
        public void Configure(EntityTypeBuilder<BloodType> builder)
        {
            builder
                .HasKey(m => m.BloodGroupName);

            builder
                .HasOne(m => m.BloodStorage)
                .WithOne(m => m.BloodType)
                .HasForeignKey<BloodStorage>(m => m.BloodGroupName);
        }
    }
}
