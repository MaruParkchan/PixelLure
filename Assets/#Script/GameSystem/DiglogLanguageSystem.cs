using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogLanguageSystem : MonoBehaviour
{

    public void LanguageEnglish()
    {
        LanguageChange(Language.English);
    }

    public void LanguageKorean()
    {
        LanguageChange(Language.Korean);
    }

    private void LanguageChange(Language language)
    {
        PlayerPrefs.SetInt("LanguageIndex", (int)language);;
    }
}
