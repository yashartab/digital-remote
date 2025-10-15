using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace RahmHeroInterview
{
    public class MsgHandler : MonoBehaviour, IMsgHandler 
    {
        [SerializeField] HeroSelection heroSelection;
        [SerializeField] TopicSelection topicSelection;
        
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
                    // Change scene
                    case "changeScene":
                        replyMsg = HandleSceneChange(parameters);
                        break;
                    // Select hero
                    case "selectHero":
                        HandleHeroSelection(parameters);
                        break;
                    // Select topic
                    case "selectTopic":
                        HandleTopicSelection(parameters);
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
                return "";
            
            return null;
        }

        private void HandleHeroSelection(JObject parameters)
        {
            int heroID = parameters?["heroID"]?.ToObject<int>() ?? 1;
            
            // TODO
            
            heroSelection.gameObject.SetActive(false);
            topicSelection.gameObject.SetActive(true);
        }

        private void HandleTopicSelection(JObject parameters)
        {
            int topicID = parameters?["topicID"]?.ToObject<int>() ?? 0;

            // TODO
        }

        #endregion IncomingMessages
        
        #region OutgoingMessages

        public void OnSelectHero(int heroID)
        {
            string json = MessageBuilder.Build(
                "command",
                "selectHero",
                new Dictionary<string, object> { { "heroID", heroID } }
            );
            webSocketClient.SendMessageToServer(json);
        }

        public void OnSelectTopic(int topicID)
        {
            string json = MessageBuilder.Build(
                "command",
                "selectTopic",
                new Dictionary<string, object> { { "topicID", topicID } }
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