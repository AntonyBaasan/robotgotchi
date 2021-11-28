using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moralis;
using Nethereum.Web3;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private readonly IMoralisService moralisService;
        private readonly string userAddress = "0x0e90709f60fdacea987fcbba0927e0da0be870d9";

        public NftController(IMoralisService moralisService)
        {
            this.moralisService = moralisService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            var userAddress = this.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value;

            var t = await GetNfts(userAddress);

            return new[] { t.ToString() };

        }

        private async Task<MoralisNftResult> GetNfts(string userAddress)
        {
            return await moralisService.GetNftAsync(userAddress);
        }

        private static async Task<decimal> GetBalance(string userAddress, Web3 web3)
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(userAddress);
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
            return etherAmount;
        }
    }
}
