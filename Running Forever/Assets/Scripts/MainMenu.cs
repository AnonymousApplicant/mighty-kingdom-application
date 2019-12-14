using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance; // Instance variable that holds the singleton of this class

    public static bool retry;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject score;

    void Awake()
    {
        /// Setup singleton: Check if the instance has already been set and is not already this class, if true destroy self, otherwise this is the singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (retry == true)
        {
            retry = false;
            PlayGame();
        }
    }

    // Method to load the next scene (executed by UI button press)
    public void PlayGame()
    {
        HUDManager.Instance.isPlaying = true;
        PlayerController.Instance.StartPhysics();
        menu.SetActive(false);
        score.SetActive(true);
    }
}
