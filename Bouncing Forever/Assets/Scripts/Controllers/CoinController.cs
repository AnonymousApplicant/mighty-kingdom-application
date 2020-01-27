using UnityEngine;

public class CoinController : SpawnableController
{
    // Get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
    }

    // If the player touches a coin, play collection sound, add to coin score and put coin back into pool
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SFXManager.Instance.coinCollect.Play();
            ScoreManager.Instance.coinScore += CoinManager.Instance.coinScore;
            gameObject.SetActive(false);
        }
    }
}