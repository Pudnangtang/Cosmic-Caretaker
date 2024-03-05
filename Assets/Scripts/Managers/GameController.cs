using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float timeRemaining = 60; // 60 seconds for 1 minute
    private bool timerIsRunning = false;
    public TMP_Text timerText; // Reference to the Text component for displaying the timer

    public GameObject finalScoreScreen; // Reference to the final score screen UI
    public TMP_Text finalScoreText; // Reference to the text component displaying the final score
    public TMP_Text finalHighScoreText; // Reference to the text component displaying the final high score

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        finalScoreScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                EndGame();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = FormatTime(timeRemaining);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        // Stop the game, show final score, etc.
        ShowFinalScoreScreen();
    }

    private void ShowFinalScoreScreen()
    {
        finalScoreScreen.SetActive(true); // Activate the final score screen
        finalScoreText.text = "Final Score: " + ScoreManager.Instance.GetScore();
        finalHighScoreText.text = "High Score: " + ScoreManager.Instance.GetHighScore(); // Set high score text
    }

}
