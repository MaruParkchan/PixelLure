using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : Boss
{
    [SerializeField]
    private MapData cardBossMapData; // ���� ��Ÿ���� ��ǥ ������ 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;

    private CardRadialShapePattern cardRadialShapePattern; // ����1
    private CardSidePattern cardSidePattern;               // ����2
    private CardKingCardPattern cardKingCardPattern;       // ����3
    private CardBoomPattern cardBoomPattern;               // ����4
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        patternRandomValue = new int[4]; // ������ �����Ŀ� ���� ������ ���� �� ũ��� ������ �� ��ŭ �����ؾ���
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

    protected override IEnumerator Phase1()
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

    protected override IEnumerator Phase2()
    {
        // ����۵Ǵ� ������ ������ ���⼭ �ο��ϰ� �����ؾ��ҵ� 
        animator.SetTrigger("Hide");
        HpRecharging();
        yield return new WaitForSeconds(2.0f);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true) // ** ��ø while�����ν� ���� ���� �ȵ� **
        {
            int patternIndex = 0;
            while (patternIndex < 4)
            {
                RandomPatternValue();
                if (patternRandomValue[patternIndex] == 0)
                    yield return StartCoroutine(cardRadialShapePattern.ICardRadialShapePattern());
                if (patternRandomValue[patternIndex] == 1)
                    yield return StartCoroutine(cardSidePattern.ISidePattern());
                if (patternRandomValue[patternIndex] == 2)
                    yield return StartCoroutine(cardKingCardPattern.ICardKingCardPattern());
                if (patternRandomValue[patternIndex] == 3)
                    yield return StartCoroutine(cardBoomPattern.ICardBoomPattern());

                patternIndex++;
            }
        }
    }

    protected override void SelectionEventTime()
    {
        cardRadialShapePattern.CoroutineStop();
        cardSidePattern.CoroutineStop();
        cardKingCardPattern.CoroutineStop();
        cardBoomPattern.CoroutineStop();
        AuraEffectClear();
        AuraEffectOn();       
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
