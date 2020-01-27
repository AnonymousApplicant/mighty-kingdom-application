using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // variable that holds the instance for the singleton setup

    [Header("Music")]
    [Tooltip("Contains the music audio")]
    public AudioSource music;

    [Header("SFX")]
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
}
