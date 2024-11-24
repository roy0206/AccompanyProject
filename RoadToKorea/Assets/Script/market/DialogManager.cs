using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : Singleton<DialogManager>
{
    public GameObject DialogPanel;
    public GameObject ControlPanel;
    public GameObject A_panel,Q_panel,Suggest_panel;
    public Text A_text,Q_text;
    public List<GameObject> panelList = new List<GameObject>();
    public List<String> contentList = new List<string>();
    public int storedInt = 0;
    public int price;
    public void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1) && Suggest_panel.activeSelf)
        {
            storedInt = 1;
            price = 4000 + storedInt * 500;

            // 새로운 가격 메시지를 생성
            string message1 = $"{price}원은 어떠세요?";
            string message2 = $"{price}원이요? 안됩니다";

            // contentList의 인덱스 1과 2에 메시지를 삽입
            if (contentList.Count >= 3) 
            {
                contentList[1] = message1;  // 1번 인덱스의 값을 수정
                contentList[2] = message2;  // 2번 인덱스의 값을 수정
            }
            else
            {
                // 인덱스가 부족하면 Insert로 삽입
                while (contentList.Count < 3) 
                {
                    contentList.Add("");  // 최소 3개 항목이 되도록 빈 항목 추가
                }
                contentList[1] = message1;
                contentList[2] = message2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&& Suggest_panel.active==true)
        {
            storedInt = 2;
            price = 4000 + storedInt * 500;

            // 새로운 가격 메시지를 생성
            string message1 = $"{price}원은 어떠세요?";
            string message2 = $"{price}원이요? 안됩니다";

            // contentList의 인덱스 1과 2에 메시지를 삽입
            if (contentList.Count >= 3) 
            {
                contentList[1] = message1;  // 1번 인덱스의 값을 수정
                contentList[2] = message2;  // 2번 인덱스의 값을 수정
            }
            else
            {
                // 인덱스가 부족하면 Insert로 삽입
                while (contentList.Count < 3) 
                {
                    contentList.Add("");  // 최소 3개 항목이 되도록 빈 항목 추가
                }
                contentList[1] = message1;
                contentList[2] = message2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && Suggest_panel.active==true)
        {
            storedInt = 3;
            price = 4000 + storedInt * 500;

            // 새로운 가격 메시지를 생성
            string message1 = $"{price}원은 어떠세요?";
            string message2 = $"{price}원이요? 안됩니다";

            // contentList의 인덱스 1과 2에 메시지를 삽입
            if (contentList.Count >= 3) 
            {
                contentList[1] = message1;  // 1번 인덱스의 값을 수정
                contentList[2] = message2;  // 2번 인덱스의 값을 수정
            }
            else
            {
                // 인덱스가 부족하면 Insert로 삽입
                while (contentList.Count < 3) 
                {
                    contentList.Add("");  // 최소 3개 항목이 되도록 빈 항목 추가
                }
                contentList[1] = message1;
                contentList[2] = message2;
            }
        }
    }
    public void onNextDialogClicked(){
        if (panelList.Count == 3 || contentList.Count == 3)//패널 비활성화 안됨
        {
            if (Q_panel.activeSelf)
            {
                Q_panel.SetActive(false);
            }
            if (A_panel.activeSelf)
            {
                A_panel.SetActive(false);
            }
            Suggest_panel.SetActive(true);
        }
        else{
            AddDialog(panelList[1],contentList[1]);
            panelList.RemoveAt(0);
            contentList.RemoveAt(0);
        }
    }
    public void OnSuggestClicked(){
        Suggest_panel.SetActive(false);
        AddDialog(panelList[3],contentList[3]);
    }
    public void DialogTrigger(){
        ControlPanel.SetActive(false);
        DialogPanel.SetActive(true);
        panelList[0].SetActive(true);
        if(panelList[0]==A_panel){
            A_text.text = contentList[0];
        }
        else{
            Q_text.text = contentList[0];
        }
    }
    public void AddDialog(GameObject panel,String contents){
        if(panel == A_panel){
            DialogPanel.SetActive(true);
            A_text.text = contents;
            Q_text.text = "";
            Q_panel.SetActive(false);
            A_panel.SetActive(true);
        }
        else{
            DialogPanel.SetActive(true);
            Q_text.text = contents;
            A_text.text = "";
            Q_panel.SetActive(true);
            A_panel.SetActive(false);
        }
    }
    public void EndDialog(){
        A_text.text = "";
        Q_text.text = "";
        Q_panel.SetActive(false);
        A_panel.SetActive(false);
        DialogPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }
}
