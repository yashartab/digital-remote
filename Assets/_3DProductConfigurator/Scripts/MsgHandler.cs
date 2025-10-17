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
            
            // Reply message handling
            if (type == "reply")
            {
                switch (action)
                {
                    // Change scene
                    case "changeScene":
                        replyMsg = ReplySceneChange(parameters);
                        break;
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
        
        private string ReplySceneChange(JObject parameters)
        {
            string sceneName = parameters?["sceneName"]?.ToString();
            bool success = SceneLoader.LoadScene(sceneName);

            if (success)
                return "";
            
            return null;
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
        
        public void OnMainMenu()
        {
            string json = MessageBuilder.Build(
                "command",
                "changeScene",
                new Dictionary<string, object> { { "sceneName", "MainMenu" } }
            );
            webSocketClient.SendMessageToServer(json);
        }
        
        #endregion OutgoingMessages
    }
}