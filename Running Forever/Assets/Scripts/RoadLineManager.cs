using UnityEngine;

public class RoadLineManager : MonoBehaviour
{
    public static RoadLineManager Instance;

    private float roadLineTimer;
    private float roadLineGap;

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
        roadLineTimer = 0f;
        roadLineGap = 0f;
    }

    void Update()
    {
        roadLineTimer += Time.deltaTime;

        if (roadLineTimer >= roadLineGap)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("RoadLines");
            obj.transform.position = new Vector3(16f, -4.5f, 0f);
            obj.SetActive(true);
            roadLineTimer = 0f;
            roadLineGap = 0.5f;
        }
    }
}