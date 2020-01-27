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
    public static PoolManager Instance; // variable that holds the instance for the singleton setup

    [SerializeField]
    [Tooltip("Set size to the amount of different objects being pooled.")]
    private List<ObjectPoolItem> itemsToPool;
    [HideInInspector]
    public Dictionary<string, List<GameObject>> pooledObjects;

    void Awake()
    {
        // Run singleton check/setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Check and initialize the Object Pool system
    void Start()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();

        // Foreach object in the itemsToPool variable, set up a seperate pool with the amount specified
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // Check if objects to pool is not empty
            if (item.objectToPool != null)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    // Get the current list of items in the pool, add this object to it, replace old list
                    List<GameObject> tempList = GetTempList(item.poolKey);
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    tempList.Add(obj);
                    pooledObjects.Remove(item.poolKey);
                    pooledObjects.Add(item.poolKey, tempList);
                }
            }
            // Else if, check objectS to pool is not 0 (for objects with variations)
            else if (item.objectsToPool.Count != 0)
            {
                // Foreach object in the objectsToPool variable, add amount specified of this variation
                foreach (GameObject go in item.objectsToPool)
                {
                    for (int i = 0; i < item.amountToPool; i++)
                    {
                        // Get the current list of items in the pool, add this object to it, replace old list
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

    /// <summary>
    /// Get the List<> inside the pooling dictionary that corrosponds to the given poolKey
    /// </summary>
    /// <param name="poolKey"></param>
    /// <returns></returns>
    List<GameObject> GetTempList(string poolKey)
    {
        List<GameObject> tempList = new List<GameObject>();
        pooledObjects.TryGetValue(poolKey, out tempList);
        if (tempList == null) tempList = new List<GameObject>();
        return tempList;
    }

    /// <summary>
    /// Get a pooled object from the List<> in the Dictionary corrosponding to the poolKey
    /// </summary>
    /// <param name="poolKey"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(string poolKey)
    {
        // Chekc through for any unactive objects
        List<GameObject> tempList = GetTempList(poolKey);
        for (int i = 0; i < tempList.Count; i++)
        {
            if (!tempList[i].activeInHierarchy)
            {
                return tempList[i];
            }
        }

        // If no unactive objects found, find the corrosponding pool and check if it can expand, if so expand it by instantiating 1 new object
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

        // If no objects available and not expandable, return null
        return null;
    }

    /// <summary>
    /// Get a random pooled object from the List<> in the Dictionary corrosponding to the poolKey (Used for pools with variation)
    /// </summary>
    /// <param name="poolKey"></param>
    /// <returns></returns>
    public GameObject GetRandomPooledObject(string poolKey)
    {
        List<GameObject> tempList = GetTempList(poolKey);
        List<GameObject> notActiveList = new List<GameObject>();

        // For every item in the pool
        for (int i = 0; i < tempList.Count; i++)
        {
            if (!tempList[i].activeInHierarchy)
            {
                notActiveList.Add(tempList[i]);
            }
        }

        // If the notActiveList is not 0, return a random object from the list
        if (notActiveList.Count != 0)
        {
            return notActiveList[Mathf.RoundToInt(Random.Range(0, notActiveList.Count))];
        }

        // If no active objects found, find the corrosponding pool and check if it is expandable, if so expand it by 1 random variation
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.poolKey == poolKey && item.isExpandable)
            {
                GameObject obj = Instantiate(item.objectsToPool[Mathf.RoundToInt(Random.Range(0, tempList.Count))]);
                obj.SetActive(false);
                tempList.Add(obj);
                pooledObjects.Remove(item.poolKey);
                pooledObjects.Add(item.poolKey, tempList);
                return obj;
            }
        }

        // Otherwise return null
        return null;
    }

    /// <summary>
    /// Removes any objects currently spawned in (besides scenery)
    /// </summary>
    public void RemoveDebris()
    {
        string[] removeableObjects = new string[4]{"Coins", "Spikes", "SpikeBalls", "Platforms"};

        // Go through each pool and check if any have any active objects, if so deactivat them
        foreach (string objectName in removeableObjects)
        {
            List<GameObject> tempList = GetTempList(objectName);
            foreach (GameObject item in tempList)
            {
                if (item.activeInHierarchy)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
