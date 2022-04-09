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
    private BoxCollider2D boxCollider2D;
    private int[] patternRandomValue = new int[3]; // ������ �����Ŀ� ���� ������ ���� ��
                                                   // ũ��� ������ �� ��ŭ �����ؾ���

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

    protected override void HpRecharging(int PhaseValue)
    {
        
    }
}
