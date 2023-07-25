using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // a Funtion That will Trigger the Next Scene
    public void NextScene()
    {
        // Get the Current Scene
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        // Get the Current Scene Name
        string sceneName = currentScene.name;
        // Get the Current Scene Index
        int sceneIndex = currentScene.buildIndex;
        // Load the Next Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex + 1);
    }
}
