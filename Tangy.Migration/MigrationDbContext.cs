using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;

namespace Notification.Migration
{
    public class MigrationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public MigrationDbContext(DbContextOptions<MigrationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, options => options.CommandTimeout(300));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}