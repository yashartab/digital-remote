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
            webSocket = new WebSocket("wss://192.168.178.74:443");
            
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
                Debug.Log("Message: " + bytes);
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
