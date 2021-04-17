using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Account.SignUp
{
    public class SignUpCase: IUseCase<SignUpInput>
    {
        private IAppDbContext _context;

        public SignUpCase(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(SignUpInput request, CancellationToken cancellationToken)
        {
            var hasSuchEmail = await _context.Users.AnyAsync(x => x.Mail == request.Mail);

            if (hasSuchEmail)
            {
                return ActionOutput.Error("Такой email уже зарегистрирован");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new Entity.User(request.Mail, request.Nick, passwordHash, request.Role);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            
            return ActionOutput.Success;
        }
    }
}