using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using DefaultNamespace;
using Newtonsoft.Json;

public class MessageSender : MonoBehaviour
{
    public Text debugText;

    [DllImport("__Internal")]
    private static extern void OnSendToClient(string messageText);

    void Start()
    {
        StartCoroutine(GetGlobalSettings(2));
    }

    public void UpdateGlobalSettings()
    {
        var message = new IRequestMessage()
        {
            MessageType = RequestMessageType.GlobalSettings,
        };
        SendToClient(message);
    }

    public void SendEchoMessage()
    {
        var message = new IRequestMessage()
        {
            MessageType = RequestMessageType.Echo,
            Payload = "Hello World"
        };
        this.SendToClient(message);
    }

    public void Login()
    {
        var message = new IRequestMessage()
        {
            MessageType = RequestMessageType.Login,
        };
        this.SendToClient(message);
    }

    public void Logout()
    {
        var message = new IRequestMessage()
        {
            MessageType = RequestMessageType.Logout,
        };
        this.SendToClient(message);
    }

    private void SendToClient(IRequestMessage message)
    {
        debugText.text = "loading...";
        var jsonString = JsonConvert.SerializeObject(message);
        Debug.Log(jsonString);
        OnSendToClient(jsonString);
    }

    IEnumerator GetGlobalSettings(float time)
    {
        yield return new WaitForSeconds(time);

        UpdateGlobalSettings();
    }
}