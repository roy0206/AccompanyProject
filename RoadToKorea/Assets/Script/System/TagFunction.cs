using System.Collections;
using UnityEngine;

public class TagFunction : MonoBehaviour
{
    [SerializeField] Transform card;
    [SerializeField] Gateway destination;
    float tagTime;

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), 1, LayerMask.GetMask("CardTag"));
        if (collider)
        {
            tagTime += Time.deltaTime;
            if(tagTime > 1)
            {
                tagTime = -99999;
                StartCoroutine(OnTagged());
            }
        }
        else
        {
            tagTime = 0;
        }
    }

    IEnumerator OnTagged()
    {
        PersistedCanvasManager.Instance.FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);

        Camera.main.transform.position = destination.Scene.transform.position + new Vector3(0, 0, -1);
        GameManager.Instance.player.transform.position = destination.transform.position;
        PersistedCanvasManager.Instance.FadeIn(0.5f);
        enabled = false;
    }


}
