using UnityEngine;

public class SpikeBallManager : MonoBehaviour
{
    public static SpikeBallManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    private float spikeBallTimer; // Timer that keeps track of the time since last spawn
    private float spikeBallGap; // The variable that keeps track of the current gap

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
        // Set timer to 0f and set gap to the startingGap
        spikeBallTimer = 0f;
        spikeBallGap = startingGap;
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying == true)
        {
            // Increase timer
            spikeBallTimer += Time.deltaTime;

            // Check if the timer is equal to or more than the gap time
            if (spikeBallTimer >= spikeBallGap)
            {
                // Get new pooled object
                GameObject obj = PoolManager.Instance.GetPooledObject("SpikeBalls");
                // Set new objects position
                obj.transform.position = new Vector3(16f, Random.Range(2f, -1f), 0f);
                // Activate object
                obj.SetActive(true);
                // Reset timer and pick new random gap time
                spikeBallTimer = 0f;
                spikeBallGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
