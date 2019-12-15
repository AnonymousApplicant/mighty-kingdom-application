using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    private float platformTimer; // Timer that keeps track of the time since last spawn
    private float platformGap; // The variable that keeps track of the current gap

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
        platformTimer = 0f;
        platformGap = startingGap;
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying == true)
        {
            // Increase timer
            platformTimer += Time.deltaTime;

            // Check if the timer is equal to or more than the gap time
            if (platformTimer >= platformGap)
            {
                // Get new pooled object
                GameObject obj = PoolManager.Instance.GetPooledObject("Platforms");
                // Set new objects position
                obj.transform.position = new Vector3(17f, 2.5f, 0f);
                // Activate object
                obj.SetActive(true);

                // Execute SpawnPlatformCoins() method so 3 coins spawn above the platform at the same time
                CoinManager.Instance.SpawnPlatformCoins();

                // Reset timer and pick new random gap time
                platformTimer = 0f;
                platformGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
