using UnityEngine;

public class AppControl : MonoBehaviour
{
    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        // If the escape button is pressed, quit the application
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    public void OnChangeScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }
    
    public void OnQuit()
    {
        Application.Quit();
    }
}
