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

    // Run the ScoreSart method
    void Start()
    {
        ScoreStart();
    }

    // Check if the game is currently playing, if so increase the timer and update the current score
    void Update()
    {
        if (HUDManager.Instance.isPlaying)
        {
            timer += Time.deltaTime;
            currentScore = (timer * timeMultiplier) + coinScore;
        }
    }

    // Re/Starts the score and timer
    public void ScoreStart()
    {
        timer = 0f;
        currentScore = 0f;
        coinScore = 0f;
    }
}
