using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public float difficulty;
    [SerializeField]
    private float multiplier;

    private float timer;

    void Awake()
    {
        // Check if the Instance variable is not null and not 'this'
        if (Instance != null && Instance != this)
        {
            // Destroy gameObject connected to this script if Instance is already defined
            Destroy(this.gameObject);
        }
        else
        {
            // Assign 'this' to the Instance variable if Instance is null
            Instance = this;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            if (difficulty < 20f) difficulty = difficulty * multiplier;
            timer = 0f;
        }
    }
}
