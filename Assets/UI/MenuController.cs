using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [HideInInspector] public static bool IsGamePaused = false;
    // Start is called before the first frame update
    public void Exit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        IsGamePaused = false;
    }
}
