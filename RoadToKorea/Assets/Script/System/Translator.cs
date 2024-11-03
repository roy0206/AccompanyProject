using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Language { Korean, English}

public class Translator : Singleton<Translator>
{
    List<Dictionary<string, object>> translotionData;
    List<Translation> changeDetector;

    void Awake()
    {
        base.Awake();
        if (translotionData == null) translotionData = CSVReader.Read("CSV/TranslationData");
        changeDetector = FindAllTranslations();
    }

    public string GetTranslation(int num)
    {
        return translotionData[num - 2][GameManager.Instance.Settings.Language.ToString()].ToString();
    }

    public void OnLanguageChanged()
    {
        foreach (var cd in changeDetector)
        {
            cd.ChangeLanguage();
        }
    }

    private List<Translation> FindAllTranslations()
    {
        IEnumerable<Translation> savables = FindObjectsOfType<MonoBehaviour>().OfType<Translation>();
        return new List<Translation>(savables);
    }
}