using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableController : MonoBehaviour
{
    public Rigidbody2D rb; // Variable that holds the Rigidbody2D component of the object it's attached to

    // If active, constantly move the rigidbodies position based on the difficulty, / parMultiplier for parrelaxing effect
    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty) * Time.deltaTime), transform.position.y, 0f);
        rb.MovePosition(position);

        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}
