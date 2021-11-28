using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Signer;
using Security.Auth;
using WebApi.AuthController.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseApp firebaseApp;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenService tokenService;

        public AuthController(FirebaseApp firebaseApp, ILogger<AuthController> logger, ITokenService tokenService)
        {
            this.firebaseApp = firebaseApp;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        // GET api/auth/noncetosign
        [HttpPost("noncetosign")]
        public async Task<ActionResult<GetNonceToSignResponse>> GetNonceToSign([FromBody] GetNonceToSignRequest request)
        {
            string existingNonce = await tokenService.GetNonceAsync(request.address);
            if (!string.IsNullOrEmpty(existingNonce))
            {
                return new GetNonceToSignResponse(existingNonce);
            }
            try
            {
                // Create new user
                var auth = FirebaseAuth.GetAuth(firebaseApp);
                await auth.CreateUserAsync(new UserRecordArgs { Uid = request.address });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                //return NotFound("Can't create user");
            }
            try
            {
                // Save nonce for the new user
                string generatedNonce = await tokenService.UpdateNonceAsync(request.address);
                return new GetNonceToSignResponse(generatedNonce);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound("Can't find Nonce");
            }
        }

        // GET api/auth/verifysignedmessage
        [HttpPost("verifysignedmessage")]
        public async Task<ActionResult<VerifySignedMessageResponse>> VerifySignedMessage([FromBody] VerifySignedMessageRequest request)
        {
            // get nonce for the user
            string existingNonce = await tokenService.GetNonceAsync(request.address);
            if (string.IsNullOrEmpty(existingNonce))
            {
                return NotFound("Can't find user nonce");
            }

            // recover user address from signature
            var signer1 = new EthereumMessageSigner();
            var addressRecovered = signer1.EncodeUTF8AndEcRecover(existingNonce, request.signature);
            if (!addressRecovered.ToLower().Equals(request.address))
            {
                return NotFound("Invalid signature");
            }

            // update nonce
            await tokenService.UpdateNonceAsync(request.address);

            // generate Firebase token
            var auth = FirebaseAuth.GetAuth(firebaseApp);
            var token = await auth.CreateCustomTokenAsync(request.address);

            return new VerifySignedMessageResponse(token);

        }
    }
}

