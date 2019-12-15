using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    private float cloudsTimer; // Timer that keeps track of the time since last spawn
    private float cloudsGap; // The variable that keeps track of the current gap

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
        cloudsTimer = 0f;
        cloudsGap = startingGap;

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
        cloudsTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

        // Check if the timer is equal to or more than the gap time
        if (cloudsTimer >= cloudsGap)
        {
            // Get new pooled object
            GameObject obj = PoolManager.Instance.GetRandomPooledObject("Clouds");
            // Set new objects position
            obj.transform.position = new Vector3(16f, Random.Range(1.4f, 4f), 0f);
            // Activate object
            obj.SetActive(true);
            // Reset timer and pick new random gap time
            cloudsTimer = 0f;
            cloudsGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}