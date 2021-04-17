using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserProgressConfiguration: IEntityTypeConfiguration<UserProgress>
    {
        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.UserProgresses)
                .HasForeignKey(x => x.UserId);
            
            builder.HasOne<Lesson>(x => x.Lesson)
                .WithMany(x => x.UserProgresses)
                .HasForeignKey(x => x.LessonId);
        }
    }
}