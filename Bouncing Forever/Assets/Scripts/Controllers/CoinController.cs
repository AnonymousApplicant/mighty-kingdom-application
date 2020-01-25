using UnityEngine;

public class CoinController : SpawnableController
{
    // Get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with the player
        if (other.tag == "Player")
        {
            // Play SFX, increase ScoreManager's coinScore by the coinScore set in CoinManager
            SFXManager.Instance.coinCollect.Play();
            ScoreManager.Instance.coinScore += CoinManager.Instance.coinScore;
            // Deactivate the object putting it back into the pool
            gameObject.SetActive(false);
        }
    }
}