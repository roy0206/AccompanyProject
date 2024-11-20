using UnityEngine;

public class OpenAirportKiosk : Interaction
{
    [SerializeField] Gateway destination;
    Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public override bool InteractionCondition()
    {
        return (bool)GameManager.Instance.stageData["Stage1"].GetParameter("IsTicketBought") == false;
    }

    public override void OnInteracting()
    {
        player.DisableMobileButtons();
        //키오스크로 이동
    }
}
