using Unity.VisualScripting;
using UnityEngine;

public class Gateway : MonoBehaviour
{
    [SerializeField] Transform scene;
    [SerializeField] Gateway destination;
    Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.isEnableTp)
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
