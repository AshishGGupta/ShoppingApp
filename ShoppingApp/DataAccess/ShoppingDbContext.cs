using Microsoft.EntityFrameworkCore;
using ShoppingApp.Model;
using ShoppingApp.Model.Domain;

namespace ShoppingApp.DataAccess
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderAndPayment> OrderAndPayments { get; set; }
    }
}
