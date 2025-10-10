using System;
using UnityEngine;

using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket webSocket;
    async void Start()
    {
        try
        {
            webSocket = new WebSocket("ws://localhost:443");
            
            webSocket.OnOpen += () =>
            {
                Debug.Log("Connection open!");
            };

            webSocket.OnError += (e) =>
            {
                Debug.Log("Error! " + e);
            };

            webSocket.OnClose += (e) =>
            {
                Debug.Log("Connection closed!");
            };

            webSocket.OnMessage += (bytes) =>
            {
                string message = System.Text.Encoding.UTF8.GetString(bytes);
                Debug.Log($"Message from server: {message}");
                if (message.StartsWith("SceneLoaded:"))
                {
                    string sceneName = message.Split(':')[1];
                    Debug.Log($"✅ Scene '{sceneName}' wurde auf dem Server erfolgreich geladen!");
                }
                else if (message.StartsWith("SceneNotFound:"))
                {
                    string sceneName = message.Split(':')[1];
                    Debug.LogWarning($"⚠️ Scene '{sceneName}' existiert nicht auf dem Server!");
                }
            };
            
            await webSocket.Connect();
        }
        catch (Exception e)
        {
            Debug.LogError("WebSocket connection failed: " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            webSocket.DispatchMessageQueue();
        #endif
    }

    // Method for sending messages through the WebSocket
    public async void SendMessageToServer(string message)
    {
        try
        {
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.SendText(message);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Sending message failed: " + e.Message);
        }
    }

    private async void OnApplicationQuit()
    {
        try
        {
            await webSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("WebSocket close failed: " + e.Message);
        }
    }
}
