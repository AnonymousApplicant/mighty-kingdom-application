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

    void Start()
    {
        // For loop for start of the game object spawns
        for (int i = 0; i < 8; i++)
        {
            // Get a new pooled object
            GameObject obj = PoolManager.Instance.GetRandomPooledObject("Clouds");
            // Place its X position at -16f + (4f * i) so each new object is 4f further than the other
            obj.transform.position = new Vector3(-16f + (4f * i), Random.Range(1.4f, 4f), 0f);
            // Acivate the object
            obj.SetActive(true);
        }
    }

    void Update()
    {
        // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
        currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

        // Check if the timer is equal to or more than the gap time
        if (currentTimer >= currentGap)
        {
            // Get new pooled object
            GameObject obj = PoolManager.Instance.GetRandomPooledObject("Clouds");
            // Set new objects position
            obj.transform.position = new Vector3(16f, Random.Range(1.4f, 4f), 0f);
            // Activate object
            obj.SetActive(true);
            // Reset timer and pick new random gap time
            currentTimer = 0f;
            currentGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}