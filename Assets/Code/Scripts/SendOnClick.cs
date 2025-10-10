using UnityEngine;
using UnityEngine.UI;

public class SendOnClick : MonoBehaviour
{
    [TextArea] public string msg;
    void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            if (WebSocketClient.GetInstance() != null)
            {
                WebSocketClient.GetInstance().SendMessageToServer(msg);
            }
            else
            {
                Debug.LogWarning("Websocket client not present.");
            }
        });
    }
}
