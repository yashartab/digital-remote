using UnityEngine;

public class AppControl : MonoBehaviour
{
    private static AppControl instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
