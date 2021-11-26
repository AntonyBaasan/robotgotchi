namespace WebApi.AuthController.Dto
{
    public record GetNonceToSignRequest(string address);
    public record GetNonceToSignResponse(string nonce);

    public record VerifySignedMessageRequest(string address, string signature);
    public record VerifySignedMessageResponse(string token);
}
