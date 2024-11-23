using UnityEngine;

public class TpInteraction : Interaction
{
    [SerializeField] Gateway destination;
    Player player;
    private void Start()
    {
        
    }
    public override bool InteractionCondition()
    {
        return true;
    }

    public override void OnInteracting()
    {
        player = GameManager.Instance.player;
        Camera.main.transform.position = destination.Scene.transform.position + new Vector3(0, 0, -1);
        player.transform.position = destination.transform.position;
        player.isEnableTp = false;
        player.Invoke("EnablePlayerTp", 2);
    }
}
