using Newtonsoft.Json;

namespace Moralis
{
    public interface IMoralisService
    {
        Task<MoralisNftResult> GetNftAsync(string userAddress);
    }

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
        string token_address { get; set; } = string.Empty;
        string token_id { get; set; } = string.Empty;
        string amount { get; set; } = string.Empty;
        string owner_of { get; set; } = string.Empty;
        string block_number { get; set; } = string.Empty;
        string block_number_minted { get; set; } = string.Empty;
        string contract_type { get; set; } = string.Empty;
        string name { get; set; } = string.Empty;
        string symbol { get; set; } = string.Empty;
        string token_uri { get; set; } = string.Empty;
        string metadata { get; set; } = string.Empty;
        int isValid { get; set; }
        int syncing { get; set; }
        DateTime synced_at { get; set; }
        int frozen { get; set; }
    }


}