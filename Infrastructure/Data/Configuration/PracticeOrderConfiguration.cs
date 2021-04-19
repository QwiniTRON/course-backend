using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class PracticeOrderConfiguration: IEntityTypeConfiguration<PracticeOrder>
    {
        public void Configure(EntityTypeBuilder<PracticeOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");

            builder.HasOne<Lesson>(x => x.Lesson)
                .WithMany(x => x.PracticeOrders)
                .HasForeignKey(x => x.LessonId);

            builder.HasOne<User>(x => x.Author)
                .WithMany(x => x.PracticeOrders)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne<User>(x => x.Teacher)
                .WithMany(x => x.PracticeOrdersChecks)
                .HasForeignKey(x => x.TeacherId);

            builder.HasOne<AppFile>(x => x.PracticeContent)
                .WithMany(x => x.PracticeOrders)
                .HasForeignKey(x => x.PracticeContentId);
        }
    }
}