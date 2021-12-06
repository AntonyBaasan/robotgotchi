using Moralis.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                return Convert(content);
            }
            return null;
        }

        private MoralisNftResult Convert(string content)
        {
            JObject moralistNftResult = JObject.Parse(content);
            int total = moralistNftResult["total"] != null ? (int)moralistNftResult["total"] : -1;
            int page = moralistNftResult["page"] != null ? (int)moralistNftResult["page"] : -1;
            int page_size = moralistNftResult["page_size"] != null ? (int)moralistNftResult["page_size"] : -1;
            string status = moralistNftResult["status"]?.ToString();
            var nftObjects = new List<MoralisNftObject>();

            if (moralistNftResult["result"] != null)
            {
                var result = (JArray)moralistNftResult["result"];

                foreach (JObject jobject in result)
                {
                    var Token_address = jobject["token_address"]?.ToString();
                    var Token_id = jobject["token_id"]?.ToString();
                    var Amount = jobject["amount"]?.ToString();
                    var Owner_of = jobject["owner_of"]?.ToString();
                    var Block_number = jobject["block_number"]?.ToString();
                    var Block_number_minted = jobject["block_number_minted"]?.ToString();
                    var Contract_type = jobject["contract_type"]?.ToString();
                    var Name = jobject["name"]?.ToString();
                    var Symbol = jobject["symbol"]?.ToString();
                    var Token_uri = jobject["token_uri"]?.ToString();
                    var IsValid = jobject["is_valid"] != null ? (int)jobject["is_valid"] : -1;
                    var Syncing = jobject["syncing"] != null ? (int)jobject["syncing"] : -1;
                    var Synced_at = DateTime.Parse(jobject["synced_at"]?.ToString());


                    MoralisNftMetadata Metadata = ConvertMetadata(jobject["metadata"]);

                    var nftObject = new MoralisNftObject
                    {
                        Token_address = Token_address,
                        Token_id = Token_id,
                        Amount = Amount,
                        Owner_of = Owner_of,
                        Block_number = Block_number,
                        Block_number_minted = Block_number_minted,
                        Contract_type = Contract_type,
                        Name = Name,
                        Symbol = Symbol,
                        Token_uri = Token_uri,
                        IsValid = IsValid,
                        Syncing = Syncing,
                        Synced_at = Synced_at,
                        Metadata = Metadata,
                    };
                    nftObjects.Add(nftObject);
                }
            }

            return new MoralisNftResult(total, page, page_size, status, nftObjects);
        }

        private MoralisNftMetadata ConvertMetadata(JToken? metadataObj)
        {
            if (metadataObj == null || string.IsNullOrEmpty(metadataObj.ToString())) { return null; }

            var jobject = JObject.Parse(metadataObj.ToString());

            var ImageUrl = jobject["image_url"]?.ToString();
            var Ipfs = jobject["ipfs"]?.ToString();
            var Name = jobject["name"]?.ToString();
            var Description = jobject["descriptions"]?.ToString();
            var JObjectProperties = jobject["properties"];
            List<MoralisNftProperty> Properties = new List<MoralisNftProperty>();
            if (JObjectProperties != null)
            {
                foreach (JObject prop in JObjectProperties)
                {
                    var property = new MoralisNftProperty { Trait_type = prop["trait_type"].ToString(), Value = prop["value"].ToString() };
                    Properties.Add(property);
                }
            }

            return new MoralisNftMetadata
            {
                ImageUrl = ImageUrl,
                Ipfs = Ipfs,
                Description = Description,
                Name = Name,
                Properties = Properties,
            };
        }


    }



}