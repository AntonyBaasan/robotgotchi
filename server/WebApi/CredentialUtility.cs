using Google.Apis.Auth.OAuth2;
using System.Text;

namespace WebApi
{

    public class RobotgotchiFirebaseCredential
    {
        public string ServiceType { get; set; } = String.Empty;
        public string ProjectId { get; set; } = String.Empty;
        public string PrivateKeyId { get; set; } = String.Empty;
        public string PrivateKey { get; set; } = String.Empty;
        public string ClientEmail { get; set; } = String.Empty;
        public string ClientId { get; set; } = String.Empty;
        public string AuthUri { get; set; } = String.Empty;
        public string TokenUri { get; set; } = String.Empty;
        public string AuthProviderX509CertUrl { get; set; } = String.Empty;
        public string ClientX509CertUrl { get; set; } = String.Empty;
    }

    public class CredentialUtility
    {
        public static GoogleCredential GetGoogleCredential(RobotgotchiFirebaseCredential credentialFromConfig)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append($"\"type\":\"{credentialFromConfig.ServiceType}\",");
            json.Append($"\"project_id\":\"{credentialFromConfig.ProjectId}\",");
            json.Append($"\"private_key_id\":\"{credentialFromConfig.PrivateKeyId}\",");
            json.Append($"\"private_key\":\"{credentialFromConfig.PrivateKey}\",");
            json.Append($"\"client_email\":\"{credentialFromConfig.ClientEmail}\",");
            json.Append($"\"client_id\":\"{credentialFromConfig.ClientId}\",");
            json.Append($"\"auth_uri\":\"{credentialFromConfig.AuthUri}\",");
            json.Append($"\"token_uri\":\"{credentialFromConfig.TokenUri}\",");
            json.Append($"\"auth_provider_x509_cert_url\":\"{credentialFromConfig.AuthProviderX509CertUrl}\",");
            json.Append($"\"client_x509_cert_url\":\"{credentialFromConfig.ClientX509CertUrl}\"");
            json.Append("}");

            return GoogleCredential.FromJson(json.ToString());
        }
    }
}
