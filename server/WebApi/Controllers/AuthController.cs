using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Signer;
using WebApi.AuthController.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseApp firebaseApp;
        private readonly ILogger<AuthController> logger;
        private string projectId = "robotgotchi-1";

        public AuthController(FirebaseApp firebaseApp, ILogger<AuthController> logger)
        {
            this.firebaseApp = firebaseApp;
            this.logger = logger;
        }

        // GET api/auth/noncetosign
        [HttpPost("noncetosign")]
        public async Task<object> GetNonceToSign([FromBody] GetNonceToSignRequest request)
        {
            try
            {
                var db = FirestoreDb.Create(projectId);

                DocumentReference userDocRef = db.Collection("user_nonce").Document(request.address);
                var userDoc = await userDocRef.GetSnapshotAsync();
                if (userDoc.Exists)
                {
                    userDoc.TryGetValue("nonce", out string existingNonce);
                    return new GetNonceToSignResponse(existingNonce);
                }

                // Create new user
                var auth = FirebaseAuth.GetAuth(firebaseApp);
                await auth.CreateUserAsync(new UserRecordArgs { Uid = request.address });

                // Save nonce for the new user
                var generatedNonce = Math.Floor(Random.Shared.NextDouble() * 1000000).ToString();
                var userDocument = new Dictionary<string, object> { { "nonce", generatedNonce } };
                await db.Collection("user_nonce").Document(request.address).SetAsync(userDocument);

                return new GetNonceToSignResponse(generatedNonce);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return NotFound("Can't find Nonce");
        }

        // GET api/auth/verifysignedmessage
        [HttpPost("verifysignedmessage")]
        public async Task<object> VerifySignedMessage([FromBody] VerifySignedMessageRequest request)
        {
            var db = FirestoreDb.Create(projectId);

            DocumentReference userDocRef = db.Collection("user_nonce").Document(request.address);
            var userDoc = await userDocRef.GetSnapshotAsync();
            if (!userDoc.Exists)
            {
                return NotFound("Can't find user nonce");
            }

            // recover user address from signature
            userDoc.TryGetValue("nonce", out string existingNonce);
            var signer1 = new EthereumMessageSigner();
            var addressRecovered = signer1.EncodeUTF8AndEcRecover(existingNonce, request.signature);
            if (!addressRecovered.ToLower().Equals(request.address))
            {
                return NotFound("Invalid signarute");
            }

            // update nonce
            var generatedNonce = Math.Floor(Random.Shared.NextDouble() * 1000000).ToString();
            var userDocument = new Dictionary<string, object> { { "nonce", generatedNonce } };
            await db.Collection("user_nonce").Document(request.address).UpdateAsync(userDocument);

            // generate Firebase token
            var auth = FirebaseAuth.GetAuth(firebaseApp);
            var token = await auth.CreateCustomTokenAsync(request.address);

            return new VerifySignedMessageResponse(token);

        }
    }
}

