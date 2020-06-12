using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess.Mappings
{
    public class UserMapping:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(m => m.Email);

            builder
                .HasOne(m => m.Role)
                .WithOne(m => m.User)
                .HasForeignKey<Role>(m => m.UserEmail);

            builder
                .HasOne(m => m.BloodDonator)
                .WithOne(m => m.User)
                .HasForeignKey<BloodDonator>(m => m.UserEmail);

            builder
                .Property(m => m.Password)
                .IsRequired();

            builder
                .Property(m => m.IsActive)
                .IsRequired();
        }
    }
}
