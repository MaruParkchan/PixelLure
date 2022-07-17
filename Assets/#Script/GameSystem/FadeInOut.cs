using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    [SerializeField] private bool fadeIn;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        if (fadeIn)
            StartCoroutine(FadeEffect(1, 0));
        else
            StartCoroutine(FadeEffect(0, 1));
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

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
