using UnityEngine;

public class RoadLineController : SceneryController
{
    // Set the parMultiplier in base class for parrelaxing effect
    void Start()
    {
        base.parMultiplier = 1;
    }
}