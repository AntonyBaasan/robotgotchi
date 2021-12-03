using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class MessageReceiver : MonoBehaviour
{
     public Text debugText;

     public void ReceiveClientMessage(string messageText)
     {
         var message = JsonConvert.DeserializeObject<IResponseMessage>(messageText);
         switch (message.MessageType.ToLower())
         {
             case ResponseMessageType.EchoResponse:
                 HandleEcho(message);
                 break;
             case ResponseMessageType.UserInfo:
                 HandleUserInfo(message);
                 break;
             default:
                 Debug.Log("No handler for a message:");
                 Debug.Log(message);
                 break;
         }
     }

     private void HandleEcho(IResponseMessage message)
     {
         debugText.text = message.Payload.ToString();
     }
     
     private void HandleUserInfo(IResponseMessage message)
     {
         Debug.Log(message.ToString());
         debugText.text = message.Payload?.ToString();
     }
     
}
