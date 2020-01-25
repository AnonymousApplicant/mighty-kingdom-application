using UnityEngine;

public class RoadLineManager : SpawnableManager
{
    public static RoadLineManager Instance; // variable that holds the instance for the singleton setup

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
        InitializePool(7, "RoadLines", -16f, 4.5f, new Vector2(-4.5f, 0f));
    }

    void Update()
    {
        // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
        currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

        // Check if the timer is equal to or more than the gap time
        if (currentTimer >= currentGap)
        {
            SpawnObject("RoadLines", 16f, new Vector2(-4.5f, 0f), false);
            // Reset timer and pick new random gap time
            currentTimer = 0f;
            currentGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}