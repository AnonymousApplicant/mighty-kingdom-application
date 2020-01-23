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

    void Start()
    {
        // For loop for start of the game object spawns
        for (int i = 0; i < 7; i++)
        {
            // Get a new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("RoadLines");
            // Place its X position at -15f + (4.5f * i) so each new object is 4.5f further than the other
            obj.transform.position = new Vector3(-15f + (4.5f * i), -4.5f, 0f);
            // Acivate the object
            obj.SetActive(true);
        }
    }

    void Update()
    {
        // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
        currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / 9);

        // Check if the timer is equal to or more than the gap time
        if (currentTimer >= currentGap)
        {
            // Get new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("RoadLines");
            // Set new objects position
            obj.transform.position = new Vector3(16f, -4.5f, 0f);
            // Activate object
            obj.SetActive(true);
            // Reset timer and pick new random gap time
            currentTimer = 0f;
            currentGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}