using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSoju : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public IEnumerator FadeEffect(float start, float end,float fadeTime) // 페이드 인,아웃 설정
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.transform.gameObject);
        }
    }
}
