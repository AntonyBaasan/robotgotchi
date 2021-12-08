using System;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Robotgotchi.Dto.Identity;
using Robotgotchi.Dto.Settings;

public class MessageReceiver : MonoBehaviour
{
    public Text debugText;
    public Action<GlobalSettings> GlobalSettingsReceived;
    public Action<UserInfo> UserInfoChanged;

    public void ReceiveClientMessage(string messageText)
    {
        Debug.Log("Message Received: " + messageText);
        var message = JsonConvert.DeserializeObject<IResponseMessage>(messageText);
        switch (message.MessageType.ToLower())
        {
            case ResponseMessageType.EchoResponse:
                HandleEcho(message);
                break;
            case ResponseMessageType.UserInfo:
                HandleUserInfo(message);
                break;
            case ResponseMessageType.GlobalSettings:
                HandleGlobalSettings(message);
                break;
            default:
                Debug.Log("No handler for a message:");
                Debug.Log(message);
                break;
        }
    }

    private void HandleEcho(IResponseMessage message)
    {
        debugText.text = message.Payload?.ToString();
    }

    private void HandleUserInfo(IResponseMessage message)
    {
        if (message.Payload != null)
        {
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(message.Payload.ToString());
            Debug.Log("deserialize, userInfo.uid: " + userInfo.Uid);
            Debug.Log("deserialize, userInfo.token: " + userInfo.Token);
            UserInfoChanged?.Invoke(userInfo);
            debugText.text = "user received!";
        }
        else
        {
            UserInfoChanged?.Invoke(null);
            debugText.text = "user object is empty!";
        }
    }
    
    private void HandleGlobalSettings(IResponseMessage message)
    {
        if (message.Payload != null)
        {
            var globalSettings = JsonConvert.DeserializeObject<GlobalSettings>(message.Payload.ToString());
            Debug.Log("deserialize, globalSettings.WebApiUrl: " + globalSettings.WebApiUrl);
            GlobalSettingsReceived?.Invoke(globalSettings);
            debugText.text = "user received!";
        }
    }
}