using UnityEngine;

public class CheckCardInteraction : CamMoveInteraction
{
    public override bool InteractionCondition()
    {
        return (bool) GameManager.Instance.stageData["Stage1"].GetParameter("IsTicketBought");
    }

    public override void OnInteracting()
    {
        base.OnInteracting();
    }
}
