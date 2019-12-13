using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesManager : MonoBehaviour
{
    public static SpikesManager Instance;

    private float spikesTimer;
    private float spikesGap;

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
        spikesTimer = 0f;
        spikesGap = 5f;
    }

    void Update()
    {
        spikesTimer += Time.deltaTime;

        if (spikesTimer >= spikesGap)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Spikes");
            obj.transform.position = new Vector3(16f, -3.5f, 0f);
            obj.SetActive(true);
            spikesTimer = 0f;
            spikesGap = Random.Range(1.5f, 3f);
        }
    }
}
