using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; // variable that holds the instance for the singleton setup

    [HideInInspector]
    public bool isPlaying; // Boolean that defines whether the game is being played or not (Start or End game etc)

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

    public TextFileReader reader; // Variable that holds the LogReader script
    private float bestScore = 0; // Keeps track of the highest score when using txtFileManager

    [HideInInspector]
    public List<SpawnableManager> spawnableClasses;

    void Awake()
    {
        // Check if the Instance variable is not null and not 'this'
        if (Instance != null && Instance != this)
        {
            // Destroy gameObject connected to this script if Instance is already defined
            Destroy(this.gameObject);
        }
        else
        {
            // Assign 'this' to the Instance variable if Instance is null
            Instance = this;
        }

        // Set target framerate to 60 to reduced unused frames
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Constantly update the score text to the current score
        sScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
    }

    // Triggered when retry button is pressed
    public void RetryClicked()
    {
        // // Sets static variable to true so play button is pressed immediatly 
        // MainMenu.retry = true;
        // // Reloads current scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        StartGame();

        foreach (SpawnableManager spawnableClass in spawnableClasses)
        {
            spawnableClass.TimerStart();
        }
    }

    // Trigger when the player steps into the finish trigger with all 3 pups
    public void EndGame()
    {
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

        // Set score texts text
        highscoreScore.SetText(reader.LoadStringByKey("highscore"));

        // Display UI
        smallScore.gameObject.SetActive(false);
        largeScore.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        highscoreObject.gameObject.SetActive(true);

        PlayerController.Instance.GoToSpawn();
        PlayerController.Instance.StopPhysics();
        // Stop the music playing
        SFXManager.Instance.music.Stop();
        // Set larger score text the current (end game) score
        lScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
        // Set is playing to false so difficulty stops increasing and non-scenic objects stop spawning
        isPlaying = false;
    }

    void StartGame()
    {
        smallScore.gameObject.SetActive(true);
        largeScore.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        highscoreObject.gameObject.SetActive(false);

        SFXManager.Instance.music.Play();

        PoolManager.Instance.RemoveDebris();

        DifficultyManager.Instance.difficultyStart();
        ScoreManager.Instance.ScoreStart();

        PlayerController.Instance.StartPhysics();

        isPlaying = true;
    }
}
