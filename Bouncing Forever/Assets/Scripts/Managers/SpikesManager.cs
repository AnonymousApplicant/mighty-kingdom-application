using UnityEngine;

public class SpikesManager : SpawnableManager
{
    public static SpikesManager Instance; // variable that holds the instance for the singleton setup

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

    // Override the base start function, call the base start function, add this class to the spawnableClasses list
    public override void Start()
    {
        base.Start();
        HUDManager.Instance.spawnableClasses.Add(this);
    }
}
