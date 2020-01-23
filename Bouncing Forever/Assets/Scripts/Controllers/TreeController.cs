using UnityEngine;

public class TreeController : SceneryController
{
    // Set the parMultiplier in base class for parrelaxing effect, get the Rigidbody2D and assign it to the Rigidbody2D variable
    void Start()
    {
        base.parMultiplier = 2;
    }
}