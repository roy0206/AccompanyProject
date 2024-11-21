using System;
using UnityEngine;

public class MarketManager : Singleton<MarketManager>
{
    public GameObject player;
    public bool isSellectScene = false;

    public Action OnFrontPanelClicked;

    private SpriteRenderer playerRenderer;
    void Start()
    {
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerRenderer.enabled = false;
        OnFrontPanelClicked+=playerActive;
    }
    void Update()
    {
        
    }
    private void playerActive(){
        isSellectScene = true;
        playerRenderer.enabled = true;
    }
}
