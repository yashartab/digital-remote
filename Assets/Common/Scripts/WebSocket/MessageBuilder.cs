using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json; 

public class MessageBuilder
{
    public static string Build(string type, string action, Dictionary<string, object> parameters = null)
    {
        var msg = new Dictionary<string, object>
        {
            {"type", type},
            {"action", action},
            {"parameters", parameters ?? new Dictionary<string, object>()}
        };
        return JsonConvert.SerializeObject(msg);
    }
}