using System.Threading.Tasks;
using course_backend.Abstractions.DI;
using Domain.Data;
using Domain.Entity;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, 
        UserRoleEntity, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IAppDbContext
    {
        // entities
        public  DbSet<User> Users { get; set; }
        
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserProgress> Progresses { get; set; }
        public DbSet<PracticeOrder> PracticeOrders { get; set; }
        public DbSet<UserRoleEntity> RoleEntities { get; set; }
        
        
        private IUnitOfWork _currentUnitOfWork;

        /* ctor */
        public AppDbContext(DbContextOptions options)
            : base(options) {  }
        
        /* model creating */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        /* unit of work */
        public IUnitOfWork CreateUnitOfWork()
        {
            if (_currentUnitOfWork != null)
            {
                return new DumbTransaction();
            }

            _currentUnitOfWork = new Transaction(Database.BeginTransaction(), this);
            return _currentUnitOfWork;
        }
        
        /* App Transaction */
        private class Transaction : IUnitOfWork
        {
            private readonly AppDbContext _dbContext;
            private readonly IDbContextTransaction _transaction;

            public Transaction(IDbContextTransaction transaction, AppDbContext context)
            {
                _dbContext = context;
                _transaction = transaction;
            }

            public void Dispose()
            {
                _transaction.Dispose();
                _dbContext._currentUnitOfWork = null;
            }

            public async Task Apply()
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }

            public Task Cancel() => _transaction.RollbackAsync();
        }
        
        /* null transaction */
        private class DumbTransaction : IUnitOfWork
        {
            public void Dispose()
            {
            }

            public Task Apply() => Task.CompletedTask;
            public Task Cancel() => Task.CompletedTask;
        }
    }
}