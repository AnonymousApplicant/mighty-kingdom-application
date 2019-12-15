using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The time between gaps after the first one")]
    public float gapTime;

    private float treeTimer; // Timer that keeps track of the time since last spawn
    private float treeGap; // The variable that keeps track of the current gap

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
        treeTimer = 0f;
        treeGap = startingGap;

        // For loop for start of the game object spawns
        for (int i = 0; i < 2; i++)
        {
            // Get a new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("Trees");
            // Place its X position at -8f + (12f * i) so each new object is 12f further than the other
            obj.transform.position = new Vector3(-8f + (12f * i), 0f, 0f);
            // Acivate the object
            obj.SetActive(true);
        }
    }

    void Update()
    {
        // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
        treeTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / 9);

        // Check if the timer is equal to or more than the gap time
        if (treeTimer >= treeGap)
        {
            // Get new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("Trees");
            // Set new objects position
            obj.transform.position = new Vector3(16f, 0f, 0f);
            // Activate object
            obj.SetActive(true);
            // Reset timer and pick new random gap time
            treeTimer = 0f;
            treeGap = gapTime;
        }
    }
}