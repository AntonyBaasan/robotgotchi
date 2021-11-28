using Newtonsoft.Json;

namespace Moralis
{
    public class MoralisService : IMoralisService
    {
        private readonly HttpClient _httpClient;
        private readonly string chain = "polygon";

        public MoralisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MoralisNftResult> GetNftAsync(string userAddress)
        {
            string APIURL = $"{userAddress}/nft?chain={chain}";
            var response = await _httpClient.GetAsync(APIURL);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                return JsonConvert.DeserializeObject<MoralisNftResult>(content);
            }
            return null;
        }
    }

    public record MoralisNftResult(int total, int page, int page_size, string status, List<Dictionary<string, string>> result);
    public record MoralisNftObject
    {
        public string Token_address { get; set; } = string.Empty;
        public string Token_id { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string Owner_of { get; set; } = string.Empty;
        public string Block_number { get; set; } = string.Empty;
        public string Block_number_minted { get; set; } = string.Empty;
        public string Contract_type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Token_uri { get; set; } = string.Empty;
        public int IsValid { get; set; }
        public int Syncing { get; set; }
        public int Frozen { get; set; }
        public DateTime Synced_at { get; set; }
        public List<MoralisNftMetadata> Metadata { get; set; } = new List<MoralisNftMetadata>();
    }

    public record MoralisNftMetadata
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Ipfs { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MoralisNftProperty> Properties { get; set; } = new List<MoralisNftProperty>();
    }
    public record MoralisNftProperty
    {
        public string Trait_type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

}