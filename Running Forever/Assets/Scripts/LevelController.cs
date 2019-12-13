using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - (LevelManager.Instance.difficulty * Time.deltaTime), 0.5f, 0f);
        rb.MovePosition(position);
    }

    void FixedUpdate()
    {
        if (transform.position.x < -((LevelManager.Instance.amountActive / 2) * 10))
        {
            float offset = transform.position.x + ((LevelManager.Instance.amountActive / 2) * 10);
            LevelManager.Instance.AddToLevel(offset);
            gameObject.SetActive(false);
        }
    }
}
