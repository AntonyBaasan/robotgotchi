namespace Security.Auth
{
    public interface ITokenService
    {
        Task<string> GetNonceAsync(string uid);
        Task<string> UpdateNonceAsync(string uid);
    }
}