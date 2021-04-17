using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CommentConfiguration: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne<User>(x => x.Author)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne<Lesson>(x => x.Lesson)
                .WithMany(x => x.Comments);
        }
    }
}