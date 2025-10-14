using Unity.XR.CoreUtils.Collections;

public static class Messages 
{
    [System.Serializable]
    public class Message
    {
        public string type;          // z.B. "command", "input", "event", "status"
        public string action;        // z.B. "changeScene", "moveObject", "setSpeed"
        public SerializableDictionary<string, string> parameters; // flexibel für alle Zusatzinfos
    }
}
