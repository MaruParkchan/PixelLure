using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBoss : BossHp, ICoroutineStop, IPause
{
    [SerializeField]
    private GameObject smokeEffect;
    private SmokeMovePattern smokeMovePattern;
    private SmokeMiniDestructPattern smokeMiniDestructPattern;
    private SmokeAshtrayPattern smokeAshtrayPattern;
    private SmokeSprayingFirePattern smokeSprayingFirePattern;

    private Animator animator;

    [SerializeField]
    private GameSystem gameSystem;

    [SerializeField]
    private int limitBossHp; // 일정피가 된다면 선택지 등장할 hp 수치값
    private bool isChoice = false;
    private bool isHit = false;
    private bool isDie = false;
    private bool isInvincibility; // 무적인가?
    private BoxCollider2D boxCollider2D;
    private int[] patternRandomValue = new int[4]; // 선택지 선택후에 랜덤 패턴을 위한 값
                                                   // 크기는 패턴의 수 만큼 조정해야함

    private void Awake()
    {
        animator = GetComponent<Animator>();
        smokeMovePattern = GetComponent<SmokeMovePattern>();
        smokeMiniDestructPattern = GetComponent<SmokeMiniDestructPattern>();
        smokeAshtrayPattern = GetComponent<SmokeAshtrayPattern>();
        smokeSprayingFirePattern = GetComponent<SmokeSprayingFirePattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentHp = GetFirstHp();
        StartCoroutine("SmokeBossPattern");
    }

    private IEnumerator SmokeBossPattern() // 첫번째 패턴 < 선택지 전 >
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

    private IEnumerator SmokeBossPatternTwo()
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
            while(patternIndex < 4)
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

    public void HideorAppear() // 숨거나 나타날때 이펙트 생성
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position + new Vector3(-0.11f,0,0);
    }

    public void IsisInvincibilityOn() // 무적 활성화
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // 무적 비활성화 
    {
        isInvincibility = false;
    }

    public void ColliderEnableOn()
    {
        boxCollider2D.enabled = true;
    }

    public void ColliderEnableOff()
    {
        boxCollider2D.enabled = false;
    }

    private IEnumerator Hit() 
    {
        isHit = true;
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }

    public void CoroutineStop() // 선택지 나올시 코루틴을 중지 시켜야함 
    {
        isChoice = true;
        isInvincibility = true; // 무적 활성화
        isHit = false;
        StopAllCoroutines();
        smokeMovePattern.CoroutineStop();
        smokeMiniDestructPattern.CoroutineStop();
        smokeAshtrayPattern.CoroutineStop();
        smokeSprayingFirePattern.CoroutineStop();
        animator.SetTrigger("Choice");
        IsisInvincibilityOn();
        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet") && isDie == false)
        {
            Destroy(collision.transform.gameObject);
            TakeDamage();
        }
    }

    private void ChoiceOn() // 일정피 닳으면 선택하기 위해 패턴 및 시스템 멈추고 다이얼로그 활성화
    {
        CoroutineStop();
        gameSystem.PauseAndTalk();
    }

    public void Resume() // 미구현
    {
        StartCoroutine("SmokeBossPatternTwo");
        HpRecharging(); // 피 재생성      
    }

    protected override void TakeDamage()
    {
        if (isHit == true || isInvincibility == true)
            return;

        currentHp--;
        if (isChoice == false)
        {
            if (limitBossHp >= currentHp)
            {
                ChoiceOn();
            }
        }
        else if (currentHp <= 0)
        {
            isDie = true;
        }
        StartCoroutine("Hit");
    }

    protected override void HpRecharging()
    {
        currentHp = GetSecondHp();
    }

    private void RandomPatternValue() // 중복없는 난수 출력 and 패턴 랜덤
    {
        for (int i = 0; i < patternRandomValue.Length; i++) // 중복없는 난수 출력
        {
            patternRandomValue[i] = Random.Range(0, patternRandomValue.Length);
            for (int j = 0; j < i; j++)
            {
                if (patternRandomValue[i] == patternRandomValue[j])
                {
                    i--;
                    break;
                }
            }
        }
    }
}
