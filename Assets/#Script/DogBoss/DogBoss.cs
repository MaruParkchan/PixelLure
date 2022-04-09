using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : BossHp, ICoroutineStop, IPause
{
    private Animator animator;

    private DogBubblePattern dogBubblePattern;
    private DogSmallSojuPattern dogSmallSojuPattern;

    private DogBigTracePattern dogBigTracePattern;
    private DogBigLaserPattern dogBigLaserPattern;
    private DogBigPoundingPattern dogBigPoundingPattern;

    [SerializeField]
    private bool isBulkUp; // 벌크업하였는가?

    [SerializeField]
    private GameSystem gameSystem;

    [SerializeField]
    private int limitBossHp; // 일정피가 된다면 선택지 등장할 hp 수치값
    private bool isChoice = false;
    private bool isHit = false;
    private bool isDie = false;
    private bool isInvincibility; // 무적인가?
    private BoxCollider2D boxCollider2D;
    private int[] patternRandomValue = new int[3]; // 선택지 선택후에 랜덤 패턴을 위한 값
                                                   // 크기는 패턴의 수 만큼 조정해야함

    private void Start()
    {
        dogBubblePattern = GetComponent<DogBubblePattern>();
        dogSmallSojuPattern = GetComponent<DogSmallSojuPattern>();
        dogBigTracePattern = GetComponent<DogBigTracePattern>();
        dogBigLaserPattern = GetComponent<DogBigLaserPattern>();
        dogBigPoundingPattern = GetComponent<DogBigPoundingPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(DogBossSmallPattern());
    }

    private IEnumerator DogBossSmallPattern()
    {
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            yield return StartCoroutine(dogBubblePattern.IBubbleSpawner());
            yield return StartCoroutine(dogSmallSojuPattern.ISojuPattern());
        }
    }

    private IEnumerator DogBossBulkUpPattern()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            yield return StartCoroutine(dogBigTracePattern.ISpawnSoju());
            yield return StartCoroutine(dogBigLaserPattern.ILaserPattern());
            yield return StartCoroutine(dogBigPoundingPattern.ISojuRain());
        }
    }

    private IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.transform.gameObject);
            if (isHit == true || isInvincibility == true)
                return;

            StartCoroutine("Hit");
            TakeDamage();

            if (isChoice == false)
            {
                if (currentHp <= 0)
                {
                    ChoiceOn();
                }
            }
            else if (currentHp <= 0)
            {
                isDie = true;
            }
        }
    }

    private void ChoiceOn()
    {
        CoroutineStop();
        gameSystem.PauseAndTalk();
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

    protected override void TakeDamage()
    {
        currentHp--;
    }

    public void CoroutineStop()
    {
        isChoice = true;
        isInvincibility = true;

        StopAllCoroutines();
    }

    public void Resume()
    {

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

    protected override void HpRecharging(int PhaseValue)
    {
        
    }
}
