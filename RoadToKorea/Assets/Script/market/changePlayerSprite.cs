using System;
using UnityEngine;

public class changePlayerSprite : MonoBehaviour
{
    public Sprite boyHead,girlHead;
    private SpriteRenderer playerSprite;
    Animator animator;
    void Start(){
        playerSprite = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(MarketManager.Instance.isSellectScene){
            animator.speed = 0.0f;
            playerSprite.sprite = girlHead;
        }
        else{
            animator.speed = 1.0f;
        }
    }
}
