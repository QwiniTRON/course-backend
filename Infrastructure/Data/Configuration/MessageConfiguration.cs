using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class MessageConfiguration: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("NOW()");

            builder.HasOne<User>(x => x.Author)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne<Chat>(x => x.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId);

            builder.HasOne<AppFile>(x => x.Content)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ContentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}