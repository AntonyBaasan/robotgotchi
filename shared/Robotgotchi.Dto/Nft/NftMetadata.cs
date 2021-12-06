using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Robotgotchi.Dto.Nft
{
    [Serializable]
    public class NftMetadata
    {
        [JsonConstructor]
        public NftMetadata()
        {
        }

        [JsonProperty(PropertyName = "image_url")]
        public string Image_url { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "ipfs")]
        public string Ipfs { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "properties")]
        public List<NftProperty> Properties { get; set; } = new List<NftProperty>();
    }
}
