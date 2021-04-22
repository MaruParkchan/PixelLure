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

    private void HideorAppear() // ���ų� ��Ÿ���� ����Ʈ ����
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position;
    }


}
