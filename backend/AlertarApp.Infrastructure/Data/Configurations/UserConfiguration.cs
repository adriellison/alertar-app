using AlertarApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertarApp.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(20);
                
            builder.Property(u => u.DocumentId)
                .IsRequired()
                .HasMaxLength(14); // CPF com formatação
                
            builder.Property(u => u.HashedPin)
                .IsRequired()
                .HasMaxLength(128); // Tamanho para hash SHA-256 e sal
                
            builder.Property(u => u.CreatedAt)
                .IsRequired();
                
            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
                
            // Índices para pesquisas comuns
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.DocumentId).IsUnique();
            builder.HasIndex(u => u.Phone);
        }
    }
}
