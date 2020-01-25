using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The difficulty at which to start")]
    public float startingDifficulty;
    [HideInInspector]
    public float difficulty; // Variable that holds the current difficulty
    [SerializeField]
    [Tooltip("The multiplier to multiply the difficulty by")]
    private float multiplier;

    private float timer; // Timer to keep track of time

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
        difficultyStart();
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying && difficulty < 20f)
        {
            // Increase timer
            timer += Time.deltaTime;

            // If timer is greater than or equal to 1(second)
            if (timer >= 1f)
            {
                // Increase difficulty by multiplier and reset timer
                difficulty = difficulty * multiplier;
                timer = 0f;
            }
        }
    }

    public void difficultyStart()
    {
        difficulty = startingDifficulty;
    }
}
