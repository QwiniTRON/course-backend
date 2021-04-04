using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace course_backend.Presentation
{
    public static class JsonSerializationHelper
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o, SerializerSettings);
        }
    }
}