using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Account.SignIn
{
    public class SignInCase: IUseCase<SignInInput>
    {
        private IAppDbContext _context;

        public SignInCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(SignInInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == request.Mail);

            if (user is null)
            {
                return ActionOutput.Error("Данные не верны");
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password) == false)
            {
                return ActionOutput.Error("Данные не верны 123");
            }
            
            return ActionOutput.Success;
        }
    }
}