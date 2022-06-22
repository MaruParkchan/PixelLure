using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuCircle : MonoBehaviour
{
    private DogSmallSoju[] dogSmallSojus;

    private float rotateSpeed; // 회전 속도
    private float rotateTime; // 패턴 회전 시간 
    private float fadeTime; // 페이드 걸리는 시간

    private BoxCollider2D[] boxColliders2D = new BoxCollider2D[2];

    void Start()
    {
        boxColliders2D = GetComponents<BoxCollider2D>();
        dogSmallSojus = GetComponentsInChildren<DogSmallSoju>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360.0f));
        StartCoroutine("FadeEffectCycle");
        StartCoroutine("RotateRadnom");
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

        for (int i = 0; i < boxColliders2D.Length; i++)
            boxColliders2D[i].enabled = true;

        yield return new WaitForSeconds(rotateTime); // 소주병 돌아가는 시간 < 끝나면 페이드 아웃

        for (int i = 0; i < dogSmallSojus.Length; i++)
        {
            StartCoroutine(dogSmallSojus[i].FadeEffect(1, 0, fadeTime));
        }  // 페이드 아웃

        yield return new WaitForSeconds(fadeTime); // 돌아가는 시간 끝나면 삭제

        Destroy(this.gameObject);
    }

    private IEnumerator RotateRadnom()
    {
        while (true)
        {
            float timer = Random.Range(1.0f, 3.0f);
            yield return new WaitForSeconds(timer);
            rotateSpeed *= -1;
        }
    }

    public void Init(float rotateSpeed, float rotateTime, float fadeTime)
    {
        this.rotateSpeed = rotateSpeed;
        this.rotateTime = rotateTime;
        this.fadeTime = fadeTime;

        int randomIndex = Random.Range(0, 2); // 0 = false , 1 = true

        if (randomIndex == 0) // 회전 방향 조건문
        {
            rotateSpeed *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.transform.gameObject);
        }
    }

}
