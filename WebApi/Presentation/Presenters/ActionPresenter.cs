using course_backend.Presentation.Output;
using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Presentation.Presenters
{
    public class ActionPresenter: IPresenter<ActionOutput>
    {
        public IActionResult Present(ActionOutput output)
        {
            return JsonActionResult.Ok(output);
        }
    }
}