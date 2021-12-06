using Robotgotchi.Dto.Identity;
using UnityEngine;

public class WebSettings : MonoBehaviour
{
    public string BaseUrl;
    public string Token;

    // Start is called before the first frame update
    void Start()
    {
        var messageReceiver = GameObject.FindObjectOfType<MessageReceiver>();
        messageReceiver.UserInfoChanged += OnUserInfoChanged;
    }

    private void OnUserInfoChanged(UserInfo userInfo)
    {
        Token = userInfo.Token;
    }

    // Update is called once per frame
    void Update()
    {
    }
}