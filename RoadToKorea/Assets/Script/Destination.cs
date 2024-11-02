using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject idle, selected;
    public Stage stage;
    public void Awake()
    {
        idle = transform.Find("Idle").gameObject;
        selected = transform.Find("Selected").gameObject;
    }
}
