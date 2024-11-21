using UnityEngine;

public class MarketUIScript : MonoBehaviour
{
    public GameObject frontPanel, ContorlCanvas;
    public void OnFrontPicClicked(){
        frontPanel.SetActive(false);
        ContorlCanvas.SetActive(true);
        MarketManager.Instance.OnFrontPanelClicked.Invoke();
    }
    void Awake(){
        frontPanel.SetActive(true);
        ContorlCanvas.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
