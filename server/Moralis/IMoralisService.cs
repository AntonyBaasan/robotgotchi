using Moralis.Models;

namespace Moralis
{
    public interface IMoralisService
    {
        Task<MoralisNftResult> GetNftAsync(string userAddress);
    }
}