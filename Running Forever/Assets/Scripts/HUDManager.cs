using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; // variable that holds the instance for the singleton setup

    [HideInInspector]
    public bool isPlaying; // Boolean that defines whether the game is being played or not (Start or End game etc)

    [SerializeField]
    private TextMeshProUGUI sScore; // Variable that holds the small score's text
    [SerializeField]
    private TextMeshProUGUI lScore; // Variable that holds the large score's text

    [SerializeField]
    private GameObject smallScore; // Variable that holds the small score object
    [SerializeField]
    private GameObject largeScore; // Variable that holds the large score object
    [SerializeField]
    private GameObject retryButton; // Variable that holds the retry button

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
        // Sets static variable to true so play button is pressed immediatly 
        MainMenu.retry = true;
        // Reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Trigger when the player steps into the finish trigger with all 3 pups
    public void EndGame()
    {
        // Display UI
        smallScore.gameObject.SetActive(false);
        largeScore.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        // Set larger score text the current (end game) score
        lScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
        // Set is playing to false so difficulty stops increasing and non-scenic objects stop spawning
        isPlaying = false;
    }
}
