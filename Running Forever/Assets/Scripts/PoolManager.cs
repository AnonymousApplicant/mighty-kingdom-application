using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    [Tooltip("The key (name) for the pool, used when trying to 'GetPooledObjects()'")]
    public string poolKey;
    [Tooltip("The object that will be instansiated and pooled, leave empty if your using 'Objects To Pool'.")]
    public GameObject objectToPool;
    [Tooltip("The objects that will be instansiated and pooled per 'Amount To Pool', leave empty if your using 'Object To Pool'")]
    public List<GameObject> objectsToPool;
    [Tooltip("The amount of that object to pool (or) the amount per objects to pool at the start")]
    public int amountToPool;
    [Tooltip("Whether or not this object can expand it's pool if no pooled objects are available.")]
    public bool isExpandable;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]
    [Tooltip("Set size to the amount of different objects being pooled.")]
    private List<ObjectPoolItem> itemsToPool;
    [HideInInspector]
    public Dictionary<string, List<GameObject>> pooledObjects;

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

    void OnEnable()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool != null)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    List<GameObject> tempList = GetTempList(item.poolKey);
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    tempList.Add(obj);
                    pooledObjects.Remove(item.poolKey);
                    pooledObjects.Add(item.poolKey, tempList);
                }
            }
            else if (item.objectsToPool.Count != 0)
            {
                foreach (GameObject go in item.objectsToPool)
                {
                    for (int i = 0; i < item.amountToPool; i++)
                    {
                        List<GameObject> tempList = GetTempList(item.poolKey);
                        GameObject obj = Instantiate(go);
                        obj.SetActive(false);
                        tempList.Add(obj);
                        pooledObjects.Remove(item.poolKey);
                        pooledObjects.Add(item.poolKey, tempList);
                    }
                }
            }
            else return;
        }
    }

    List<GameObject> GetTempList(string poolKey)
    {
        List<GameObject> tempList = new List<GameObject>();
        pooledObjects.TryGetValue(poolKey, out tempList);
        if (tempList == null) tempList = new List<GameObject>();
        return tempList;
    }

    public GameObject GetPooledObject(string poolKey)
    {
        List<GameObject> tempList = GetTempList(poolKey);

        for (int i = 0; i < tempList.Count; i++)
        {
            if (!tempList[i].activeInHierarchy)
            {
                return tempList[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.poolKey == poolKey && item.isExpandable)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                tempList.Add(obj);
                pooledObjects.Remove(item.poolKey);
                pooledObjects.Add(item.poolKey, tempList);
                return obj;
            }
        }

        return null;
    }

    public GameObject GetPooledLevels()
    {
        List<GameObject> tempList = GetTempList("Levels");
        List<GameObject> activeList = new List<GameObject>();

        for (int i = 0; i < tempList.Count; i++)
        {
            if (!tempList[i].activeInHierarchy)
            {
                activeList.Add(tempList[i]);
            }
        }

        if (activeList.Count != 0)
        {
            return activeList[Mathf.RoundToInt(Random.Range(0, activeList.Count))];
        }

        return null;
    }
}
