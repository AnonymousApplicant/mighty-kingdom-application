using UnityEngine;

public class CloudController : SceneryController
{
    // Set the parMultiplier in base class for parrelaxing effect
    void Start()
    {
        base.parMultiplier = 5;
    }
}