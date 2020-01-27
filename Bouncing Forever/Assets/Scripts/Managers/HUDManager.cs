using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; // variable that holds the instance for the singleton setup

    [HideInInspector]
    public bool isPlaying; // Boolean that defines whether the game is being played or not (Start or End game etc)

    [Header("Score Text Variables")]
    [SerializeField]
    [Tooltip("Variable that holds the small scores's text")]
    private TextMeshProUGUI sScore;
    [SerializeField]
    [Tooltip("Variable that holds the large scores's text")]
    private TextMeshProUGUI lScore;
    [SerializeField]
    [Tooltip("Variable that holds the highscore text")]
    private TextMeshProUGUI highscoreText;
    [SerializeField]
    [Tooltip("Variable that holds the highscore score object")]
    private TextMeshProUGUI highscoreScore;

    [Header("Score Object Variables")]
    [SerializeField]
    [Tooltip("Variable that holds the highscore text object")]
    private GameObject highscoreObject;
    [SerializeField]
    [Tooltip("Variable that holds the small scores object")]
    private GameObject smallScore;
    [SerializeField]
    [Tooltip("Variable that holds the large scores object")]
    private GameObject largeScore;
    [SerializeField]
    [Tooltip("Variable that holds the retry button")]
    private GameObject retryButton;

    [SerializeField]
    [Tooltip("Variable that holds Text File Reader component")]
    private TextFileReader reader; // Variable that holds the LogReader script
    private float bestScore = 0; // Keeps track of the highest score when using txtFileManager

    [HideInInspector]
    public List<SpawnableManager> spawnableClasses;

    void Awake()
    {
        // Run singleton check/setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Constantly update the score text to the current score
    void Update()
    {
        sScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
    }

    /// <summary>
    /// Runs the StartGame method and puts any objects still spawned in, back into the pool
    /// </summary>
    public void RetryClicked()
    {
        StartGame();

        foreach (SpawnableManager spawnableClass in spawnableClasses)
        {
            spawnableClass.TimerStart();
        }
    }

    /// <summary>
    /// Triggered by player death, Resets the level and shows the end screen
    /// </summary>
    public void EndGame()
    {
        // Load the latest highscore, compare it and or check if there is no highscore
        bestScore = reader.LoadFloatByKey("highscore");
        if (ScoreManager.Instance.currentScore > bestScore)
        {
            reader.SaveKeyValuePair("highscore", ScoreManager.Instance.currentScore.ToString("F2"), false);
            highscoreText.SetText("NEW Highscore");
        }
        else if (bestScore == 0)
        {
            reader.SaveKeyValuePair("highscore", ScoreManager.Instance.currentScore.ToString("F2"), false);
        }
        
        // Set score texts
        highscoreScore.SetText(reader.LoadStringByKey("highscore"));
        lScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));

        // Toggle UI
        smallScore.gameObject.SetActive(false);
        largeScore.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        highscoreObject.gameObject.SetActive(true);

        // Stop the music, player physics and set isPlaying to false
        PlayerController.Instance.GoToSpawn();
        PlayerController.Instance.StopPhysics();
        SFXManager.Instance.music.Stop();
        isPlaying = false;
    }

    /// <summary>
    /// Triggered by the retry button, sets the UI up and music, removes debris and resets the difficulty, starts the game and player
    /// </summary>
    void StartGame()
    {
        // Toggle UI
        smallScore.gameObject.SetActive(true);
        largeScore.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        highscoreObject.gameObject.SetActive(false);

        // Reset the level and score
        PoolManager.Instance.RemoveDebris();
        DifficultyManager.Instance.difficultyStart();
        ScoreManager.Instance.ScoreStart();

        // Start the music, player physics and sets isPlaying to true
        SFXManager.Instance.music.Play();
        PlayerController.Instance.StartPhysics();
        isPlaying = true;
    }
}
