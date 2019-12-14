using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    public float startingGap;
    public Vector2 gapRange;

    private float platformTimer;
    private float platformGap;

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
        platformTimer = 0f;
        platformGap = startingGap;
    }

    void Update()
    {
        if (HUDManager.Instance.isPlaying == true)
        {
            platformTimer += Time.deltaTime;

            if (platformTimer >= platformGap)
            {
                GameObject obj = PoolManager.Instance.GetPooledObject("Platforms");
                obj.transform.position = new Vector3(17f, 2.5f, 0f);
                obj.SetActive(true);

                CoinManager.Instance.SpawnPlatformCoins();

                platformTimer = 0f;
                platformGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
