using UnityEngine;

public class SpawnableManager : MonoBehaviour
{
    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    [HideInInspector]
    public float currentTimer; // Timer that keeps track of the time since last spawn
    [HideInInspector]
    public float currentGap; // The variable that keeps track of the current gap

    void Start()
    {
        // Set timer to 0f and set gap to the startingGap
        currentTimer = 0f;
        currentGap = startingGap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
