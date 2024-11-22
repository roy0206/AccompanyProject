using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
   
    public List<GameObject> panel;
    public List<GameObject> buttons;

    public Dropdown Language;
    public enum Language_select
    {
        korean,
        english,
        japanese
    };
    public Language_select Language_state;

    public enum panel_state
    {
        phone_screen,
        setting_screen,
        note_screen,
        task_screen,
        map_screen,
        bag_screen,
        no_screen,
    };

    public panel_state screen_state;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen_state = panel_state.no_screen;
    }

    // Update is called once per frame
    void Update()
    {   //언어 설정
        switch (Language.value)
        {
            case 0 : 
                Language_state = Language_select.korean;
                break;
            case 1 :
                Language_state = Language_select.english;
                break;
            case 2 : 
                Language_state = Language_select.japanese;
                break;
        }
       Debug.Log(Language_state);
       //화면상태
       switch (screen_state)
       {
           case panel_state.phone_screen:
               buttons[0].SetActive(false);
               panel[0].SetActive(true);
               panel[1].SetActive(false);
               panel[2].SetActive(false);
               break;
           case panel_state.setting_screen:
               buttons[0].SetActive(false);
               panel[0].SetActive(false);
               panel[1].SetActive(true);
               panel[2].SetActive(false);
               break;
          case panel_state.no_screen:
               buttons[0].SetActive(true);
               panel[0].SetActive(false);
               panel[1].SetActive(false);
               panel[2].SetActive(false);
              break;
          case panel_state.note_screen:
               buttons[0].SetActive(false);
               panel[0].SetActive(false);
               panel[1].SetActive(false);
               panel[2].SetActive(true);
              break;
       }

    }

    public void OnPhoneScreen()
    {
        screen_state = panel_state.phone_screen;
    }
    public void OnClickSettingButtons()
    {
        screen_state = panel_state.setting_screen;
    }

    public void OnclickNoteButton()
    {
        screen_state = panel_state.note_screen;
    }
}
