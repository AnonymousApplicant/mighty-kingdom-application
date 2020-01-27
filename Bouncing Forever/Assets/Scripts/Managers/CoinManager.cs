using UnityEngine;

public class CoinManager : SpawnableManager
{
    public static CoinManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The score recieved when collecting a coin")]
    public float coinScore;

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

    /// <summary>
    /// Spawn 3 coins equally seperated at the same height as a platform, called by PlatformManager
    /// </summary>
    public void SpawnPlatformCoins()
    {
        // For three coins
        for (int i = 0; i < 3; i++)
        {
            // Get a new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject("Coins");
            // Place its X position at -12.5f + (4.5f * i) so each new object is 4.5f further than the other
            obj.transform.position = new Vector3(12.5f + (4.5f * i), 3.5f, 0f);
            // Activate object
            obj.SetActive(true);
        }
    }
}
