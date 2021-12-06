using Newtonsoft.Json;
using System;

namespace Robotgotchi.Dto.Nft
{
    [Serializable]
    public class NftProperty
    {
        [JsonConstructor]
        public NftProperty()
        {
        }

        [JsonProperty(PropertyName = "trait_type")]
        public string Trait_type { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
