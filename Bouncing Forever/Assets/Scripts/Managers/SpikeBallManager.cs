using UnityEngine;

public class SpikeBallManager : SpawnableManager
{
    public static SpikeBallManager Instance; // variable that holds the instance for the singleton setup

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

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying == true)
        {
            // Increase timer
            currentTimer += Time.deltaTime;

            // Check if the timer is equal to or more than the gap time
            if (currentTimer >= currentGap)
            {
                // Get new pooled object
                GameObject obj = PoolManager.Instance.GetPooledObject("SpikeBalls");
                // Set new objects position
                obj.transform.position = new Vector3(16f, Random.Range(2f, -1f), 0f);
                // Activate object
                obj.SetActive(true);
                // Reset timer and pick new random gap time
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
