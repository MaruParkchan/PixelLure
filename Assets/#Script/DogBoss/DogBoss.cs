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
    private CircleCollider2D circleCollider2D;
    private float bigDogBosscirCleColliderOffsetY;
    private float bigDogBosscirCleColliderRadius;
    private int[] patternRandomValue = new int[3]; // 선택지 선택후에 랜덤 패턴을 위한 값
                                                   // 크기는 패턴의 수 만큼 조정해야함

    private void BigBossCircleColliderPositionAndSizeData() // 개 보스 벌크업하면 콜라이더 변화값 
    {
        bigDogBosscirCleColliderOffsetY = 1.05f;
        bigDogBosscirCleColliderRadius = 0.33f;
    }

    private void CircleColliderInit() // 콜라이더 크기, 위치 변화
    {
        BigBossCircleColliderPositionAndSizeData();
        circleCollider2D.offset = new Vector2(0, bigDogBosscirCleColliderOffsetY);
        circleCollider2D.radius = bigDogBosscirCleColliderRadius;

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dogBubblePattern = GetComponent<DogBubblePattern>();
        dogSmallSojuPattern = GetComponent<DogSmallSojuPattern>();
        dogBigTracePattern = GetComponent<DogBigTracePattern>();
        dogBigLaserPattern = GetComponent<DogBigLaserPattern>();
        dogBigPoundingPattern = GetComponent<DogBigPoundingPattern>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        currentHp = GetFirstHp();
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
        animator.SetTrigger("BulkUp");
        CircleColliderInit();;
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            //yield return StartCoroutine(dogBigTracePattern.ISpawnSoju());
            //yield return StartCoroutine(dogBigLaserPattern.ILaserPattern());
            yield return StartCoroutine(dogBigPoundingPattern.ISojuRain());
        }
    }

    private IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }

    public void CoroutineStop()
    {
        isChoice = true;
        isInvincibility = true;
        isHit = false;
        StopAllCoroutines();
        dogBubblePattern.CoroutineStop();
        dogSmallSojuPattern.CoroutineStop();
        animator.SetTrigger("Choice");
        IsisInvincibilityOn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet") && isDie == false)
        {
            Destroy(collision.transform.gameObject);
            TakeDamage();
        }
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
        circleCollider2D.enabled = true;
    }

    public void ColliderEnableOff()
    {
        circleCollider2D.enabled = false;
    }

    public void Resume()
    {
        StartCoroutine("DogBossBulkUpPattern");
        HpRecharging(); // 피 재생성     
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

    protected override void HpRecharging()
    {
        currentHp = GetSecondHp();
    }
}
