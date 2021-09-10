using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : Hp, ICoroutineStop, IPause
{
    [SerializeField]
    private MapData cardBossMapData; // ���� ��Ÿ���� ��ǥ ������ 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private Stage1System stage1System;
    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern; // ����1
    private CardSidePattern cardSidePattern;               // ����2
    private CardKingCardPattern cardKingCardPattern;       // ����3
    private CardBoomPattern cardBoomPattern;               // ����4

    [SerializeField]
    private int limitBossHp; // �����ǰ� �ȴٸ� ������ ������ hp ��ġ��
    private bool isChoice = false;
    private bool isDie = false;

    private bool isInvincibility; //����

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        ColliderEnableOff();
       // isInvincibility = true; // �����Ҷ� �����λ���
    }

    private void Start()
    {
        StartCoroutine("CardBossPattern");
    }

    private IEnumerator CardBossPattern()
    {
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            yield return StartCoroutine(cardRadialShapePattern.ICardRadialShapePattern());
            yield return StartCoroutine(cardSidePattern.ISidePattern());
            yield return StartCoroutine(cardKingCardPattern.ICardKingCardPattern());
            yield return StartCoroutine(cardBoomPattern.ICardBoomPattern());
        }
    }

    private IEnumerator CardBossPatternTwo()
    {
        while (true)
        {
            yield return StartCoroutine(cardRadialShapePattern.ICardRadialShapePattern());
            yield return StartCoroutine(cardSidePattern.ISidePattern());
            yield return StartCoroutine(cardKingCardPattern.ICardKingCardPattern());
            yield return StartCoroutine(cardBoomPattern.ICardBoomPattern());
        }
    }


    public void AuraEffectOn() // �ƿ츮 ����Ʈ ���
    {
        auraEffect.Play();
    }

    public void AuraEffectOff() // �ƿ츮 ����Ʈ ����
    {
        auraEffect.Stop();
    }

    public void AuraEffectClear()
    {
        auraEffect.Clear();
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

    public void CoroutineStop()
    {
        isChoice = true;
        isInvincibility = true; // ���� Ȱ��ȭ

        StopAllCoroutines();
        cardRadialShapePattern.CoroutineStop();
        cardSidePattern.CoroutineStop();
        cardKingCardPattern.CoroutineStop();
        cardBoomPattern.CoroutineStop();
        animator.SetTrigger("Choice");
        IsisInvincibilityOn();
        AuraEffectClear();
        AuraEffectOn();
        this.transform.position = Vector3.zero;        
    }

    public void Resume()
    {
        StartCoroutine("CardBossPatternTwo");
        IsisInvincibilityOff();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBullet") && isDie == false)
        {
            Destroy(collision.transform.gameObject);

            if (isInvincibility)
                return;

            TakeDamage();

            if (isChoice == false)
            {
                if (limitBossHp >= hp) // ������ ���� �Ǵ¼��� ������â Ȱ��ȭ
                {
                    ChoiceOn();
                }
            }
            else if(hp <= 0)
            {
                isDie = true;
            }
        }
    }

    private void ChoiceOn()
    {
        CoroutineStop();
        stage1System.PauseAndTalk();
    }

    protected override void TakeDamage()
    {
        if (isInvincibility)
        {
            return;
        }
        hp--;
    }
}
