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
    private int limitBossHp; // �����ǰ� �ȴٸ� ������ ������ hp ��ġ��
    private bool isChoice = false;
    private bool isHit = false;
    private bool isDie = false;
    private bool isInvincibility; // �����ΰ�?
    private BoxCollider2D boxCollider2D;
    private int[] patternRandomValue = new int[4]; // ������ �����Ŀ� ���� ������ ���� ��
                                                   // ũ��� ������ �� ��ŭ �����ؾ���

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

    private IEnumerator SmokeBossPattern() // ù��° ���� < ������ �� >
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

    public void HideorAppear() // ���ų� ��Ÿ���� ����Ʈ ����
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = transform.position + new Vector3(-0.11f,0,0);
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

    private IEnumerator Hit() 
    {
        isHit = true;
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }

    public void CoroutineStop() // ������ ���ý� �ڷ�ƾ�� ���� ���Ѿ��� 
    {
        isChoice = true;
        isInvincibility = true; // ���� Ȱ��ȭ
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

    private void ChoiceOn() // ������ ������ �����ϱ� ���� ���� �� �ý��� ���߰� ���̾�α� Ȱ��ȭ
    {
        CoroutineStop();
        gameSystem.PauseAndTalk();
    }

    public void Resume() // �̱���
    {
        StartCoroutine("SmokeBossPatternTwo");
        HpRecharging(); // �� �����      
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
}
