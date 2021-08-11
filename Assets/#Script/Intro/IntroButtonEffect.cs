using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroButtonEffect : MonoBehaviour
{
    // Ŭ�� ������ ��¦ �Ÿ��� ȿ���� ���� ��ũ��Ʈ 

    [SerializeField]
    private float fadeTime = 1.0f;
    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void StartTwinkle()
    {
        StartCoroutine("TwinkleLoop");
    }

    private IEnumerator TwinkleLoop()
    {
        yield return StartCoroutine(FadeEffect(1, 0)); // Alpha 1 -> 0 ���� FadeOut
        yield return StartCoroutine(FadeEffect(0, 1)); // Alpha 0 -> 1 ���� FadeIn
        yield return StartCoroutine(FadeEffect(1, 0));
        yield return StartCoroutine(FadeEffect(0, 1));
        yield return StartCoroutine(FadeEffect(1, 0));
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = buttonImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            buttonImage.color = color;

            yield return null;
        }
    }
}
