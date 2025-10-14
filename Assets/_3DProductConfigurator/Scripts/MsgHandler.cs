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
            // TODO based on the needs of this scene
            Debug.Log(msg);

            return "";
        } 
        
        #endregion IncomingMessages
        
        #region OutgoingMessages
        
        #endregion OutgoingMessages
    }
}