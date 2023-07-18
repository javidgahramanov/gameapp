using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GameService.Core.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = Init();

        public static T FromJsonString<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }

        private static JsonSerializerSettings Init()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            jsonSerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            jsonSerializerSettings.Converters.Add(new KeyValuePairConverter());
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return jsonSerializerSettings;
        }
    }
}