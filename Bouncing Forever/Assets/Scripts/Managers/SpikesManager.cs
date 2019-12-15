using UnityEngine;

public class SpikesManager : MonoBehaviour
{
    public static SpikesManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    private float spikesTimer; // Timer that keeps track of the time since last spawn
    private float spikesGap; // The variable that keeps track of the current gap

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
        spikesTimer = 0f;
        spikesGap = startingGap;
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying == true)
        {
            // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
            spikesTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

            // Check if the timer is equal to or more than the gap time
            if (spikesTimer >= spikesGap)
            {
                // Get new pooled object
                GameObject obj = PoolManager.Instance.GetPooledObject("Spikes");
                // Set new objects position
                obj.transform.position = new Vector3(16f, -3.5f, 0f);
                // Activate object
                obj.SetActive(true);
                // Reset timer and pick new random gap time
                spikesTimer = 0f;
                spikesGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
