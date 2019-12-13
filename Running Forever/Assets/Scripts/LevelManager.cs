using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Tooltip("The starting difficulty.")]
    public float difficulty;
    [SerializeField]
    [Tooltip("The amount to multiply the diffiiculty by per second.")]
    private float multiplier;
    private float timer;

    [Tooltip("The amount of level prefabs to be active at 1 time. (Even Number)")]
    public int amountActive;
    private List<GameObject> activeObjects;

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
        int startingPoint = -((amountActive / 2) * 10);

        for (int i = 0; i < amountActive; i++)
        {
            GameObject obj = PoolManager.Instance.GetPooledLevels();
            obj.transform.position = new Vector3(startingPoint + (i * 10f), 0.5f, 0f);
            obj.SetActive(true);
        }
    }

    void Update()
    {
        if (difficulty < 15f) timer += Time.deltaTime;

        if (timer >= 1f)
        {
            difficulty = difficulty * multiplier;
            timer = 0f;
        }
    }

    public void AddToLevel(float offset)
    {
        GameObject obj = PoolManager.Instance.GetPooledLevels();
        obj.transform.position = new Vector3(((amountActive / 2) * 10) + offset, 0.5f, 0f);
        obj.SetActive(true);
    }
}
