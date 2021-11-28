using Models.Database;
using MongoDB.Driver;

namespace Security.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IMongoCollection<UserNonce> userNonceCollection;

        public TokenService(IRobotgotchiDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            userNonceCollection = database.GetCollection<UserNonce>(databaseSettings.UserNonceCollectionName);
        }

        public async Task<string> GetNonceAsync(string uid)
        {
            // Get Nonce by userId
            var cursor = await userNonceCollection.FindAsync(userNonce => userNonce.Uid.Equals(uid));
            var nonce = await cursor.FirstOrDefaultAsync();
            return (nonce != null) ? nonce.Nonce : "";
        }

        public async Task<string> UpdateNonceAsync(string uid)
        {
            var generatedNonce = Math.Floor(Random.Shared.NextDouble() * 1000000).ToString();
            var userNonce = new UserNonce(uid, generatedNonce);
            await userNonceCollection.ReplaceOneAsync(userNonce => userNonce.Uid.Equals(uid), userNonce, new ReplaceOptions { IsUpsert = true });
            return generatedNonce;
        }
    }
}
