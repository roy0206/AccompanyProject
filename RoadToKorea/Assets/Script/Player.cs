using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isEnableTp = true;

    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed;

    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(joystick.Horizontal != 0  || joystick.Vertical != 0)
        {
            transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = joystick.Horizontal > 0;
            animator.SetBool("IsWalking", true);
        }
        else animator.SetBool("IsWalking", false);

    }
}
