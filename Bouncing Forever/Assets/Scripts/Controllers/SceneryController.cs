using UnityEngine;

public class SceneryController : MonoBehaviour
{
    [HideInInspector]
    public int parDivider;

    // If active, constantly move the rigidbodies position based on the difficulty, / parMultiplier for parrelaxing effect
    void Update()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty / parDivider) * Time.deltaTime), transform.position.y, 0f);
        transform.position = position;

        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}
