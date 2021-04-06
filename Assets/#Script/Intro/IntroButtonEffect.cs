using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroButtonEffect : MonoBehaviour
{
    // Ŭ�� ������ ��¦ �Ÿ��� ȿ���� ���� ��ũ��Ʈ 

    [SerializeField]
    private float fadeTime = 1.0f;
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
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

            Color color = render.color;
            color.a = Mathf.Lerp(start, end, percent);
            render.color = color;

            yield return null;
        }
    }
}
