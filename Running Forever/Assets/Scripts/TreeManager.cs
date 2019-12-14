using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance;

    public float startingGap;
    public float gapTime;

    private float treeTimer;
    private float treeGap;

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
        treeTimer = 0f;
        treeGap = startingGap;

        for (int i = 0; i < 2; i++)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Trees");
            obj.transform.position = new Vector3(-8f + (12f * i), 0f, 0f);
            obj.SetActive(true);
        }
    }

    void Update()
    {
        treeTimer += Time.deltaTime * (DifficultyManager.Instance.difficulty / 9);

        if (treeTimer >= treeGap)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Trees");
            obj.transform.position = new Vector3(16f, 0f, 0f);
            obj.SetActive(true);
            treeTimer = 0f;
            treeGap = gapTime;
        }
    }
}