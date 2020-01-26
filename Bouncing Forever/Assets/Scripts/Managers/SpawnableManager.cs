using UnityEngine;
using System.Collections.Generic;

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

    public void InitializePool(int amountToSpawn, string poolKey, float startPos, float gapLength, Vector2 yVariation)
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

            // Get a new pooled object
            GameObject obj = PoolManager.Instance.GetPooledObject(poolKey);
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
}
