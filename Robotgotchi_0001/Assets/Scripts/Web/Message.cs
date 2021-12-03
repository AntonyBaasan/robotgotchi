using Newtonsoft.Json;

namespace DefaultNamespace
{
    public class IRequestMessage
    {
        [JsonProperty(PropertyName = "messageType")]
        public string MessageType;
        [JsonProperty(PropertyName = "payload")]
        public object Payload;
    }
    
    public class IResponseMessage
    {
        [JsonProperty(PropertyName = "messageType")]
        public string MessageType;
        [JsonProperty(PropertyName = "payload")]
        public object Payload;
    }

    public static class RequestMessageType
    {
        public const string Echo = "echo";
        public const string Login = "login";
        public const string Logout = "logout";
        public const string GetCurrentUser = "getcurrentuser";
        public const string GetNft = "getnft";
        public const string CallTestApi = "callTestApi";
    }
    
    public static class ResponseMessageType
    {
        public const string UserInfo = "userinfo";
        public const string TestResponse = "testresponse";
        public const string EchoResponse = "echoresponse";
    }
}