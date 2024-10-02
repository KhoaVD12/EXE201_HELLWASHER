using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WashShopContextFactory : IDesignTimeDbContextFactory<WashShopContext>
    {
        public WashShopContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WashShopContext>();
            optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=12345;Database=WashShop;TrustServerCertificate=True;Encrypt=True"); // For migrations, use a valid connection string

            return new WashShopContext(optionsBuilder.Options);
        }
    }
}
