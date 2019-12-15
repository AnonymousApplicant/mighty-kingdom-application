using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("Contains the music audio")]
    public AudioSource music;

    [Tooltip("Contains the jumping audio")]
    public AudioSource jump;
    [Tooltip("Contains the doubleJumping audio")]
    public AudioSource doubleJump;
    [Tooltip("Contains the coinCollecting audio")]
    public AudioSource coinCollect;
    [Tooltip("Contains the playerPopping audio")]
    public AudioSource playerPop;

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
}
