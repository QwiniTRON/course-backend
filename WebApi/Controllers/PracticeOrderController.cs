using System.Threading.Tasks;
using course_backend.Implementations;
using Domain.UseCases.PracticeOrder.Add;
using Domain.UseCases.PracticeOrder.GetMany;
using Domain.UseCases.PracticeOrder.GetOne;
using Domain.UseCases.PracticeOrder.OneInfo;
using Domain.UseCases.PracticeOrder.Reject;
using Domain.UseCases.PracticeOrder.Resolve;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    public class PracticeOrderController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public PracticeOrderController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /* add new order */
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddPracticeOrder(AddPracticeOrderInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* get many */
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPracticeOrders(GetPracticesInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* get by user id */
        [HttpGet("user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOneByUserId(GetOnePracticeInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* get one */
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOneInfo(GetPracticeInfoInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* resolve */
        [HttpPut("resolve")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Resolve(ResolvePracticeInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* reject */
        [HttpPut("reject")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Reject(RejectPracticeInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}