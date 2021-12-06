
namespace Moralis.Models
{
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
        public MoralisNftMetadata Metadata { get; set; }
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
