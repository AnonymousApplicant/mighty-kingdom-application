using System;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty) * Time.deltaTime), transform.position.y, 0f);
        rb.MovePosition(position);
    }

    void FixedUpdate()
    {
        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SFXManager.Instance.playerPop.Play();
            PlayerController.Instance.gameObject.SetActive(false);
        }
    }
}