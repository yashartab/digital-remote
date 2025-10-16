using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MainMenu
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
            
            // Command message handling
            if (type == "command")
            {
                switch (action)
                {
                    // Change scene
                    case "changeScene":
                        replyMsg = HandleSceneChange(parameters);
                        break;
                    // Quit application
                    case "quitApplication":
                        HandleApplicationQuit(parameters);
                        break;
                    // No corresponding action
                    default:
                        replyMsg = null;
                        break;
                }
            }
            
            // Reply message handling
            if (type == "reply")
            {
                switch (action)
                {
                    // Change scene
                    case "changeScene":
                        replyMsg = ReplySceneChange(parameters);
                        break;
                    // No corresponding action
                    default:
                        replyMsg = null;
                        break;
                }
            }
            
            return replyMsg;
        }
        
        private string HandleSceneChange(JObject parameters)
        {
            string sceneName = parameters?["sceneName"]?.ToString();
            bool success = SceneLoader.LoadScene(sceneName);

            if (success)
            {
                // Return reply message
                return MessageBuilder.Build(
                    "reply",
                    "changeScene",
                    new Dictionary<string, object> { { "sceneName", sceneName } }
                );
            }
            
            return null;
        }
        
        private void HandleApplicationQuit(JObject parameters)
        {
            Application.Quit();
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

        public void OnChangeScene(string sceneName)
        {
            string json = MessageBuilder.Build(
                "command",
                "changeScene",
                new Dictionary<string, object> { { "sceneName", sceneName } }
            );
            webSocketClient.SendMessageToServer(json);
        }

        public void OnQuit()
        {
            string json = MessageBuilder.Build(
                "command",
                "quitApplication",
                new Dictionary<string, object> { }
            );
            webSocketClient.SendMessageToServer(json);
            
            Application.Quit();
        }

        #endregion OutgoingMessages
    }
}