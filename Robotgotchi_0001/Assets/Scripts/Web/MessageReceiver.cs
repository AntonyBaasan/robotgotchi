using System.Linq;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class MessageReceiver : MonoBehaviour
{
     public Text debugText;

     public void ReceiveClientMessage(string messageText)
     {
         var message = JsonConvert.DeserializeObject<Message>(messageText);
         switch (message.FunctionName)
         {
             case FunctionName.Echo:
                 HandleEcho(message);
                 break;
             case FunctionName.LoginResult:
                 HandleLoginResult(message);
                 break;
             case FunctionName.GetWalletAddressResponse:
                 HandleGetWalletAddressResponse(message);
                 break;
             default:
                 Debug.Log("No handler for a message:");
                 Debug.Log(message);
                 break;
         }

     }

     private void HandleEcho(Message message)
     {
         debugText.text = message.Data.ToString();
     }
     
     private void HandleLoginResult(Message message)
     {
         debugText.text = message.Data.ToString();
     }
     
     
     private void HandleGetWalletAddressResponse(Message message)
     {
         var walletAddress = message.Data.ToString();
         debugText.text = walletAddress;
     }

     public void UpdateText(string text)
    {
        Debug.Log("Message Received!");
        debugText.text = text;
    }

    public void UpdateArray(string text)
    {
        Debug.Log("Message Received!");
        if (!string.IsNullOrEmpty(text) && text.Split(',').Length > 0)
        {
            var textArray = text.Split(',');

            // debugText.text = string.Join('\n', textArray.Select((t, i) => i + ") " + t));
        }
        else
        {
            debugText.text = text;
        }
    }

}
