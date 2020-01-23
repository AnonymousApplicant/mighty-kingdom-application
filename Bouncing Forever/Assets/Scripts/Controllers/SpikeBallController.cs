using UnityEngine;

public class SpikeBallController : SpawnableController
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
            // Play SFX, Deactivate player object and execute EndGame()
            SFXManager.Instance.playerPop.Play();
            PlayerController.Instance.gameObject.SetActive(false);
            HUDManager.Instance.EndGame();
        }
    }
}