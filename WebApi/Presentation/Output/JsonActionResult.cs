using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Presentation.Output
{
    public class JsonActionResult: ContentResult
    {
        public JsonActionResult(object response): this(JsonSerializationHelper.Serialize(response))
        {
        }

        public JsonActionResult(string response)
        {
            Content = response;
            ContentType = "application/json";
        }

        public static ActionResult Ok(object content) => new JsonActionResult(content);
        public static ActionResult NotFound(object content) => WithCode(content, HttpStatusCode.NotFound);
        public static ActionResult BadRequest(object content) => WithCode(content, HttpStatusCode.BadRequest);
        public static ActionResult Unauthorized(object content) => WithCode(content, HttpStatusCode.Unauthorized);
        public static ActionResult Forbidden(object content) => WithCode(content, HttpStatusCode.Forbidden);
        
        private static ActionResult WithCode(object content, HttpStatusCode code) => 
            new JsonActionResult(content) {StatusCode = (int)code};
    }
}