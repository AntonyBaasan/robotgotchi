using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Robotgotchi.Dto.Nft;
using UnityEngine;
using UnityEngine.Networking;

public class NftManager : MonoBehaviour
{
    public WebSettings webSettings;
    private const string api = "/api/nft";

    public int nftCount = 0;
    public List<NftModel> AllSkinNfts { get; set; }
    public List<NftModel> UserNfts { get; set; }
    
    void Start()
    {
        webSettings = FindObjectOfType<WebSettings>();
    }

    public void LoadUserNft()
    {
        StartCoroutine(SendRequestLoadUserNft());
    }

    private IEnumerator SendRequestLoadUserNft()
    {
        var uri = webSettings.WebApiUrl + api;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("authorization", "Bearer " + webSettings.GetToken());
            webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    HandleSuccessResponse(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public void HandleSuccessResponse(string nftModelResult)
    {
        var result = JsonConvert.DeserializeObject<List<NftModel>>(nftModelResult);
        nftCount = result.Count;
        UserNfts = result;
        Debug.Log(nftCount); 
    }
}

class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
{
 
    // Encoded RSAPublicKey
    private static string PUB_KEY = "mypublickey";
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // X509Certificate2 certificate = new X509Certificate2(certificateData);
        // string pk = certificate.GetPublicKeyString();
        // if (pk.ToLower().Equals(PUB_KEY.ToLower()))
        //     return true;
        // return false;
        return true;
    }
}