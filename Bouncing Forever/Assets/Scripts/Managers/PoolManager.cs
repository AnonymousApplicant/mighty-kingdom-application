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
        // Set pooledObjects to a new Dictionary
        pooledObjects = new Dictionary<string, List<GameObject>>();
        // For each item in the itemsToPool
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // Check if objectToPool is not null, if so try objectsToPool
            if (item.objectToPool != null)
            {
                // Loop over the amount of times to pool that item
                for (int i = 0; i < item.amountToPool; i++)
                {
                    // Set tempList to the List in the dictionary under the items poolKey
                    List<GameObject> tempList = GetTempList(item.poolKey);
                    // Instantiate the object
                    GameObject obj = Instantiate(item.objectToPool);
                    // Deactivate it
                    obj.SetActive(false);
                    // Add it to the tempList
                    tempList.Add(obj);
                    // Remove the old list, add the new list
                    pooledObjects.Remove(item.poolKey);
                    pooledObjects.Add(item.poolKey, tempList);
                }
            }
            else if (item.objectsToPool.Count != 0)
            {
                // Foreach item in objectsToPool
                foreach (GameObject go in item.objectsToPool)
                {
                    // Instantiate amountToPool per objectsToPool
                    for (int i = 0; i < item.amountToPool; i++)
                    {
                        // Set tempList to the List in the dictionary under the items poolKey
                        List<GameObject> tempList = GetTempList(item.poolKey);
                        // Instantiate the object
                        GameObject obj = Instantiate(go);
                        // Deactivate it
                        obj.SetActive(false);
                        // Add it to the tempList
                        tempList.Add(obj);
                        // Remove the old list, add the new list
                        pooledObjects.Remove(item.poolKey);
                        pooledObjects.Add(item.poolKey, tempList);
                    }
                }
            }
            // Else return to next item
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
        // Set templist to new list
        List<GameObject> tempList = new List<GameObject>();
        // Try get the list from the dictionary
        pooledObjects.TryGetValue(poolKey, out tempList);
        // If the temp list is null make it a new list
        if (tempList == null) tempList = new List<GameObject>();
        // return tempList
        return tempList;
    }

    /// <summary>
    /// Get a pooled object from the List<> in the Dictionary corrosponding to the poolKey
    /// </summary>
    /// <param name="poolKey"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(string poolKey)
    {
        // Set tempList to the list in the dictionary
        List<GameObject> tempList = GetTempList(poolKey);

        // For every item in the pool
        for (int i = 0; i < tempList.Count; i++)
        {
            // Check if it is not currently active
            if (!tempList[i].activeInHierarchy)
            {
                // if its not active return it
                return tempList[i];
            }
        }

        // If no active objects found go through each item in the itemsToPool
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // Check if the poolKey matches the items poolKey and the pool is expandable
            if (item.poolKey == poolKey && item.isExpandable)
            {
                // Instantiate new object
                GameObject obj = Instantiate(item.objectToPool);
                // Deactivate it
                obj.SetActive(false);
                // Add it to the tempList
                tempList.Add(obj);
                // Remove the old list, add the new list
                pooledObjects.Remove(item.poolKey);
                pooledObjects.Add(item.poolKey, tempList);
                // Return that newly made object
                return obj;
            }
        }

        // If no objects available and not expandable, return null
        return null;
    }

    /// <summary>
    /// Get a random pooled object from the List<> in the Dictionary corrosponding to the poolKey
    /// </summary>
    /// <param name="poolKey"></param>
    /// <returns></returns>
    public GameObject GetRandomPooledObject(string poolKey)
    {
        // Set tempList to the list in the dictionary
        List<GameObject> tempList = GetTempList(poolKey);
        // Set notActiveList to a new list
        List<GameObject> notActiveList = new List<GameObject>();

        // For every item in the pool
        for (int i = 0; i < tempList.Count; i++)
        {
            // Check if it is not currently active
            if (!tempList[i].activeInHierarchy)
            {
                // If its not active add it to the notActiveList
                notActiveList.Add(tempList[i]);
            }
        }

        // If the notActiveList is not 0
        if (notActiveList.Count != 0)
        {
            // Return a random object from the list
            return notActiveList[Mathf.RoundToInt(Random.Range(0, notActiveList.Count))];
        }

        // If no active objects found go through each item in the itemsToPool
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // Check if the poolKey matches the items poolKey and the pool is expandable
            if (item.poolKey == poolKey && item.isExpandable)
            {
                // Instantiate new object
                GameObject obj = Instantiate(item.objectsToPool[Mathf.RoundToInt(Random.Range(0, tempList.Count))]);
                // Deactivate it
                obj.SetActive(false);
                // Add it to the tempList
                tempList.Add(obj);
                // Remove the old list, add the new list
                pooledObjects.Remove(item.poolKey);
                pooledObjects.Add(item.poolKey, tempList);
                // Return that newly made object
                return obj;
            }
        }

        // Otherwise return null
        return null;
    }

    public void RemoveDebris()
    {
        string[] removeableObjects = new string[4]{"Coins", "Spikes", "SpikeBalls", "Platforms"};

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
