using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CloudController : MonoBehaviour
{
    private Rigidbody2D rb; // Variable that holds the Rigidbody2D component of the object it's attached to

    // Get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // If active, constantly move the rigidbodies position based on the difficulty, / 5 for parrelaxing effect
    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty / 5) * Time.deltaTime), transform.position.y, 0f);
        rb.MovePosition(position);
    }

    // Check if the objects position is off screen, if so deactivate the object putting it back into the pool
    void FixedUpdate()
    {
        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}