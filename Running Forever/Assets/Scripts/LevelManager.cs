using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float difficulty;
    public float multiplier;
    private float timer;

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
        GameObject obj1 = PoolManager.Instance.GetPooledLevels();
        obj1.transform.position = new Vector3(-10.9f, -4.5f, 0f);
        obj1.SetActive(true);

        GameObject obj2 = PoolManager.Instance.GetPooledLevels();
        obj2.transform.position = new Vector3(0f, -4.5f, 0f);
        obj2.SetActive(true);

        GameObject obj3 = PoolManager.Instance.GetPooledLevels();
        obj3.transform.position = new Vector3(10.9f, -4.5f, 0f);
        obj3.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            difficulty = difficulty * multiplier;
            timer = 0f;
        }
    }

    public void AddToLevel()
    {
        GameObject obj = PoolManager.Instance.GetPooledLevels();
        obj.transform.position = new Vector3(15.2f, -4.5f, 0f);
        obj.SetActive(true);
    }
}
