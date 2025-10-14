using UnityEngine;

namespace RahmHeroInterview
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

        public string HandleMessage(string msg)
        {
            // TODO based on the needs of this scene
            Debug.Log(msg);

            return "";
        } 
    }
}