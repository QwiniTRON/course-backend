using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Mail).IsUnique();
            
            builder
                .HasMany(u => u.RolesEntities)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        }
    }
}