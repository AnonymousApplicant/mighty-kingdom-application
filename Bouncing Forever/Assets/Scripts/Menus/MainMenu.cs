using UnityEngine;

/// <summary>
/// Manages main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance; // Instance variable that holds the singleton of this class

    [Tooltip("Satic variable that if true on Start() will press play for you (For retry button)")]
    public static bool retry;

    [SerializeField]
    [Tooltip("The GameObject holding the menu objects")]
    private GameObject menu;
    [SerializeField]
    [Tooltip("The GameObject holding the score objects")]
    private GameObject score;

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

    /// <summary>
    /// Starts the game by activating physics and timers
    /// </summary>
    public void PlayGame()
    {
        // Set is playing to true
        HUDManager.Instance.isPlaying = true;
        // Start the physics on the player controllers rigidbody
        PlayerController.Instance.StartPhysics();
        // Hide menu items, show score items
        menu.SetActive(false);
        score.SetActive(true);
    }
}
