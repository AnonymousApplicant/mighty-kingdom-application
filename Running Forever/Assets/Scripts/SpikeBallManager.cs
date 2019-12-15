using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallManager : MonoBehaviour
{
    public static SpikeBallManager Instance;

    public float startingGap;
    public Vector2 gapRange;

    private float spikeBallTimer;
    private float spikeBallGap;

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
        spikeBallTimer = 0f;
        spikeBallGap = startingGap;
    }

    void Update()
    {
        if (HUDManager.Instance.isPlaying == true)
        {
            spikeBallTimer += Time.deltaTime;

            if (spikeBallTimer >= spikeBallGap)
            {
                GameObject obj = PoolManager.Instance.GetPooledObject("SpikeBalls");
                obj.transform.position = new Vector3(16f, Random.Range(2f, -1f), 0f);
                obj.SetActive(true);
                spikeBallTimer = 0f;
                spikeBallGap = Random.Range(gapRange.x, gapRange.y);
            }
        }
    }
}
