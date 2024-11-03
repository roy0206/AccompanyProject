using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onclick : MonoBehaviour
{
    public List<GameObject> panelList;
    public Text stationName;
    public Text fee;
    void Start()
    {
        kioskManager.Instance.previousPanel=0;
        ActivePanel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toFront(){
        ActivePanel(0);
        kioskManager.Instance.previousPanel=0;
    }
    public void tobefore(){
        ActivePanel(kioskManager.Instance.previousPanel-1);
    }
    public void stageClicked(){
        ActivePanel(1);
        kioskManager.Instance.previousPanel=1;
    }
    public void elderClicked(){
        ActivePanel(1);
        kioskManager.Instance.isElder=true;
        kioskManager.Instance.previousPanel=1;
    }
    public void seoul(){
        kioskManager.Instance.selectedStationIndex=0;
        kioskManager.Instance.previousPanel=2;
        ActivePanel(2);
        stationName.text = kioskManager.Instance.station[0];
    }
    public void gongdeok(){
        kioskManager.Instance.selectedStationIndex=1;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[1];
        ActivePanel(2);
    }
    public void hongdea(){
        kioskManager.Instance.selectedStationIndex=2;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[2];
        ActivePanel(2);
    }
    public void gimpo(){
        kioskManager.Instance.selectedStationIndex=3;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[3];
        ActivePanel(2);
    }
    public void incheon(){
        kioskManager.Instance.selectedStationIndex=4;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[4];
        ActivePanel(2);
    }
    public void ActivePanel(int i){
        for(int j=0;j<panelList.Count;j++){
            panelList[j].SetActive(false);
        }
        panelList[i].SetActive(true);
    }
}
