using UnityEngine;

public class CloudController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty / 5) * Time.deltaTime), transform.position.y, 0f);
        rb.MovePosition(position);
    }

    void FixedUpdate()
    {
        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}