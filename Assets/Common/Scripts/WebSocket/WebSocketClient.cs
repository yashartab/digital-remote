using System;
using UnityEngine;
using NativeWebSocket;
using UnityEngine.SceneManagement;
using System.Collections.Concurrent;

public class WebSocketClient : MonoBehaviour
{
    // WebSocket client and its instance
    private WebSocket webSocket;
    private static WebSocketClient instance;
    
    // The current message handler of the scene
    private IMsgHandler currentMsgHandler;
    
    // Queue containing all incoming messages from the server
    private readonly ConcurrentQueue<string> msgQueue = new();

    private void Awake()
    {
        // Check if we are already running the client
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        // Make this object persistent
        DontDestroyOnLoad(gameObject);
        // Also updates the scenes while the app is in the background
        Application.runInBackground = true;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private async void Start()
    {
        try
        {
            // Initialize the websocket client
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
                
                // Emit the message to the unity main-thread
                msgQueue.Enqueue(message);
            };
            
            await webSocket.Connect();
        }
        catch (Exception e)
        {
            Debug.LogError("WebSocket connection failed: " + e.Message);
        }
    }
    
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            webSocket.DispatchMessageQueue();
        #endif
        
        // Process all messages in the queue
        while (msgQueue.TryDequeue(out string msg))
        {
            HandleMessage(msg);
        }
    }
    
    // Handles incoming messages
    private void HandleMessage(string msg)
    {
        if (currentMsgHandler == null)
        {
            Debug.LogWarning($"Unhandled message: {msg}");
            return;
        }

        // Handle message depending on scene and get reply message
        string replyMsg = currentMsgHandler.HandleMessage(msg);

        if (!string.IsNullOrEmpty(replyMsg))
        {
            // Send reply message to clients
            SendMessageToServer(replyMsg);
        }
        else if (replyMsg == null)
        {
            Debug.LogWarning($"Unhandled message: {msg}");
        }
        
        
        // if (msg.StartsWith("SceneLoaded:"))
        // {
        //     string sceneName = msg.Split(':')[1];
        //     Debug.Log($"✅ Scene '{sceneName}' wurde auf dem Server erfolgreich geladen!");
        //     SceneManager.LoadScene(sceneName);
        // }
        // else if (msg.StartsWith("SceneNotFound:"))
        // {
        //     string sceneName = msg.Split(':')[1];
        //     Debug.LogWarning($"⚠️ Scene '{sceneName}' existiert nicht auf dem Server!");
        // }
        // else if (msg.StartsWith("HeroSelected:"))
        // {
        //     Debug.Log(msg);
        //     if (SceneManager.GetActiveScene().name == "RahmHeroInterview")
        //     {
        //         GameObject heroSelection = FindFirstObjectByType<HeroSelection>(FindObjectsInactive.Include).gameObject;
        //         GameObject topicSelection = FindFirstObjectByType<TopicSelection>(FindObjectsInactive.Include).gameObject;
        //         if (heroSelection != null)
        //             heroSelection.SetActive(false);
        //         if (topicSelection != null)
        //             topicSelection.SetActive(true);
        //     }
        // }
        // else if (msg.StartsWith("TopicSelected:"))
        // {
        //     Debug.Log(msg);
        // }
    }
    
    // Sends a message to the WebSocket server
    public async void SendMessageToServer(string msg)
    {
        try
        {
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.SendText(msg);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Sending message failed: " + e.Message);
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and set the scene handler of the current active scene
        currentMsgHandler = FindMsgHandler(scene);
    }
    
    // Returns the message handler of the current scene
    private static IMsgHandler FindMsgHandler(Scene scene)
    {
        if (!scene.IsValid() || !scene.isLoaded)
        {
            return null;
        }

        // Iterate through all objects in scene and find the message handler
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            IMsgHandler handler = root.GetComponentInChildren<IMsgHandler>(includeInactive: true);
            if (handler != null)
            {
                return handler;
            }
        }

        return null;
    }
    
    public static WebSocketClient GetInstance()
    {
        return instance;
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
