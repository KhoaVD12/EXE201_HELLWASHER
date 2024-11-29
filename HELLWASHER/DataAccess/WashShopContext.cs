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
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCheckout> ProductCheckouts { get; set; }
        public DbSet<ServiceCheckout> ServiceCheckouts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentLinkInformation> PaymentLinkInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentLinkInformation>()
                .HasKey(k => k.PaymentLinkInformationId);
            base.OnModelCreating(modelBuilder);
        }
    }

}
