using UnityEngine;
using System.Collections.Generic;

public class SpawnableManager : MonoBehaviour
{
    [Tooltip("The starting time to wait for the first object to be introduced")]
    public float startingGap;
    [Tooltip("The range of time between gaps after the first one")]
    public Vector2 gapRange;

    public bool isScenery;

    public string poolKey;

    public float spawnStartPos;
    [Tooltip("The random range to spawn between for the Y value (set X to desired value and Y to 0 if no range needed)")]
    public Vector2 spawnY;
    public bool isRandomized;

    [HideInInspector]
    public float currentTimer; // Timer that keeps track of the time since last spawn
    [HideInInspector]
    public float currentGap; // The variable that keeps track of the current gap

    public virtual void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        // Set timer to 0f and set gap to the startingGap
        currentTimer = 0f;
        currentGap = startingGap;
    }

    public void InitializePool(int amountToSpawn, string poolKey, float startPos, float gapLength, Vector2 yVariation, bool isRandom)
    {
        // For loop for start of the game object spawns
        for (int i = 0; i < amountToSpawn; i++)
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
            // Place its X position at -15f + (4.5f * i) so each new object is 4.5f further than the other
            obj.transform.position = new Vector3(startPos + (gapLength * i), yPos, 0f);
            // Acivate the object
            obj.SetActive(true);
        }
    }

    public void SpawnObject(string poolKey, float startPos, Vector2 yVariation, bool isRandom)
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
        // Set new objects position
        obj.transform.position = new Vector3(startPos, yPos, 0f);
        // Activate object
        obj.SetActive(true);
    }

    void Update()
    {
        if (HUDManager.Instance.isPlaying == true)
        {
            // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
            currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

            // Check if the timer is equal to or more than the gap time
            if (currentTimer >= currentGap)
            {
                SpawnObject(poolKey, spawnStartPos, spawnY, isRandomized);
                if (poolKey == "Platforms")
                {
                    // Execute SpawnPlatformCoins() method so 3 coins spawn above the platform at the same time
                    CoinManager.Instance.SpawnPlatformCoins();
                }
                // Reset timer and pick new random gap time
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
        else if (isScenery == true)
        {
            // Update the timer based on the (difficulty / startingDifficulty) so at the beginning its 1f
            currentTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty);

            // Check if the timer is equal to or more than the gap time
            if (currentTimer >= currentGap)
            {
                SpawnObject(poolKey, spawnStartPos, spawnY, isRandomized);
                // Reset timer and pick new random gap time
                currentTimer = 0f;
                currentGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
