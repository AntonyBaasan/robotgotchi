using UnityEngine;
using UnityEngine.UI;
// use web3.jslib
using System.Runtime.InteropServices;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine.Serialization;

public class MessageSender : MonoBehaviour
{
    public Text debugText;
    public Text inputText;

    [DllImport("__Internal")]
    private static extern void OnSendToClient(string messageText);

    public void SendEchoMessage()
    {
        var echoMessage = new Message
        {
            FunctionName = FunctionName.Echo,
            Data = "Hello World"
        };
        this.SendToClient(echoMessage);
    }
    
    public void Login()
    {
        var message = new Message
        {
            FunctionName = FunctionName.Login,
        };
        this.SendToClient(message);
    }
    
    public void GetWalletAddress()
    {
        var message = new Message
        {
            FunctionName = FunctionName.GetWalletAddress,
        };
        this.SendToClient(message);
    }
    
    private void SendToClient(Message message)
    {
        debugText.text = "loading...";
        var jsonString = JsonConvert.SerializeObject(message);
        Debug.Log(jsonString);
        OnSendToClient(jsonString);
    }
}