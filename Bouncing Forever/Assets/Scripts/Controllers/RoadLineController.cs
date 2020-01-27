using UnityEngine;

public class RoadLineController : SceneryController
{
    [SerializeField]
    [Tooltip("The amount to divide the speed by to create parrelaxing effect")]
    private int parrelaxDivider;

    // Set the parMultiplier in base class for parrelaxing effect
    void Start()
    {
        base.parDivider = parrelaxDivider;
    }
}