using ItemRazorV1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemRazorV1.EFDbContext
{
    public class ItemDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ItemDBv2; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
