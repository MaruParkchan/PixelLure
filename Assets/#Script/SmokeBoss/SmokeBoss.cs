using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect;
    private SmokeMovePattern smokeMovePattern;

    private Animator animator;

    [SerializeField]
    private float settting;
    private void Start()
    {
        animator = GetComponent<Animator>();
        smokeMovePattern = GetComponent<SmokeMovePattern>();
        StartCoroutine("SmokeBossPattern");
    }

    private IEnumerator SmokeBossPattern()
    {
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            HideorAppear();
            animator.SetTrigger("Hide");

            //while (true)
            //{
            //    yield return StartCoroutine(smokeMovePattern.MovePattern());
            //}

            yield return new WaitForSeconds(2.0f);
            HideorAppear();
            yield return new WaitForSeconds(settting);
            animator.SetTrigger("Appear");
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void HideorAppear() // ���ų� ��Ÿ���� ����Ʈ ����
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position + new Vector3(-0.11f,0,0);
    }
}
