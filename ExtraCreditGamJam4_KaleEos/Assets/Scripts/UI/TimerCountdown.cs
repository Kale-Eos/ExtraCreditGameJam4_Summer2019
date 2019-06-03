using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    AudioManager audioManager;

    private float timer;
    private bool canCount = true;
    private bool playOnce = false;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timer = mainTimer;

        Scene currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("f");
        }

        else if (timer <= 0.0f && !playOnce)
        {
            canCount = false;
            playOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            GameOver();
        }

    }

    void GameOver()
    {
        audioManager.StopSound("Level1_BGM");
        audioManager.PlaySound("GameOver_BGM");
        SceneManager.LoadScene("GameOver");
    }

    public void ResetButton()
    {
        timer = mainTimer;
        canCount = true;
        playOnce = false;
    }
}
