using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Presentation
{
    public interface IPresenter<in TOutput>
        where TOutput: IOutput
    {
        IActionResult Present(TOutput output);
    }
}