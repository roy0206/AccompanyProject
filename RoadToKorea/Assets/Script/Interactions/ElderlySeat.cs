using System.Collections;
using UnityEngine;

public class ElderlySeat : Interaction
{
    [SerializeField] protected Transform destination;
    [SerializeField] protected bool disableMobileUi;

    [SerializeField] Transform p0, p1, p2, p3;
    [SerializeField] Transform npc;

    bool isInteracted;
    public override bool InteractionCondition()
    {
        return !isInteracted;
    }

    public override void OnInteracting()
    {
        isInteracted = true;
        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        GameManager.Instance.IsControllable = false;
        npc.GetComponent<SpriteRenderer>().flipX = false;
        npc.position = p0.position;
        Vector2 vec;
        var player = GameManager.Instance.player;
        do
        {
            vec = p1.position - player.transform.position;
            player.transform.position += (Vector3)vec * player.moveSpeed * Time.deltaTime;
            yield return null;
        }
        while (vec.magnitude > 0.1f);

        player.AlterView(ViewType.TopView);
        yield return new WaitForSeconds(2f);

        do
        {
            vec = p2.position - npc.transform.position;
            npc.transform.position += (Vector3)vec * 2 * Time.deltaTime;
            yield return null;
        }
        while (vec.magnitude > 0.1f);

        yield return new WaitForSeconds(1f);

        Transform chat = npc.Find("Chat");
        Vector2 pos = chat.position;
        chat.gameObject.SetActive(true);
        for(int i = 0; i < 20; i++)
        {
            chat.transform.position = pos + UnityEngine.Random.insideUnitCircle * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        chat.position = pos;
        yield return new WaitForSeconds(1f);
        chat.gameObject.SetActive(false);


        do
        {
            vec = p3.position - npc.transform.position;
            npc.transform.position += (Vector3)vec * 0.5f * Time.deltaTime;
            yield return null;
        }
        while (vec.magnitude > 0.1f);
        yield return new WaitForSeconds(1f);
        player.AlterView(ViewType.SideView);
        GameManager.Instance.IsControllable = true;
    }
}
