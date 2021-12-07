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
        public const string GlobalSettings = "globalsettings";
    }
    
    public static class ResponseMessageType
    {
        public const string UserInfo = "userinfo";
        public const string EchoResponse = "echoresponse";
        public const string GlobalSettings = "globalsettings";
    }
}