using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PersistedCanvasManager : Singleton<PersistedCanvasManager>
{
    Canvas canvas;
    Image darkBackground;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        darkBackground = transform.Find("DarkBackground").GetComponent<Image>();
    }
    private void Update()
    {
        if(canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }


    public void FadeIn(float fadeInTime) => StartCoroutine(ControllDarkBackground(0, 1, fadeInTime));
    public void FadeOut(float fadeInTime) => StartCoroutine(ControllDarkBackground(1, 0, fadeInTime));

    IEnumerator ControllDarkBackground(float from, float to, float time)
    {
        var b = from < to;
        if (b) darkBackground.gameObject.SetActive(true);

        float curTime = 0;
        while(true)
        {
            yield return null;
            curTime += Time.deltaTime;
            darkBackground.color = new Color(0, 0, 0, Mathf.Lerp(from, to, curTime / time));
            if (curTime > time) break;
        }

        if(!b) darkBackground.gameObject.SetActive(false);
    }
    
}
