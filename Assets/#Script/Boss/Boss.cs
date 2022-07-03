using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{

    protected int currentBossHp; public int CurrentBossHp { get { return currentBossHp; } }
    [SerializeField] protected int phase1BossHp; public int Phase1BossHp { get { return phase1BossHp; } }
    [SerializeField] protected int phase2BossHp; public int Phase2BossHp { get { return phase2BossHp; } }
    [SerializeField] protected int limitPhaseHp; // 일정 HP 이하로 떨어지면 페이즈2 및 선택지 나올 체력값
    [SerializeField] protected GameObject bossHitEffect;
    protected bool isInvincibility = false;
    private float hitDelayCycleTime = 0.1f; // 피격시 몇초간 무적
    protected int[] patternRandomValue;

    private bool isBossDied = false; public bool IsBossDied { get { return isBossDied; } } // 죽었는가?
    private bool isAattacked = false; // 공격당했는가?
    private bool isChoice = false; // 선택시간일때;
    protected bool isPhaseCompled = false; // 페이즈가 바뀌었는가?
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

    public void IsisInvincibilityOn() // 무적 활성화
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // 무적 비활성화 
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

    protected abstract void SelectionEventTime(); // 선택지 나올시 제어할 메소드

    protected abstract void CoroutineAllStop();

    protected void HpRecharging() // 페이즈2 시작시 체력 재충전
    {
        currentBossHp = phase2BossHp;
    }

    protected void RandomPatternValue() // 중복없는 난수 출력 and 패턴 랜덤
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

    public void BossDiedEffect()
    {
      //  Instantiate(bossDieEffect, transform.position, Quaternion.identity);
    }
}
