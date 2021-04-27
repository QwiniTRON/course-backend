using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class SubjectSertificateConfiguration: IEntityTypeConfiguration<SubjectSertificate>
    {
        public void Configure(EntityTypeBuilder<SubjectSertificate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedTime).HasDefaultValueSql("NOW()");
            
            builder.HasOne<User>(x => x.Owner)
                .WithMany(x => x.SubjectSertificates)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired();

            builder.HasOne<Subject>(x => x.Subject)
                .WithMany(x => x.SubjectSertificates)
                .HasForeignKey(x => x.SubjectId)
                .IsRequired();
        }
    }
}