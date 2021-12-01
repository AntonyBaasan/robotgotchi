namespace DefaultNamespace
{
    public class Message
    {
        public string FunctionName;
        public object Data;
    }

    public static class FunctionName
    {
        public const string Echo = "echo";
        public const string Login = "login";
        public const string LoginResult = "loginResult";
        public const string RequestNft = "requestNft";
        public const string ReceiveNft = "receiveNft";
        public const string GetWalletAddress = "getWalletAddress";
        public const string GetWalletAddressResponse = "getWalletAddressResponse";
        
    }
}