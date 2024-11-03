using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed;
    void Update()
    {
        transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * moveSpeed * Time.deltaTime;
    }
}
