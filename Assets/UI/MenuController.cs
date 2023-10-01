using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [HideInInspector] public static bool IsGamePaused = false;
    // Start is called before the first frame update
    [SerializeField]
    public GameObject HUD;
    public GameObject DeathPanel;
    public GameObject PausePanel;
    private float timer = 0f;
    public TMP_Text TimerText;
    public TMP_Text Gameovertimer;
    private void Start()
    {   IsGamePaused = true;
        StartTimer();
        if(IsGamePaused == true)
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
        IsGamePaused = true;
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
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        // Format the timer text with minutes and seconds
        string timerTextString = string.Format("You Survived: {0:00}:{1:00}", minutes, seconds);

        Gameovertimer.text = timerTextString;
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
    private void StartTimer()
    {
        InvokeRepeating("UpdateTimer", 1f, 1f); // Update the countdown every second
    }
    private void UpdateTimer()
    {
        timer++;
        UpdateTimerDisplay();

    
    }

    private void UpdateTimerDisplay()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        // Format the timer text with minutes and seconds
        string timerTextString = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        // Update the UI text
        TimerText.text = timerTextString;
    }

    public void pausebuttonpressed()
    {
        if(IsGamePaused == true)
        {
            Resume();
        }
        else
        {
            pauseButton();
        }
    }
    private void Resume()
    {
        PausePanel.SetActive(false);
       PauseControl();
    }

    public void pauseButton()
    {
        PauseControl();
        PausePanel.SetActive(true);
    }
}
