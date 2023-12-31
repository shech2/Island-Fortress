using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    // Singletone SceneManager
    public static SceneManager Instance { get; private set; }

    public float Health = 100f;
    // Awake is called before Start
    private void Awake()
    {
        // Check if there is an instance of the SceneManager
        if (Instance == null)
        {
            // If not, set it to this
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If there is an instance of the SceneManager
        else
        {
            // Destroy this
            Destroy(gameObject);
        }
    }

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
    public void ResetScene()
    {
        // Get the Current Scene
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        // Get the Current Scene Name
        string sceneName = currentScene.name;
        // Get the Current Scene Index
        int sceneIndex = currentScene.buildIndex;
        // Load the Next Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
