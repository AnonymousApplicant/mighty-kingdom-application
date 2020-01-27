using UnityEngine;
using System.Collections.Generic;

public class SpawnableManager : MonoBehaviour
{
    [Header("Mid-Game Spawn Setings")]
    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;
    public float spawnStartPos;
    [Tooltip("The random range to spawn between for the Y value (set X to desired value and Y to 0 if no range needed)")]
    public Vector2 spawnY;

    [Header("Object Information")]
    public bool isRandomized;
    public bool isScenery;
    public string poolKey;

    [HideInInspector]
    public float currentTimer; // Timer that keeps track of the time since last spawn
    [HideInInspector]
    public float currentGap; // The variable that keeps track of the current gap

    // Run the TimerStart method
    public virtual void Start()
    {
        TimerStart();
    }

    /// <summary>
    /// Re/Starts the timer for checking the gap
    /// </summary>
    public void TimerStart()
    {
        // Set timer to 0f and set gap to the startingGap
        currentTimer = 0f;
        currentGap = startingGap;
    }

    /// <summary>
    /// Spawns an object (if possible) from a given pool
    /// </summary>
    /// <param name="amountToSpawn">The amount to spawn at the start</param>
    /// <param name="poolKey">The key to the desired pool to acces</param>
    /// <param name="startPos">The position to start the object at</param>
    /// <param name="gapLength">The length to seperate each next object, if spawning less than 2, set this to 0</param>
    /// <param name="yVariation">The variation in the Y value, if no variation wanted then set the Y value of the Vector2 to 0</param>
    /// <param name="isRandom">Is the object being spawned part of a pool with varying objects</param>
    public void SpawnObjects(int amountToSpawn, string poolKey, float startPos, float gapLength, Vector2 yVariation, bool isRandom)
    {
        // Repeat based on amountToSpawn
        for (int i = 0; i < amountToSpawn; i++)
        {
            // Get Spawn data
            float yPos = GetYPosition(yVariation);
            GameObject obj = GetObject(isRandom);
            
            // Position and spawn object
            if (gapLength == 0)
            {
                obj.transform.position = new Vector3(startPos, yPos, 0f);
            }
            else
            {
                obj.transform.position = new Vector3(startPos + (gapLength * i), yPos, 0f);
            }
            obj.SetActive(true);
        }
    }

    // Check and spawn new objects when needed using the gap variables
    void Update()
    {
        // Check if game is playing, if so increase timer, if timer is greater than the gap length then spawn a new object and reset timer and pick a new gap
        if (HUDManager.Instance.isPlaying == true)
        {
            currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

            if (currentTimer >= currentGap)
            {
                SpawnObjects(1, poolKey, spawnStartPos, 0, spawnY, isRandomized);

                // If the object being spawned is a platform, run the SpawnPlatformCoins method
                if (poolKey == "Platforms")
                {
                    CoinManager.Instance.SpawnPlatformCoins();
                }

                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
        // Else if the object is scenery then keep spawning them
        else if (isScenery == true)
        {
            currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

            if (currentTimer >= currentGap)
            {
                SpawnObjects(1, poolKey, spawnStartPos, 0, spawnY, isRandomized);
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }

    /// <summary>
    /// Get the float value of the result of the yVariation variable
    /// </summary>
    /// <param name="yVariation">The yVariation defined in the inspector</param>
    /// <returns></returns>
    float GetYPosition(Vector2 yVariation)
    {
        float yPos = 0;
        if (yVariation.y == 0f)
        {
            yPos = yVariation.x;
        }
        else
        {
            yPos = Random.Range(yVariation.x, yVariation.y);
        }
        return yPos;
    }

    /// <summary>
    /// Get the GameObject of the result of the poolKey variable
    /// </summary>
    /// <param name="isRandom">The poolKey defined in the inspector</param>
    /// <returns></returns>
    GameObject GetObject(bool isRandom)
    {
        GameObject obj = null;
        if (isRandom == true)
        {
            // Get new pooled object
            obj = PoolManager.Instance.GetRandomPooledObject(poolKey);
        }
        else
        {
            // Get a new pooled object
            obj = PoolManager.Instance.GetPooledObject(poolKey);
        }
        return obj;
    }
}
