using UnityEngine;

public class PlatformManager : SpawnableManager
{
    public static PlatformManager Instance; // variable that holds the instance for the singleton setup

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
        HUDManager.Instance.spawnableClasses.Add(this);
    }
}
