using UnityEngine;
using UnityEngine.UI;

public class SendOnClick : MonoBehaviour
{
    public enum CommandType
    {
        Action,
        Scene,
        Back
    }

    public CommandType commandType;
    [TextArea] public string msg;
    void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            var client = WebSocketClient.GetInstance();
            if (client == null)
            {
                Debug.LogWarning("Websocket client not present.");
                return;
            }

            string finalMessage;
            switch (commandType)
            {
                case CommandType.Back:
                    finalMessage = "Action:Back";
                    break;
                default:
                    finalMessage = $"{commandType}:{msg}";
                    break;
            }
            client.SendMessageToServer(finalMessage);
        });
    }
    
    #if UNITY_EDITOR
        void OnValidate()
        {
            if (commandType == CommandType.Back)
            {
                msg = string.Empty;
            }
        }
    #endif
}
