using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static bool LoadScene(string sceneName)
    {
        // Validate if the parameter is a valid scene name
        if (IsSceneAvailable(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            return true;
        }
        else
        {
            Debug.LogWarning("Scene '" + sceneName + "' not found in build settings!");
            return false;
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