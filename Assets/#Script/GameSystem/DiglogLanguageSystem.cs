using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiglogLanguageSystem : MonoBehaviour
{
    [SerializeField] private Image englishButtonImage;
    [SerializeField] private Image koreanButtonImage;

    private void Start()
    {
        ChangeImageAlpha();
    }

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
        PlayerPrefs.SetInt("LanguageIndex", (int)language); ;

        ChangeImageAlpha();
    }

    public void ChangeImageAlpha()
    {
        if (PlayerPrefs.GetInt("LanguageIndex") == 0)
        {
            ChangeAlphaValue(englishButtonImage, Color.yellow, 255f);
            ChangeAlphaValue(koreanButtonImage, Color.white, 50f);
        }

        else if (PlayerPrefs.GetInt("LanguageIndex") == 1)
        {
            ChangeAlphaValue(englishButtonImage, Color.white, 50f);
            ChangeAlphaValue(koreanButtonImage, Color.yellow, 255f);
        }
    }

    public void ChangeAlphaValue(Image buttonImage, Color color, float alphaValue)
    {
        Color colorAlpha = englishButtonImage.color;
        colorAlpha.a = alphaValue / 255f;
        buttonImage.color = new Color(color.r, color.g, color.b, colorAlpha.a);
    }
}
