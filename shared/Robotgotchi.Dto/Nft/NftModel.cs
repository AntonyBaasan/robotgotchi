using Newtonsoft.Json;
using System;

namespace Robotgotchi.Dto.Nft
{
    public class NftModel
    {
        public NftModel()
        {
        }

        [JsonProperty(PropertyName = "token_address")]
        public string Token_address { get; set; }
        [JsonProperty(PropertyName = "token_id")]
        public string Token_id { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }
        [JsonProperty(PropertyName = "owner_of")]
        public string Owner_of { get; set; }
        [JsonProperty(PropertyName = "block_number")]
        public string Block_number { get; set; }
        [JsonProperty(PropertyName = "block_number_minted")]
        public string Block_number_minted { get; set; }
        [JsonProperty(PropertyName = "contract_type")]
        public string Contract_type { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "token_uri")]
        public string Token_uri { get; set; }
        [JsonProperty(PropertyName = "isValid")]
        public int IsValid { get; set; }
        [JsonProperty(PropertyName = "syncing")]
        public int Syncing { get; set; }
        [JsonProperty(PropertyName = "frozen")]
        public int Frozen { get; set; }
        [JsonProperty(PropertyName = "synced_atss")]
        public DateTime Synced_at { get; set; }
        [JsonProperty(PropertyName = "metadata")]
        public NftMetadata Metadata { get; set; }
    }
}
