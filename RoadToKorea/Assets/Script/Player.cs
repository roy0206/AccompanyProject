using System;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public bool isEnableTp = true;

    [SerializeField] Joystick joystick;

    [SerializeField] float moveSpeed;

    SpriteRenderer spriteRenderer;
    Animator animator;


    //Interaction
    public bool IsActivated
    {
        get => onInteracting != null;
    }
    public Action OnInteracting
    {
        get => onInteracting;
        set { onInteracting = value; interactionButton.GetComponent<CanvasGroup>().alpha = (IsActivated) ? 1 : 0.5f; }
    }
    private Action onInteracting;
    [SerializeField] Button interactionButton;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        interactionButton.onClick.AddListener(OnInteractionButtonClicked);
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

    public void OnInteractionButtonClicked()
    {
        if (!IsActivated) return;
        onInteracting.Invoke();
        onInteracting = null;
    }
}