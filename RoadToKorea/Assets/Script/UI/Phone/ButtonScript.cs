using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Language_select
{
    korean,
    english,
    japanese
};
public class ButtonScript : MonoBehaviour
{
    public Slider Slider_Sound;
    public List<GameObject> panel;
    public List<GameObject> buttons;
    public Dropdown Language;
    public string Contentxstring_one = "";
    public Text memo;
    public float Sound = 1f;
    public Text SoundText;
 
    public Language_select Language_state;

    private int Sound_hundred;
    public enum panel_state
    {
        phone_screen,
        setting_screen,
        note_screen,
        no_screen,
    };
    public panel_state screen_state;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Slider_Sound.value = Sound;
        screen_state = panel_state.no_screen;
        //Language_state = GameManager.Instance.Settings.Language;
    }

    // Update is called once per frame
    void Update()
    {   //언어 설정
        switch (Language.value)
        {
            case 0 :
               //GameManager.Instance.Settings.Language = LanguageState.Korean;
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
               panel[3].SetActive(false);
               break;
          case panel_state.no_screen:
               buttons[0].SetActive(true);
               panel[0].SetActive(false);
               panel[1].SetActive(false);
               panel[2].SetActive(false);
               panel[3].SetActive(true);
              break;
          case panel_state.note_screen:
               buttons[0].SetActive(false);
               panel[0].SetActive(false);
               panel[1].SetActive(false);
               panel[2].SetActive(true);
               panel[3].SetActive(false);
               break;               
           case panel_state.setting_screen:
               buttons[0].SetActive(false);
               panel[0].SetActive(false);
               panel[1].SetActive(true);
               panel[2].SetActive(false);
               OnChangedSoundValue();
               panel[3].SetActive(false);
               break;
       }

    }

    public void OnChangedSoundValue()
    {
        Sound = Slider_Sound.value;
        Sound_hundred = (int)(Sound * 100);
        SoundText.text = Sound_hundred + "%";
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

    public void OnPutText()
    {
        memo.text = Contentxstring_one;
    }

    public void OnReturnButton()
    {
        screen_state = panel_state.phone_screen;
    }

    public void OnNoScreen()
    {
        screen_state = panel_state.no_screen;
    }
        
}
