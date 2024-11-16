using UnityEngine;

public class TpInteraction : Interaction
{
    [SerializeField] Gateway destination;
    Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public override bool InteractionCondition()
    {
        return true;
    }

    public override void OnInteracting()
    {
        Camera.main.transform.position = destination.Scene.transform.position + new Vector3(0, 0, -1);
        player.transform.position = destination.transform.position;
        player.isEnableTp = false;
        Invoke("EnablePlayerTp", 2);
    }
}
