using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public float coinScore;

    public float startingGap;
    public Vector2 gapRange;

    private float coinTimer;
    private float coinGap;

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
        coinTimer = 0f;
        coinGap = startingGap;
    }

    void Update()
    {
        coinTimer += Time.deltaTime;

        if (coinTimer >= coinGap)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Coins");
            obj.transform.position = new Vector3(16f, Random.Range(2f, -1f), 0f);
            obj.SetActive(true);
            coinTimer = 0f;
            coinGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}
