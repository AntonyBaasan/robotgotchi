using Newtonsoft.Json;

namespace Robotgotchi.Dto.Settings
{
    public class GlobalSettings
    {
        [JsonProperty(PropertyName = "webapiurl")]
        public string WebApiUrl { get; set; }
    }
}
