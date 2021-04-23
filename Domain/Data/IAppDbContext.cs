using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public interface IAppDbContext: IUnitOfWorkCreator
    {
        public  DbSet<User> Users { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserProgress> Progresses { get; set; }
        public DbSet<PracticeOrder> PracticeOrders { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}