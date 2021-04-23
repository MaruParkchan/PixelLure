using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect;
    private SmokeMovePattern smokeMovePattern;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        smokeMovePattern = GetComponent<SmokeMovePattern>();
        StartCoroutine("SmokeBossPattern");
    }

    private IEnumerator SmokeBossPattern()
    {
        yield return new WaitForSeconds(4.0f);
        HideorAppear();
        animator.SetTrigger("Hide");

        while(true)
        {
            yield return StartCoroutine(smokeMovePattern.MovePattern());
        }
    }

    public void HideorAppear() // ���ų� ��Ÿ���� ����Ʈ ����
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position;
    }
}
