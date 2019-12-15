using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; // variable that holds the instance for the singleton setup

    public float coinScore; // The score recieved when collecting a coin

    public float startingGap; // The starting time to wait for the first object to be introduced
    public Vector2 gapRange; // The range of time between gaps after the first one

    private float coinTimer; // Timer that keeps track of the time since last spawn
    private float coinGap; // The variable that keeps track of the current gap

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
        coinTimer = 0f;
        coinGap = startingGap;
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying == true)
        {
            // Increase timer
            coinTimer += Time.deltaTime;

            // Check if the timer is equal to or more than the gap time
            if (coinTimer >= coinGap)
            {
                // Get new pooled object
                GameObject obj = PoolManager.Instance.GetPooledObject("Coins");
                // Set new objects position
                obj.transform.position = new Vector3(16f, Random.Range(2f, -1f), 0f);
                // Activate object
                obj.SetActive(true);
                // Reset timer and pick new random gap time
                coinTimer = 0f;
                coinGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
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
