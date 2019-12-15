using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // variable that holds the instance for the singleton setup

    [HideInInspector]
    public float currentScore; // Public variable that holds the currentScore

    [HideInInspector]
    public float coinScore; // The variabel that tracks the score from coins

    private float timer; // Timer variable to track timer
    [SerializeField]
    [Tooltip("The multiplier to multiply time by to get base score")]
    private float timeMultiplier;

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
        // Set timer to 0f
        timer = 0f;
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying)
        {
            // Increase timer
            timer += Time.deltaTime;
            // Set currentScore to timer * its multiplier, plus the coinScore
            currentScore = (timer * timeMultiplier) + coinScore;
        }
    }
}
