using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] GameObject settingWindow;
    public bool IsSettingEnabled => settingWindow.activeSelf;
    public void OnClick()
    {
        if(IsSettingEnabled)
            settingWindow.SetActive(false);
        else settingWindow.SetActive(true);
    }
}
