using System;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/files")]
    public class FilesController: Controller
    {
        private readonly IFileUploader _storage;

        public FilesController(IFileUploader storage)
        {
            _storage = storage;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetTask([FromRoute] string name) 
        {
            try
            {
                return File(_storage.GetFileStream(name), "application/zip");
            }
            catch (Exception err)
            {
                return StatusCode(404);
            }
        }
    }
}