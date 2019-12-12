using UnityEngine;

public class LevelController : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x - (LevelManager.Instance.difficulty * Time.deltaTime), -4.5f, 0f);

        if (transform.position.x < -15.2f)
        {
            LevelManager.Instance.AddToLevel();
            gameObject.SetActive(false);
        }
    }
}
