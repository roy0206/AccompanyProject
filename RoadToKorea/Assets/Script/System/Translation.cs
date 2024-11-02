using UnityEngine;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
    public int TextId;
    Text text;

    private void Awake() => text = GetComponent<Text>();

    void Start() => ChangeText(TextId);
    

    public void ChangeText(int num)
    {
        text.text = Translator.Instance.GetTranslation(num);
        TextId = num;
    }
    public void ChangeLanguage()
    {
        text.text = Translator.Instance.GetTranslation(TextId);
    }
}
