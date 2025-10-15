using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ProductConfigurator
{
    public class MsgHandler : MonoBehaviour, IMsgHandler 
    {
        private WebSocketClient webSocketClient;
        
        void Start()
        {
            webSocketClient = FindFirstObjectByType<WebSocketClient>();
             
            if  (webSocketClient == null)
                Debug.LogError("No WebSocket client found!");
        }

        #region IncomingMessages
        
        public string HandleMessage(string msg)
        {
            // Parse message to json object 
            JObject json = JObject.Parse(msg);

            // Get message type, action and parameters
            string type = json["type"]?.ToString();
            string action = json["action"]?.ToString();
            var parameters = json["parameters"] as JObject;
        
            Debug.Log($"Received type: {type}");
            Debug.Log($"Received action: {action}");
            Debug.Log($"Received parameters: {parameters}");
            
            // Default reply message
            string replyMsg = "";
            
            // Message handling
            if (type == "reply")
            {
                switch (action)
                {
                    // Reset product
                    case "resetProduct":
                        // replyMsg = HandleProductReset();
                        break;
                    // Rotate product
                    case "rotateProduct":
                        // replyMsg = HandleProductRotation(parameters);
                        break;
                    // No corresponding action
                    default:
                        replyMsg = null;
                        break;
                }
            }
            
            return replyMsg;
        } 
        
        #endregion IncomingMessages
        
        #region OutgoingMessages

        public void OnResetProduct()
        {
            string json = MessageBuilder.Build(
                "command",
                "resetProduct",
                new Dictionary<string, object> { }
            );
            webSocketClient.SendMessageToServer(json);
        }
        
        public void OnRotateProduct(string direction)
        {
            string json = MessageBuilder.Build(
                "command",
                "rotateProduct",
                new Dictionary<string, object> { { "direction", direction } }
            );
            webSocketClient.SendMessageToServer(json);
        }
        
        #endregion OutgoingMessages
    }
}