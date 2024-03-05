using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text scoreText; // Reference to the Text component displaying the score
    public TMP_Text highScoreText; // Reference to the Text component displaying the high score

    private int score;
    private int highScore;

    private int streakMultiplier = 1; // Multiplier increases with consecutive correct items

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load high score, default to 0 if not set
        UpdateHighScoreText();
        UpdateScoreText();
    }

    public void CorrectItemGiven()
    {
        score += 200 * streakMultiplier;
        streakMultiplier++; // Increase the streak multiplier for the next correct item
        Debug.Log("Score: " + score);
        UpdateScoreText();
        UpdateHighScore();
    }

    public void WrongItemGiven()
    {
        score -= 100;
        streakMultiplier = 1; // Reset the streak multiplier on wrong item
        if (score < 0) score = 0; // Optional: Prevent score from going negative
        Debug.Log("Score: " + score);
        UpdateScoreText();
        UpdateHighScore();
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // Save new high score
            UpdateHighScoreText();
            PlayerPrefs.Save(); // Don't forget to save the changes
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Optional: Method to get the current score if needed elsewhere in your game
    public int GetScore()
    {
        return score;
    }
}
