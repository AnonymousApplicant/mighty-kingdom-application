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

    public static bool retry; // Satic variable that if true on Start() will press play for you (For retry button)

    [SerializeField]
    private GameObject menu; // The GameObject holding the menu items
    [SerializeField]
    private GameObject score; // The Gameobject holding the score items

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
        // Check if the static retry variable is true
        if (retry == true)
        {
            // Set retry back to false and execute PlayGame method (as if you clicked the play button)
            retry = false;
            PlayGame();
        }
    }

    /// <summary>
    /// Starts the game by activating physics and timers
    /// </summary>
    public void PlayGame()
    {
        // Set is playing to true
        HUDManager.Instance.isPlaying = true;
        // Start the physics onthe player controllers rigidbody
        PlayerController.Instance.StartPhysics();
        // Hide menu items, show score items
        menu.SetActive(false);
        score.SetActive(true);
    }
}
