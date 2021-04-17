using System.Threading.Tasks;
using course_backend.Implementations;
using Domain.UseCases.User.Test;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/user")]
    public class UserController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public UserController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
    }
}