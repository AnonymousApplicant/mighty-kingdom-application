using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance;

    public float startingGap;
    public Vector2 gapRange;

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
        cloudsGap = startingGap;

        for (int i = 0; i < 8; i++)
        {
            GameObject obj = PoolManager.Instance.GetMixedPooledObjects("Clouds");
            obj.transform.position = new Vector3(-16f + (4f * i), Random.Range(1.4f, 4f), 0f);
            obj.SetActive(true);
        }
    }

    void Update()
    {
        cloudsTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / 9);

        if (cloudsTimer >= cloudsGap)
        {
            GameObject obj = PoolManager.Instance.GetMixedPooledObjects("Clouds");
            obj.transform.position = new Vector3(16f, Random.Range(1.4f, 4f), 0f);
            obj.SetActive(true);
            cloudsTimer = 0f;
            cloudsGap = Random.Range(gapRange.x, gapRange.y);
        }
    }
}