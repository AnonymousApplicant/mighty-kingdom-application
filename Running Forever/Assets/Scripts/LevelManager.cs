using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {

    }
}
