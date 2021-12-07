using Robotgotchi.Dto.Identity;
using Robotgotchi.Dto.Settings;
using UnityEngine;

public class WebSettings : MonoBehaviour
{
    public string WebApiUrl;
    public string Token;

    // Start is called before the first frame update
    void Start()
    {
        var messageReceiver = GameObject.FindObjectOfType<MessageReceiver>();
        messageReceiver.UserInfoChanged += OnUserInfoChanged;
        messageReceiver.GlobalSettingsReceived += OnGlobalSettingsReceived;
    }

    private void OnUserInfoChanged(UserInfo userInfo)
    {
        Debug.Log("WebSettings uid: " + userInfo.Uid);
        Debug.Log("WebSettings token: " + userInfo.Token);
        Token = userInfo.Token;
    }

    private void OnGlobalSettingsReceived(GlobalSettings settings)
    {
        WebApiUrl = settings.WebApiUrl;
    }

    public string GetToken()
    {
        return Token;
    }

    // Update is called once per frame
    void Update()
    {
    }
}