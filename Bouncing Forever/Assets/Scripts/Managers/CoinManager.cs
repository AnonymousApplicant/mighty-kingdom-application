using UnityEngine;

public class CoinManager : SpawnableManager
{
    public static CoinManager Instance; // variable that holds the instance for the singleton setup

    [Tooltip("The score recieved when collecting a coin")]
    public float coinScore;

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

    /// <summary>
    /// Spawn 3 coins equally seperated at the same height as a platform
    /// </summary>
    public void SpawnPlatformCoins()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Coins");
            obj.transform.position = new Vector3(12.5f + (4.5f * i), 3.5f, 0f);
            obj.SetActive(true);
        }
    }
}
