using BloodBank.DataAccess.Mappings;
using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BloodDonator> BloodDonators { get; set; }
        public DbSet<BloodStorage> Storage { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new BloodDonatorMapping());
            modelBuilder.ApplyConfiguration(new BloodTypeMapping());
            modelBuilder.ApplyConfiguration(new BloodStorageMapping());
        }
    }
}
