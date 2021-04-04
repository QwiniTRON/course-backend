using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}