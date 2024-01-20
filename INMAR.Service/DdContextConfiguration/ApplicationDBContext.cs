using INMAR.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace INMAR.Service.DdContextConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
    }
}
