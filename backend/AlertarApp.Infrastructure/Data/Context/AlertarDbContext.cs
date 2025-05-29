using AlertarApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlertarApp.Infrastructure.Data.Context
{
    public class AlertarDbContext : DbContext
    {
        public AlertarDbContext(DbContextOptions<AlertarDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<TrustedContact> TrustedContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplica todas as configurações definidas no assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlertarDbContext).Assembly);
        }
    }
}
