using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuCircle : MonoBehaviour
{
    private DogSmallSoju[] dogSmallSojus;

    private float rotateSpeed; // ȸ�� �ӵ�
    private float rotateTime; // ���� ȸ�� �ð� 
    private float waitTime; // ���� ���ð�
    private float fadeTime; // ���̵� �ɸ��� �ð�

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
        } // ���̵� ��
        yield return new WaitForSeconds(rotateTime);
        for (int i = 0; i < dogSmallSojus.Length; i++)
        {
            StartCoroutine(dogSmallSojus[i].FadeEffect(1, 0, fadeTime));
        }  // ���̵� �ƿ�
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
