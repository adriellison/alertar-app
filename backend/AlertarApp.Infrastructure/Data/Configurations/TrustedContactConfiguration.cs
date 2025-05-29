using AlertarApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertarApp.Infrastructure.Data.Configurations
{
    public class TrustedContactConfiguration : IEntityTypeConfiguration<TrustedContact>
    {
        public void Configure(EntityTypeBuilder<TrustedContact> builder)
        {
            builder.HasKey(tc => tc.Id);
            
            builder.Property(tc => tc.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(tc => tc.Phone)
                .IsRequired()
                .HasMaxLength(20);
                
            builder.Property(tc => tc.Email)
                .HasMaxLength(100);
                
            builder.Property(tc => tc.SendSms)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.Property(tc => tc.SendEmail)
                .IsRequired()
                .HasDefaultValue(false);
                
            builder.Property(tc => tc.CreatedAt)
                .IsRequired();
                
            builder.Property(tc => tc.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
                
            // Index para pesquisas rápidas por usuário
            builder.HasIndex(tc => tc.UserId);
            builder.HasIndex(tc => tc.Phone);
        }
    }
}
