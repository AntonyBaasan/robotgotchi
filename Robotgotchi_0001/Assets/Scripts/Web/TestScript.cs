using DefaultNamespace.Services;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private UserNftService _userNftService;
    public WebSettings webSettings;

    void Start()
    {
        webSettings = FindObjectOfType<WebSettings>();
        _userNftService = new UserNftService(webSettings.BaseUrl);
    }

    public async void ClickGetUserNftHttp()
    {
        _userNftService.SetToken(webSettings.Token);
        var nftModels = await _userNftService.GetNfts();
        Debug.Log(nftModels.Count);
    }
}