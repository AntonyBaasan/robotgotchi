
namespace Moralis.Models
{
    public record MoralisNftResult(int total, int page, int page_size, string status, List<MoralisNftObject> result);
}
