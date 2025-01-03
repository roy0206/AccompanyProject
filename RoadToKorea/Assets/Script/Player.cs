using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum ViewType { TopView, SideView }

public class Player : MonoBehaviour
{
    public bool isEnableTp = true;

    [SerializeField] Joystick joystick;

    public float moveSpeed;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Sprite sp;
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

    public void ChangeInteractionButtonTexture(Sprite texture)
    {
        interactionButton.GetComponent<Image>().sprite = texture;
    }

    public void EnableMobileButtons()
    {
        joystick.gameObject.SetActive(true);
        interactionButton.gameObject.SetActive(true);
    }
    public void DisableMobileButtons()
    {
        joystick.gameObject.SetActive(false);
        interactionButton.gameObject.SetActive(false);
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        interactionButton.onClick.AddListener(OnInteractionButtonClicked);

        sp = interactionButton.image.sprite;

        AlterView(ViewType.SideView);
    }


    public void AlterView(ViewType t)
    {
        if(t == ViewType.TopView)
        {
            animator.SetBool("IsSideView", false);
            animator.SetBool("IsTopView", true);
        }
        else if(t == ViewType.SideView)
        {
            animator.SetBool("IsSideView", true);
            animator.SetBool("IsTopView", false);
        }
    }
    void Update()
    {
        if (!GameManager.Instance.IsControllable) return;
        if (!IsActivated)
        {
            interactionButton.image.sprite = sp;
        }
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            spriteRenderer.flipX = joystick.Horizontal > 0;
            if (SceneManager.GetActiveScene().name == "market" && animator.GetBool("IsTopView")){
                transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * moveSpeed * Time.deltaTime;
            }
            else{
                transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * moveSpeed * Time.deltaTime;
                animator.SetBool("IsWalking", true);
            }
        }
        else animator.SetBool("IsWalking", false);
    }

    public void OnInteractionButtonClicked()
    {
        if (!GameManager.Instance.IsControllable) return;
        if (!IsActivated) return;
        onInteracting.Invoke();
        onInteracting = null;
    }
    
}
