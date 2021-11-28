using MongoDB.Bson.Serialization.Attributes;

namespace Models.Database
{
    public class UserNonce
    {
        public UserNonce(string uid, string nonce)
        {
            Uid = uid;
            Nonce = nonce;
        }
        [BsonId]
        public string Uid { get; set; }
        public string Nonce { get; set; }
    }
}
