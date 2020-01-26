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
                SpawnObject("SpikeBalls", 16f, new Vector2(2f, -1f), false);
                // Reset timer and pick new random gap time
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
