

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace corewebapi.Models
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product_Master> Product_Master { get; set; }
        public DbSet<Category> SubCategory { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<studentDetail> studentDetail { get; set; }
        public DbSet<studentMark> studentMark { get; set; }
        public DbSet<Usermaster> Usermaster { get; set; }
    }
}
