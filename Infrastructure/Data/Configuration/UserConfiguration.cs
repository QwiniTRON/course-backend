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
            builder.Property(x => x.RegistrationDate).HasDefaultValueSql("NOW()");
            builder.Property(x => x.Photo).HasDefaultValue("default.png");
            
            builder
                .HasMany(u => u.RolesEntities)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder
                .HasMany(x => x.UserPhoto)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}