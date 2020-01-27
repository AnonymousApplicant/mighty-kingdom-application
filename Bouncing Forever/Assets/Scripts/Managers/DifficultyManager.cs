using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The difficulty at which to start")]
    public float startingDifficulty;
    [SerializeField]
    [Tooltip("The multiplier to multiply the difficulty by")]
    private float multiplier;
    [HideInInspector]
    public float difficulty; // Variable that holds the current difficulty

    private float timer; // Timer to keep track of time

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

    // Run difficultyStart method
    void Start()
    {
        difficultyStart();
    }

    // If the game is playing, increase the timer and when it goes over 1f, multiply the difficulty and reset
    void Update()
    {
        if (HUDManager.Instance.isPlaying && difficulty < 20f)
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                difficulty = difficulty * multiplier;
                timer = 0f;
            }
        }
    }

    /// <summary>
    /// Set the difficulty to the startingDifficulty value
    /// </summary>
    public void difficultyStart()
    {
        difficulty = startingDifficulty;
    }
}
