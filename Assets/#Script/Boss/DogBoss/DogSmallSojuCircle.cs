using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuCircle : MonoBehaviour
{
    private DogSmallSoju[] dogSmallSojus;

    private float rotateSpeed; // ȸ�� �ӵ�
    private float rotateTime; // ���� ȸ�� �ð� 
    private float fadeTime; // ���̵� �ɸ��� �ð�

    void Start()
    {
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
        } // ���̵� ��

        yield return new WaitForSeconds(rotateTime); // ���ֺ� ���ư��� �ð� < ������ ���̵� �ƿ�

        for (int i = 0; i < dogSmallSojus.Length; i++)
        {
            StartCoroutine(dogSmallSojus[i].FadeEffect(1, 0, fadeTime));
        }  // ���̵� �ƿ�

        yield return new WaitForSeconds(fadeTime); // ���ư��� �ð� ������ ����

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

        if (randomIndex == 0) // ȸ�� ���� ���ǹ�
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