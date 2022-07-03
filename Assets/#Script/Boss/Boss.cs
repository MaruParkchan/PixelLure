using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{

    protected int currentBossHp; public int CurrentBossHp { get { return currentBossHp; } }
    [SerializeField] protected int phase1BossHp; public int Phase1BossHp { get { return phase1BossHp; } }
    [SerializeField] protected int phase2BossHp; public int Phase2BossHp { get { return phase2BossHp; } }
    [SerializeField] protected int limitPhaseHp; // ���� HP ���Ϸ� �������� ������2 �� ������ ���� ü�°�
    [SerializeField] protected GameObject bossHitEffect;
    protected bool isInvincibility = false;
    private float hitDelayCycleTime = 0.1f; // �ǰݽ� ���ʰ� ����
    protected int[] patternRandomValue;

    private bool isBossDied = false; public bool IsBossDied { get { return isBossDied; } } // �׾��°�?
    private bool isAattacked = false; // ���ݴ��ߴ°�?
    private bool isChoice = false; // ���ýð��϶�;
    protected bool isPhaseCompled = false; // ����� �ٲ���°�?
    protected Animator animator;
    protected GameSystem gameSystem;
    protected AudioSource bossAudioSource;

    protected SpriteRenderer bossSpriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossAudioSource = GetComponent<AudioSource>();
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        gameSystem = GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>();
        currentBossHp = phase1BossHp;
        StartCoroutine("Phase1");
    }

    protected abstract IEnumerator Phase1();
    protected abstract IEnumerator Phase2();
    private IEnumerator Hit()
    {
        isAattacked = true;
        yield return new WaitForSeconds(hitDelayCycleTime);
        isAattacked = false;
    }
    
    private IEnumerator HitEffect()
    {
        bossSpriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.05f);
        bossSpriteRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.transform.gameObject);
        

            TakeDamage(1);
        }  
    }

    private void ShowEffect(Collision col)
    {
        ContactPoint contact = col.contacts[0];
    }

    public void IsisInvincibilityOn() // ���� Ȱ��ȭ
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // ���� ��Ȱ��ȭ 
    {
        isInvincibility = false;
    }

    protected void TakeDamage(int damage)
    {
        if (isAattacked == true || isInvincibility == true || isBossDied == true)
            return;

        currentBossHp -= damage;

        if (isPhaseCompled == false && isChoice == false)
        {
            if (limitPhaseHp >= currentBossHp)
            {
                ChoiceStart();
                return;
            }
        }
        else if (isPhaseCompled == true && isChoice == true && currentBossHp <= 0)
        {
            isBossDied = true;
            GameSystem.BossDied();
            return;
        }

        StartCoroutine("Hit");
        StartCoroutine("HitEffect");
    }

    protected abstract void ColliderEnableOn();

    protected abstract void ColliderEnableOff();

    private void ChoiceStart()
    {
        StopAllCoroutines();
        isChoice = true;
        isAattacked = false;
        gameSystem.PauseAndTalk();
        animator.SetTrigger("Choice");
        IsisInvincibilityOn();
        ColliderEnableOff();
        SelectionEventTime();
    }

    public void PlayerDiedEvent()
    {
        CoroutineAllStop();
        animator.SetTrigger("Choice");
        bossAudioSource.Stop();
        isInvincibility = true;
        isAattacked = false;
    }

    public abstract void BossDiedEvent();

    public void Resume()
    {
        HpRecharging();
        isPhaseCompled = true;
        StartCoroutine("Phase2");
    }

    protected abstract void SelectionEventTime(); // ������ ���ý� ������ �޼ҵ�

    protected abstract void CoroutineAllStop();

    protected void HpRecharging() // ������2 ���۽� ü�� ������
    {
        currentBossHp = phase2BossHp;
    }

    protected void RandomPatternValue() // �ߺ����� ���� ��� and ���� ����
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

    public void BossDiedEffect()
    {
      //  Instantiate(bossDieEffect, transform.position, Quaternion.identity);
    }
}
