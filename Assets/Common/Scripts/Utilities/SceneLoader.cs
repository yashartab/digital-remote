using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static IEnumerator LoadScene(string sceneName)
    {
        // If we are already in the scene, do nothing
        if (sceneName == SceneManager.GetActiveScene().name)
            yield break;
        
        // Validate if the parameter is a valid scene name
        if (IsSceneAvailable(sceneName))
        {
            // Load scene async
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading Scene ...");
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("Scene '" + sceneName + "' not found in build settings!");
            yield return null;
        }
    }
    
    // Checks if the scene is available in the build settings
    private static bool IsSceneAvailable(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }
        return false;
    }
}