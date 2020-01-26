using UnityEngine;

public class CloudManager : SpawnableManager
{
    public static CloudManager Instance; // variable that holds the instance for the singleton setup

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
        InitializePool(5, "Clouds", -15f, 6f, new Vector2(1.4f, 4f));
    }

    void Update()
    {
        // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
        currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

        // Check if the timer is equal to or more than the gap time
        if (currentTimer >= currentGap)
        {
            SpawnObject("Clouds", 16f, new Vector2(1.4f, 4f), true);
            // Reset timer and pick new random gap time
            currentTimer = 0f;
            currentGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}