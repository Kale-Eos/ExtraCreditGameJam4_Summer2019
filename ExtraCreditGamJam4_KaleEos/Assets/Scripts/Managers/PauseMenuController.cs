using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    AudioManager audioManager;

    public string sceneName;
    public static bool PauseGame = false;
    public GameObject pauseUI;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();                       // resumes game
            }

            else
            {
                Pause();                        // pauses game
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);               // unpauses game
        Time.timeScale = 1f;                    // time is unfrozen
        PauseGame = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);                // pauses game
        Time.timeScale = 0f;                    // time is frozen
        PauseGame = true;
    }

    public void BackToMainMenu1()
    {
        Time.timeScale = 1f;                        // time is unfrozen
        audioManager.StopSound("Level1_BGM");       // Stop Level2 Music
        audioManager.PlaySound("Music");            // Restarts main menu music
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenu2()
    {
        Time.timeScale = 1f;                        // time is unfrozen
        audioManager.StopSound("Level2_BGM");       // Stop Level2 Music
        audioManager.PlaySound("Music");            // Restarts main menu music
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenu3()
    {
        Time.timeScale = 1f;                        // time is unfrozen
        audioManager.StopSound("Level3_BGM");       // Stop Level3 Music
        audioManager.PlaySound("Music");            // Restarts main menu music
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenuFreeRoam()
    {
        Time.timeScale = 1f;                        // time is unfrozen
        audioManager.StopSound("FreeRoam");         // Stop Free Roam Music
        audioManager.PlaySound("Music");            // Restarts main menu music
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();                         // quits game
    }
}
