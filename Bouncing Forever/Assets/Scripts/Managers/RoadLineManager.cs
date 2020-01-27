using UnityEngine;

public class RoadLineManager : SpawnableManager
{
    public static RoadLineManager Instance; // variable that holds the instance for the singleton setup

    [Header("Initalize Settings")]
    [Tooltip("The amount to spawn at the start of the game")]
    public int initializeAmount;
    [Tooltip("The position to start spawning the objects from")]
    public float initializeSpawnPos;
    [Tooltip("The gap to put between each next object")]
    public float initializeGap;
    [Tooltip("The random range to spawn between for the Y value (set X to desired value and Y to 0 if no range needed)")]
    public Vector2 initializeY;

    void Awake()
    {
        // Run singleton check/setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Override the base start function, call the base start function, set scenery to true and initialize objects
    public override void Start()
    {
        base.Start();
        base.isScenery = true;
        SpawnObjects(initializeAmount, "RoadLines", initializeSpawnPos, initializeGap, initializeY, false);
    }
}