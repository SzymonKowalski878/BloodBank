using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess.Mappings
{
    public class BloodDonatorMapping:IEntityTypeConfiguration<BloodDonator>
    {
        public void Configure(EntityTypeBuilder<BloodDonator> builder)
        {
            builder
                .HasKey(m => m.Pesel);

            builder
                .HasOne(m => m.BloodType)
                .WithMany()
                .HasForeignKey(m => m.BloodGroupName);

            builder
                .Property(m => m.BirthDate)
                .IsRequired();

            builder
                .Property(m => m.FirstName)
                .IsRequired();

            builder
                .Property(m => m.Surname)
                .IsRequired();

            builder
                .Property(m => m.AmmountOfBloodDonated)
                .IsRequired();

            builder
                .Property(m => m.HomeAdress)
                .IsRequired();

            builder
                .Property(m => m.PhoneNumber)
                .IsRequired();

        }
    }
}
