using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFadeOut : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private float waitTime;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine("StartFadeOut");
    }

    private IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(FadeEffect(1, 0));
        this.gameObject.SetActive(false);

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
