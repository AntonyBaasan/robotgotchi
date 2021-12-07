using Newtonsoft.Json;

namespace Robotgotchi.Dto.Identity
{
    public class UserInfo
    {
        [JsonConstructor]
        public UserInfo()
        {
        }

        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

    }
}
