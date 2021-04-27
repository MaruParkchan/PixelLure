using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect;
    private SmokeMovePattern smokeMovePattern;
    private SmokeMiniDestructPattern smokeMiniDestructPattern;
    private SmokeAshtrayPattern smokeAshtrayPattern;

    private Animator animator;

    [SerializeField]
    private float settting;
    private void Start()
    {
        animator = GetComponent<Animator>();
        smokeMovePattern = GetComponent<SmokeMovePattern>();
        smokeMiniDestructPattern = GetComponent<SmokeMiniDestructPattern>();
        smokeAshtrayPattern = GetComponent<SmokeAshtrayPattern>();
        StartCoroutine("SmokeBossPattern");
    }

    private IEnumerator SmokeBossPattern()
    {
        yield return new WaitForSeconds(4.0f);
        HideorAppear();
        animator.SetTrigger("Hide");

        while (true)
        {
            yield return StartCoroutine(smokeMovePattern.MovePattern());
            // yield return StartCoroutine(smokeMiniDestructPattern.SpawnSmokeMini());
            //yield return StartCoroutine(smokeAshtrayPattern.DrapPattern());
        }

            //yield return new WaitForSeconds(2.0f);
            //HideorAppear();
            //yield return new WaitForSeconds(settting);
            //animator.SetTrigger("Appear");
            //yield return new WaitForSeconds(2.0f);       
    }

    public void HideorAppear() // 숨거나 나타날때 이펙트 생성
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position + new Vector3(-0.11f,0,0);
    }
}
