public class CloudController : SceneryController
{
    public int parrelaxDivider;

    // Set the parMultiplier in base class for parrelaxing effect
    void Start()
    {
        base.parDivider = parrelaxDivider;
    }
}