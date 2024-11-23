using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Slider Slider_Sound;
    public List<GameObject> panel;
    public List<GameObject> buttons;
    public float Sound = 1f;
    public Text SoundText;
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
        achieve_screen,
        map_screen,
        inventory_screen,
        no_screen,
    };

    private int Sound_hundred;
    public panel_state screen_state;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen_state = panel_state.no_screen;
        Slider_Sound.value = Sound;
    }

    // Update is called once per frame
    void Update()
    {   //언어 설정
        switch (Language.value)
        {
            case 0 :
               // GameManager.Instance.Settings.Language = LanguageState.Korean;
                break;
            case 1 :
                //GameManager.Instance.Settings.Language = LanguageState.English;
                break;
            case 2 : 
                //GameManager.Instance.Settings.Language = LanguageState.Japanese;
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
               OnChangedSoundValue();
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
          case panel_state.achieve_screen:
              break;
          case panel_state.inventory_screen:
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
 
    public void OnClickMapButtons()
    {
        screen_state = panel_state.map_screen;
    }

    public void OnclickInventoryButton()
    {
        screen_state = panel_state.inventory_screen;
    }
    public void OnclickAchieveButton()
    {
        screen_state = panel_state.achieve_screen;
    }

    public void OnChangedSoundValue()
    {
        Sound = Slider_Sound.value;
        Sound_hundred = (int)(Sound * 100);
        SoundText.text = Sound_hundred + "%";
    }

    public void OnReturnButton()
    {
        screen_state = panel_state.phone_screen;
    }

  
}
