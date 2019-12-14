using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.SetText(ScoreManager.Instance.currentScore.ToString("F2"));
    }

    // Triggered when retry button is pressed
    public void RetryClicked()
    {
        // Reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Triggered when exit button is pressed
    public void QuitClicked()
    {
        Debug.Log("GAME QUIT!");
        Application.Quit();
    }

    // Trigger when the player steps into the finish trigger with all 3 pups
    public void EndGame()
    {
        // Display UI
        //congrats.gameObject.SetActive(true);
        //retry.gameObject.SetActive(true);

        // Change timeScale back to 1
        Time.timeScale = 0f;
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
