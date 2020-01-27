using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb; // Variable that holds the Rigidbody2D component of the object it's attached to

    // If active, constantly move the rigidbodies position based on the difficulty
    void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty) * Time.fixedDeltaTime), transform.position.y, 0f);
        rb.MovePosition(position);

        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}
