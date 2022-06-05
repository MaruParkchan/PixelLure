using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : Boss
{
    [SerializeField]
    private GameObject smokeEffect;
    private SmokeMovePattern smokeMovePattern;
    private SmokeMiniDestructPattern smokeMiniDestructPattern;
    private SmokeAshtrayPattern smokeAshtrayPattern;
    private SmokeSprayingFirePattern smokeSprayingFirePattern;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        smokeMovePattern = GetComponent<SmokeMovePattern>();
        smokeMiniDestructPattern = GetComponent<SmokeMiniDestructPattern>();
        smokeAshtrayPattern = GetComponent<SmokeAshtrayPattern>();
        smokeSprayingFirePattern = GetComponent<SmokeSprayingFirePattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        patternRandomValue = new int[4]; // 선택지 선택후에 랜덤 패턴을 위한 값 크기는 패턴의 수 만큼 조정해야함
    }

    public void HideorAppear() // 숨거나 나타날때 이펙트 생성
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position + new Vector3(-0.11f,0,0);
    }

    protected override IEnumerator Phase1()
    {
        yield return new WaitForSeconds(3.0f);
        HideorAppear();
        animator.SetTrigger("Hide");
        while (true)
        {
            yield return StartCoroutine(smokeMovePattern.MovePattern());
            yield return StartCoroutine(smokeMiniDestructPattern.SpawnSmokeMini());
            yield return StartCoroutine(smokeAshtrayPattern.DrapPattern());
            yield return StartCoroutine(smokeSprayingFirePattern.SprayingFire());
        }
    }

    protected override IEnumerator Phase2()
    {
        HideorAppear();
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(3.0f);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true)
        {
            RandomPatternValue();
            int patternIndex = 0;
            while (patternIndex < 4)
            {
                if (patternRandomValue[patternIndex] == 0)
                    yield return StartCoroutine(smokeMovePattern.MovePattern());
                if (patternRandomValue[patternIndex] == 1)
                    yield return StartCoroutine(smokeMiniDestructPattern.SpawnSmokeMini());
                if (patternRandomValue[patternIndex] == 2)
                    yield return StartCoroutine(smokeAshtrayPattern.DrapPattern());
                if (patternRandomValue[patternIndex] == 3)
                    yield return StartCoroutine(smokeSprayingFirePattern.SprayingFire());

                patternIndex++;
            }
        }
    }

    protected override void SelectionEventTime()
    {     
        smokeMovePattern.CoroutineStop();
        smokeMiniDestructPattern.CoroutineStop();
        smokeAshtrayPattern.CoroutineStop();
        smokeSprayingFirePattern.CoroutineStop();
        this.transform.position = Vector3.zero;
    }

    protected override void ColliderEnableOn()
    {
        boxCollider2D.enabled = true;
    }

    protected override void ColliderEnableOff()
    {
        boxCollider2D.enabled = false;
    }
}
