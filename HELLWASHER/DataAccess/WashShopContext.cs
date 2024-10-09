using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WashShopContext:DbContext
    {
        public WashShopContext(DbContextOptions<WashShopContext> options):base(options) 
        {
            
        }
        
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WashService> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        
        public DbSet<WashServiceStatus> ServiceStatuses { get; set; }
        
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<WashingStatus> WashStatuses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            

        //    /*// 2. User -> Faq (One-to-Many)
        //    modelBuilder.Entity<Faq>()
        //        .HasOne(f => f.User)
        //        .WithMany(u => u.Faqs)
        //        .HasForeignKey(f => f.UserID);*/

            

            

        //    // 5. Cart -> CartItem (One-to-Many)
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(ci => ci.Cart)
        //        .WithMany(c => c.CartItems)
        //        .HasForeignKey(ci => ci.CartId);

        //    // 6. Service -> CartItem (One-to-Many)
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(ci => ci.Service)
        //        .WithMany(s => s.CartItems)
        //        .HasForeignKey(ci => ci.ServiceId);

            

        //    // 10. Category -> Service (One-to-Many)
        //    modelBuilder.Entity<WashService>()
        //        .HasOne(s => s.Category)
        //        .WithMany(c => c.Services)
        //        .HasForeignKey(s => s.CategoryId);
        //    // Service -> CartItem (One-to-Many)
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(c => c.Service)
        //        .WithMany(s => s.CartItems)
        //        .HasForeignKey(c => c.ServiceId);
        //    // 13. ServiceStatus -> Service (One-to-Many)
        //    modelBuilder.Entity<WashService>()
        //        .HasOne(s => s.ServiceStatus)
        //        .WithMany(ss => ss.Services)
        //        .HasForeignKey(s => s.ServiceStatusId);

        //    // 14. OrderStatus -> Order (One-to-Many)
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.OrderStatus)
        //        .WithMany(os => os.Orders)
        //        .HasForeignKey(o => o.OrderStatusId);

        //    // 15. WashStatus -> Order (One-to-Many)
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.WashStatus)
        //        .WithMany(ws => ws.Orders)
        //        .HasForeignKey(o => o.WashStatusId);

        //    modelBuilder.Entity<Order>()
        //        .HasOne(o=>o.Cart)
        //        .WithOne(c => c.Order)
        //        .HasForeignKey<Order>(c=>c.CartId);
        //    // Setting up relationships for the entities
        //    // Configure additional constraints, indexes, etc., if necessary
        //    base.OnModelCreating(modelBuilder);
        //}
    }

}
