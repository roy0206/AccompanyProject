using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class onclick : MonoBehaviour
{
    public List<GameObject> panelList;
    public Text stationName;
    public Text fee;
    public Text finalFee;
    public Text Adult;
    public Text Elder;
    public Text Baby;
    public int A_count=0;
    public int B_count=0;
    [SerializeField] Gateway toScene;
    [SerializeField] Gateway fromScene;
    void Start()
    {
        kioskManager.Instance.previousPanel=0;
        ActivePanel(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        feeCount();
        if(kioskManager.Instance.previousPanel!=2 && kioskManager.Instance.previousPanel!=3){
            A_count=0;
            B_count=0;
        }
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
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(2);
/*        stationName.text = kioskManager.Instance.station[0];*/
    }
    public void gongdeok(){
        kioskManager.Instance.selectedStationIndex=1;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[1];
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(2);
    }
    public void hongdea(){
        kioskManager.Instance.selectedStationIndex=2;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[2];
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(2);
    }
    public void gimpo(){
        kioskManager.Instance.selectedStationIndex=3;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[3];
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(2);
    }
    public void incheon(){
        kioskManager.Instance.selectedStationIndex=4;
        kioskManager.Instance.previousPanel=2;
        stationName.text = kioskManager.Instance.station[4];
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(2);
    }
    public void A_zero(){
        A_count=0;
    }
    public void A_one(){
        A_count=1;
    }
    public void A_two(){
        A_count=2;
    }
    public void A_tre(){
        A_count=3;
    }
    public void A_fore(){
        A_count=4;
    }
    public void A_five(){
        A_count=5;
    }
    public void B_zero(){
        B_count=0;
    }public void B_one(){
        B_count=1;
    }
    public void B_two(){
        B_count=2;
    }public void B_tre(){
        B_count=3;
    }public void B_fore(){
        B_count=4;
    }public void B_five(){
        B_count=5;
    }
    public void pur(){
        if(kioskManager.Instance.isElder){
            ActivePanel(4);
        }
        else ActivePanel(3);
    }
    public void feeCount(){
        Adult.text=A_count.ToString();
        Baby.text=B_count.ToString();
        if(kioskManager.Instance.isElder){
            Elder.text=1.ToString();
            fee.text = 0.ToString();
            finalFee.text = 0.ToString();
        }
        else{
            fee.text=(A_count*5000+B_count*2400).ToString();
            finalFee.text=(A_count*5000+B_count*2400).ToString();
        }
    }
    public void ActivePanel(int i){
        for(int j=0;j<panelList.Count;j++){
            panelList[j].SetActive(false);
        }
        panelList[i].SetActive(true);
    }

    public void CloseUi()
    {
        Camera.main.transform.position = fromScene.Scene.transform.position + new Vector3(0, 0, -1);
        GameManager.Instance.player.EnableMobileButtons();
        GameManager.Instance.stageData["Stage1"].AlterParameter("IsTicketBought", true);
    }

    public void OpenUi()
    {
        Camera.main.transform.position = toScene.Scene.transform.position + new Vector3(0, 0, -1);
    }
}
