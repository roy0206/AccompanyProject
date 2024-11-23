using UnityEngine;

public class CamMoveInteraction : Interaction
{
    [SerializeField] Transform destination;
    public override bool InteractionCondition()
    {
        return true;
    }

    public override void OnInteracting()
    {
        Camera.main.transform.position = destination.position + new Vector3(0,0, -1);
    }
}
