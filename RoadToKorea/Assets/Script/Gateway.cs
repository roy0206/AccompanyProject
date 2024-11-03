using UnityEngine;

public class Gateway : MonoBehaviour
{
    [SerializeField] Transform scene;
    [SerializeField] Gateway destination;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Camera.main.transform.position = scene.transform.position + new Vector3(0,0,-1);
            collision.transform.position = destination.transform.position;
        }
    }
}
