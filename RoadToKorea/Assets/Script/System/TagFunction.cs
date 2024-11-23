using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TagFunction : MonoBehaviour
{
    [SerializeField] Transform card;
    [SerializeField] Gateway destination;
    [SerializeField] Image img;
    float tagTime;
    Vector2 pos;
    private void Start()
    {
        pos = card.transform.position;
        tagTime = 0;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            card.transform.position = (Vector3)pos + new Vector3(0, 0, 1);
            return;
        }

        Vector2 touchPos = Input.GetTouch(0).position;
        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(touchPos), 1, LayerMask.GetMask("Card"));
        if (collider)
            card.transform.position = Camera.main.ScreenToWorldPoint(touchPos) + new Vector3(0,0,1);
        else
            card.transform.position = (Vector3)pos + new Vector3(0, 0, 1);
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        print(tagTime);
        if (other)
        {
            tagTime += Time.deltaTime;

            if (tagTime > 1)
            {
                tagTime = -99999;
                StartCoroutine(OnTagged());
            }
        }
        else tagTime = 0;
        img.fillAmount = tagTime;
    }

    IEnumerator OnTagged()
    {
        PersistedCanvasManager.Instance.FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);

        Camera.main.transform.position = destination.Scene.transform.position + new Vector3(0, 0, -10);
        GameManager.Instance.player.transform.position = destination.transform.position + new Vector3(0,0,-5);
        PersistedCanvasManager.Instance.FadeIn(0.5f);
        enabled = false;
        GameManager.Instance.player.EnableMobileButtons();
        yield return new WaitForSeconds(0.5f);
        PersistedCanvasManager.Instance.FadeOut(0.5f);
    }


}
