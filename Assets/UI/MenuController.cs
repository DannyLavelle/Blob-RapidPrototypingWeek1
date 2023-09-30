using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [HideInInspector] public static bool IsGamePaused = false;
    // Start is called before the first frame update
    [SerializeField]
    public GameObject HUD;
    public GameObject DeathPanel;
    private void Start()
    {
        if(IsGamePaused =true)
        {
            PauseControl();
        }
    }
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
    void PauseControl()//Toggles Pause/Unpause
    {
        switch (IsGamePaused)
        {
            case false:
            Time.timeScale = 0f;
            IsGamePaused = true;
            UnlockCursor();
            RemoveHUD();
            break;
            case true:
            Time.timeScale = 1f;
            IsGamePaused = false;
            LockCursor();
            ActivateHUD();
            break;
        }
    }
    void RemoveHUD()
    {
        HUD.SetActive(false);
    }
    void ActivateHUD()
    {
        HUD.SetActive(true);
    }
    public void DeathSequence()
    {
        PauseControl();
        DeathPanel.SetActive(true);
    }
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
