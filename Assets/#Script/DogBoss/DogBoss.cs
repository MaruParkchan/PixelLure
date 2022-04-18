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
    private bool isBulkUp; // ��ũ���Ͽ��°�?

    [SerializeField]
    private GameSystem gameSystem;

    [SerializeField]
    private int limitBossHp; // �����ǰ� �ȴٸ� ������ ������ hp ��ġ��
    private bool isChoice = false;
    private bool isHit = false;
    private bool isDie = false;
    private bool isInvincibility; // �����ΰ�?
    private CircleCollider2D circleCollider2D;
    private float bigDogBosscirCleColliderOffsetY;
    private float bigDogBosscirCleColliderRadius;
    private int[] patternRandomValue = new int[3]; // ������ �����Ŀ� ���� ������ ���� ��
                                                   // ũ��� ������ �� ��ŭ �����ؾ���

    private void BigBossCircleColliderPositionAndSizeData() // �� ���� ��ũ���ϸ� �ݶ��̴� ��ȭ�� 
    {
        bigDogBosscirCleColliderOffsetY = 1.05f;
        bigDogBosscirCleColliderRadius = 0.33f;
    }

    private void CircleColliderInit() // �ݶ��̴� ũ��, ��ġ ��ȭ
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

    public void IsisInvincibilityOn() // ���� Ȱ��ȭ
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // ���� ��Ȱ��ȭ 
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
        HpRecharging(); // �� �����     
    }

    private void RandomPatternValue() // �ߺ����� ���� ��� and ���� ����
    {
        for (int i = 0; i < patternRandomValue.Length; i++) // �ߺ����� ���� ���
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
