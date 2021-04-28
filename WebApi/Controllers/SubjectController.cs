using System.Collections.Generic;
using System.Threading.Tasks;
using course_backend.Implementations;
using Domain.Abstractions.Services;
using Domain.UseCases.Subject.AddSertificate;
using Domain.UseCases.Subject.GetOne;
using Domain.Views.Subject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/subject")]
    public class SubjectController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private readonly ICurrentUserProvider _currentUserProvider;

        public SubjectController(IUseCaseDispatcher dispatcher, ICurrentUserProvider currentUserProvider)
        {
            _dispatcher = dispatcher;
            _currentUserProvider = currentUserProvider;
        }

        /// <summary>
        ///     Create certificate for user by subject by id
        /// </summary>
        /// <remarks>
        ///     # Create certificate for user by subject by id
        /// 
        ///     ### ! creates if all lessons are done
        /// </remarks>
        [HttpPost("sertificate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateSubjectSertificate([FromBody]AddCertificateInput request)
        {
            request.UserId = (await _currentUserProvider.GetCurrentUser()).Id;
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get subject with all lessons
        /// </summary>
        /// <remarks>
        ///     # Get subject with all lessons
        /// </remarks>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<SubjectDeteiledView>), 200)]
        public async Task<IActionResult> GetSubject([FromQuery]GetSubjectInfoInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}