using Unity.VisualScripting;
using UnityEngine;

public class Gateway : MonoBehaviour
{
    public Transform Scene => scene;
    [SerializeField] Transform scene;
    [SerializeField] Gateway destination;
    Player player;

    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = GameManager.Instance.player;
        if (collision.gameObject.tag == "Player" && player && player.isEnableTp && destination)
        {
            Camera.main.transform.position = destination.GetComponent<Gateway>().scene.transform.position + new Vector3(0,0,-1);
            player.transform.position = destination.transform.position;
            player.isEnableTp = false;
            Invoke("EnablePlayerTp", 2);
        }
    }

    private void EnablePlayerTp()
    {
        player.isEnableTp = true;
    }
}
