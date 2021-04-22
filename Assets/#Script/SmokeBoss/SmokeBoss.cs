using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SmokeTest());
    }

    IEnumerator SmokeTest()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            HideorAppear();
            animator.SetTrigger("Hide");
            yield return new WaitForSeconds(2.0f);
            HideorAppear();
            animator.SetTrigger("Appear");
        }
    }

    private void HideorAppear() // 숨거나 나타날때 이펙트 생성
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position;
    }


}
