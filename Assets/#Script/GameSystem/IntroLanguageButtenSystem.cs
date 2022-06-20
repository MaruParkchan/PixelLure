using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroLanguageButtenSystem : MonoBehaviour
{
    [SerializeField] private Image englishButtonImage;
    [SerializeField] private Image koreanButtonImage;

    private void Start()
    {
        ChangeImageAlpha();
    }

    public void LanguageEnglish()
    {
        ChangeImageAlpha();
    }

    public void LanguageKorean()
    {
        ChangeImageAlpha();
    }

    public void ChangeAlphaValue(Image buttonImage, float alphaValue)
    {
        Color colorAlpha = englishButtonImage.color;
        colorAlpha.a = alphaValue / 255f;
        buttonImage.color = colorAlpha;
    }

    public void ChangeImageAlpha()
    {
        if (PlayerPrefs.GetInt("LanguageIndex") == 0)
        {
            ChangeAlphaValue(englishButtonImage, 255f);
            ChangeAlphaValue(koreanButtonImage, 50f);
        }

        else if (PlayerPrefs.GetInt("LanguageIndex") == 1)
        {
            ChangeAlphaValue(englishButtonImage, 50f);
            ChangeAlphaValue(koreanButtonImage, 255f);
        }
    }
}
