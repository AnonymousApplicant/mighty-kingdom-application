using UnityEngine;

public class SpikesController : SpawnableController
{
    // Get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
    }

    // Check if the object collided with the player, if so then run the EndGame method
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SFXManager.Instance.playerPop.Play();
            HUDManager.Instance.EndGame();
        }
    }
}