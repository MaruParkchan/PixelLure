using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuCircle : MonoBehaviour
{
    private DogSmallSoju[] dogSmallSojus;

    private float rotateSpeed; // 회전 속도
    private float rotateTime; // 패턴 회전 시간 
    private float waitTime; // 패턴 대기시간
    private float fadeTime; // 페이드 걸리는 시간

    void Start()
    {
        dogSmallSojus = GetComponentsInChildren<DogSmallSoju>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360.0f));
        StartCoroutine("FadeEffectCycle");
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private IEnumerator FadeEffectCycle()
    {
        for (int i = 0; i < dogSmallSojus.Length; i++)
        {
            StartCoroutine(dogSmallSojus[i].FadeEffect(0, 1, fadeTime));
        } // 페이드 인
        yield return new WaitForSeconds(rotateTime);
        for (int i = 0; i < dogSmallSojus.Length; i++)
        {
            StartCoroutine(dogSmallSojus[i].FadeEffect(1, 0, fadeTime));
        }  // 페이드 아웃
        yield return new WaitForSeconds(fadeTime);
        Destroy(this.gameObject);
    }

    public void Init(float rotateSpeed, float rotateTime, float fadeTime)
    {
        this.rotateSpeed = rotateSpeed;
        this.rotateTime = rotateTime;
        this.fadeTime = fadeTime;
    }

}
