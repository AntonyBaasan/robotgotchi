using Newtonsoft.Json;

namespace Robotgotchi.Dto.Nft
{
    public class NftProperty
    {
        public NftProperty()
        {
        }

        [JsonProperty(PropertyName = "trait_type")]
        public string Trait_type { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
