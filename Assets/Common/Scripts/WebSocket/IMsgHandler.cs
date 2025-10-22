
// Interface for all message handlers
public interface IMsgHandler
{
    string HandleMessage(string msg);
    
    void OnSceneLoaded(string sceneName);
}