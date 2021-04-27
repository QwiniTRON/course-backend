using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entity;
using Domain.UseCases.PracticeOrder.GetOne;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public interface IPracticeOrderProvider
    {
        Task<List<PracticeOrder>> GetUserPracticeOrders(int userId, int lessonId);
        Task<List<PracticeOrder>> GetUserPracticeOrdersAll(int userId);
    }

    public class PracticeOrderProviderService : IPracticeOrderProvider
    {
        private readonly IAppDbContext _context;

        public PracticeOrderProviderService(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<List<PracticeOrder>> GetUserPracticeOrders(int userId, int lessonId)
        {
            return await _context.PracticeOrders
                .Include(x => x.Author)
                .Where(x => x.Author.Id == userId && x.LessonId == lessonId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
        
        public async Task<List<PracticeOrder>> GetUserPracticeOrdersAll(int userId)
        {
            return await _context.PracticeOrders
                .Include(x => x.Author)
                .Where(x => x.Author.Id == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}