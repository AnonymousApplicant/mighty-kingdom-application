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

    /// <summary>
    /// Starts the game by activating physics and timers, and toggling the hud
    /// </summary>
    public void PlayGame()
    {
        HUDManager.Instance.isPlaying = true;
        PlayerController.Instance.StartPhysics();
        menu.SetActive(false);
        score.SetActive(true);
    }
}
