using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance;

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
        treeGap = 0f;
    }

    void Update()
    {
        treeTimer += Time.deltaTime;

        if (treeTimer >= treeGap)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Trees");
            obj.transform.position = new Vector3(16f, 0f, 0f);
            obj.SetActive(true);
            treeTimer = 0f;
            treeGap = 5f;
        }
    }
}