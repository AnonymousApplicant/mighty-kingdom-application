using UnityEngine;

public class RoadLineManager : MonoBehaviour
{
    public static RoadLineManager Instance; // variable that holds the instance for the singleton setup

    public float startingGap; // The starting time to wait for the first object to be introduced
    public float gapTime; // The time between gaps after the first one

    private float roadLineTimer; // Timer that keeps track of the time since last spawn
    private float roadLineGap; // The variable that keeps track of the current gap

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
        roadLineTimer = 0f;
        roadLineGap = startingGap;

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
        roadLineTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / 9);

        // Check if the timer is equal to or more than the gap time
        if (roadLineTimer >= roadLineGap)
        {
            // Get new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("RoadLines");
            // Set new objects position
            obj.transform.position = new Vector3(16f, -4.5f, 0f);
            // Activate object
            obj.SetActive(true);
            // Reset timer and pick new random gap time
            roadLineTimer = 0f;
            roadLineGap = gapTime;
        }
    }
}