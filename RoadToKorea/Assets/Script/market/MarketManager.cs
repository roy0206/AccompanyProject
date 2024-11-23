using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketManager : Singleton<MarketManager>
{
    public GameObject player;
    public bool isSellectScene = false;

    public Action OnFrontPanelClicked;
    private SpriteRenderer playerRenderer;
    private Animator animator;
    void Start()
    {
        playerRenderer = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        animator.SetBool("IsIdle",false);
        animator.SetBool("isWaking",false);
        animator.SetBool("isSellect",true);
        playerRenderer.enabled = false;
        OnFrontPanelClicked+=playerActive;
    }
    void Update()
    {
        if(player.transform.position.x != 0 && SceneManager.GetActiveScene().name=="market"){
            isSellectScene=false;
        }
    }
    private void playerActive(){
        isSellectScene = true;
        playerRenderer.enabled = true;
    }
}
