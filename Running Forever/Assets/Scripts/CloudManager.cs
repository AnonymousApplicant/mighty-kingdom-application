using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance;

    private float cloudsTimer;
    private float cloudsGap;

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
        cloudsTimer = 0f;
        cloudsGap = 0f;
    }

    void Update()
    {
        cloudsTimer += Time.deltaTime;

        if (cloudsTimer >= cloudsGap)
        {
            GameObject obj = PoolManager.Instance.GetMixedPooledObjects("Clouds");
            obj.transform.position = new Vector3(16f, Random.Range(3f, 4f), 0f);
            obj.SetActive(true);
            cloudsTimer = 0f;
            cloudsGap = Random.Range(1.5f, 5f);
        }
    }
}