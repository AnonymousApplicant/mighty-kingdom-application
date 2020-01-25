using UnityEngine;

public class PlatformController : SpawnableController
{
    // Get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
    }
}