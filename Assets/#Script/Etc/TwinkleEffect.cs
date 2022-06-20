using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinkleEffect : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void StartTwinkle()
    {
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine("TwinkleLoop");
        }
    }

    private IEnumerator TwinkleLoop()
    {
        yield return StartCoroutine(FadeEffect(1, 0)); // Alpha 1 -> 0 으로 FadeOut
        yield return StartCoroutine(FadeEffect(0, 1)); // Alpha 0 -> 1 으로 FadeIn
        yield return StartCoroutine(FadeEffect(1, 0));
        yield return StartCoroutine(FadeEffect(0, 1));
        yield return StartCoroutine(FadeEffect(1, 0));
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        float fadeTime = 0.3f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }
}
