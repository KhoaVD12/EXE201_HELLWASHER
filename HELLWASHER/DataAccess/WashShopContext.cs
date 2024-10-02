﻿using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WashShopContext:DbContext
    {
        public WashShopContext(DbContextOptions<WashShopContext> options):base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WashService> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothUnit> ClothUnits { get; set; }
        public DbSet<WashServiceType> ServiceTypes { get; set; }
        public DbSet<WashServiceStatus> ServiceStatuses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<WashingStatus> WashStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. User -> Orders (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            /*// 2. User -> Faq (One-to-Many)
            modelBuilder.Entity<Faq>()
                .HasOne(f => f.User)
                .WithMany(u => u.Faqs)
                .HasForeignKey(f => f.UserID);*/

            // 3. User -> Feedback (One-to-Many)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId);

            

            // 5. Cart -> CartItem (One-to-Many)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            // 6. Service -> CartItem (One-to-Many)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Service)
                .WithMany(s => s.CartItems)
                .HasForeignKey(ci => ci.ServiceId);

            // 7. Service -> Feedback (One-to-Many)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Service)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.ServiceId);

            // 8. Order -> OrderItem (One-to-Many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // 9. Service -> OrderItem (One-to-Many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Service)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(oi => oi.ServiceId);

            // 10. Category -> Service (One-to-Many)
            modelBuilder.Entity<WashService>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId);

            // 11. ClothUnit -> Service (One-to-Many)
            modelBuilder.Entity<WashService>()
                .HasOne(s => s.ClothUnit)
                .WithMany(cu => cu.Services)
                .HasForeignKey(s => s.ClothUnitId);

            // 12. ServiceType -> Service (One-to-Many)
            modelBuilder.Entity<WashService>()
                .HasOne(s => s.ServiceType)
                .WithMany(st => st.Services)
                .HasForeignKey(s => s.ServiceTypeId);

            // 13. ServiceStatus -> Service (One-to-Many)
            modelBuilder.Entity<WashService>()
                .HasOne(s => s.ServiceStatus)
                .WithMany(ss => ss.Services)
                .HasForeignKey(s => s.ServiceStatusId);

            // 14. OrderStatus -> Order (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusId);

            // 15. WashStatus -> Order (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.WashStatus)
                .WithMany(ws => ws.Orders)
                .HasForeignKey(o => o.WashStatusId);

            // Setting up relationships for the entities
            // Configure additional constraints, indexes, etc., if necessary
            base.OnModelCreating(modelBuilder);
        }
    }

}
