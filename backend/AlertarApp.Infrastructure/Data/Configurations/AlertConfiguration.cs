using AlertarApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertarApp.Infrastructure.Data.Configurations
{
    public class AlertConfiguration : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(a => a.Description)
                .HasMaxLength(500);
                
            builder.Property(a => a.Latitude)
                .IsRequired()
                .HasColumnType("decimal(10,6)");
                
            builder.Property(a => a.Longitude)
                .IsRequired()
                .HasColumnType("decimal(10,6)");
                
            builder.Property(a => a.CreatedAt)
                .IsRequired();
                
            builder.Property(a => a.Status)
                .IsRequired();
                
            // Index para pesquisas por usuário
            builder.HasIndex(a => a.UserId);
        }
    }
}
