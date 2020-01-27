using UnityEngine;

public class SceneryController : MonoBehaviour
{
    [HideInInspector]
    public int parDivider;

    // If active, constantly move the objects position based on the difficulty, / parMultiplier for parrelaxing effect
    void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x - ((DifficultyManager.Instance.difficulty / parDivider) * Time.fixedDeltaTime), transform.position.y, 0f);
        transform.position = position;

        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
        }
    }
}
