using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [HideInInspector]
    public bool isPlaying;

    [SerializeField]
    private TextMeshProUGUI sScore;
    [SerializeField]
    private TextMeshProUGUI lScore;

    [SerializeField]
    private GameObject smallScore;
    [SerializeField]
    private GameObject largeScore;
    [SerializeField]
    private GameObject retryButton;

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
    }

    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        sScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
    }

    // Triggered when retry button is pressed
    public void RetryClicked()
    {
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

        lScore.SetText(ScoreManager.Instance.currentScore.ToString("F2"));

        isPlaying = false;
    }

    // Converts floats into integers and display as time (90 = 01:30)
    private string TimeConvert(float time)
    {
        // Assign minutes and second variables 
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        // Format the result string
        string result = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Return result
        return result;
    }
}
