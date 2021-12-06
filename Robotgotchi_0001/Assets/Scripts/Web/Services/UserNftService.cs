using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Robotgotchi.Dto.Nft;

namespace DefaultNamespace.Services
{
    public class UserNftService
    {
        private string baseUrl = "";
        private string token = "";
        private const string api = "/api/nft";

        public UserNftService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        public async Task<List<NftModel>> GetNfts()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authorization", "Bearer " + token);

            var response = await httpClient.GetAsync(baseUrl + api);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var contentAsStr = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<NftModel>>(contentAsStr);
                return result;
            }

            return new List<NftModel>();

        }
    }
}