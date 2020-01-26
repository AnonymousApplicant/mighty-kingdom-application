using UnityEngine;

public class PlatformManager : SpawnableManager
{
    public static PlatformManager Instance; // variable that holds the instance for the singleton setup

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
    
    public override void Start()
    {
        base.Start();
        HUDManager.Instance.spawnableClasses.Add(this);
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
                SpawnObject("Platforms", 17f, new Vector2(2.5f, 0f), false);

                // Execute SpawnPlatformCoins() method so 3 coins spawn above the platform at the same time
                CoinManager.Instance.SpawnPlatformCoins();

                // Reset timer and pick new random gap time
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
