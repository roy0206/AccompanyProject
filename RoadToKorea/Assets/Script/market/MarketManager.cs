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
        animator.SetBool("IsWalking",false);
        playerRenderer.enabled = false;
        OnFrontPanelClicked+=playerActive;
    }
    void Update()
    {
        if(player.transform.position.x != 0 && SceneManager.GetActiveScene().name=="market"){
            player.GetComponent<Player>().AlterView(ViewType.SideView);
        }
    }
    private void playerActive(){
        player.GetComponent<Player>().AlterView(ViewType.TopView);
        playerRenderer.enabled = true;
    }
}
