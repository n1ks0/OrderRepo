using Microsoft.EntityFrameworkCore;
using Order.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DAL.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public Context()
        {
            Database.EnsureCreated();
        }

        public DbSet<BL.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<BL.Entities.Order>()
                .HasIndex(oi => new { oi.Number, oi.ProviderId })
                .IsUnique();

            modelBuilder.Entity<Provider>()
                .HasData(new Provider[]
                {
                     new Provider { Id= 1,Name = "Яблочный король" },
                     new Provider { Id= 2, Name = "Апельсиновая принцесса"}
                });

            modelBuilder.Entity<BL.Entities.Order>()
                .HasData(new BL.Entities.Order[]
                {
                    new BL.Entities.Order
                    {
                        Id= 1,
                        Date= DateTime.Today,
                        Number = "1",
                        ProviderId = 1
                    },
                    new BL.Entities.Order
                    {
                        Id= 2,
                        Date= DateTime.Today,
                        Number = "2",
                        ProviderId = 2
                    }
                });

            modelBuilder.Entity<OrderItem>()
                .HasData(new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id= 1,
                        Name = "Яблоко",
                        Quantity = 3,
                        Unit = "Штука",
                        OrderId = 1
                    },
                    new OrderItem
                    {
                        Id = 2,
                        Name = "Апельсин",
                        Quantity = 2,
                        Unit = "Штука",
                        OrderId = 2
                    }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
