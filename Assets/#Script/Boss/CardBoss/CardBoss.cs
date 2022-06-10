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

    public CardBossData cardBossData;
    public CardBossData phase1CardBossData;
    public CardBossData phase2CardBossData;
    private CardRadialShapePattern cardRadialShapePattern; // ����1
    private CardSidePattern cardSidePattern;               // ����2
    private CardKingCardPattern cardKingCardPattern;       // ����3
    private CardBoomPattern cardBoomPattern;               // ����4
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        //cardBossData = GetComponent<CardBossData>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        patternRandomValue = new int[4]; // ������ �����Ŀ� ���� ������ ���� �� ũ��� ������ �� ��ŭ �����ؾ���
        PhaseChange(phase1CardBossData);
    }

    private void PhaseChange(CardBossData cardBossData)
    {
        this.cardBossData = cardBossData;
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
            if (cardBossData.isP1 == true)
                yield return StartCoroutine(cardRadialShapePattern.Attacking());
            if (cardBossData.isP2 == true)
                yield return StartCoroutine(cardSidePattern.Attacking());
            if (cardBossData.isP3 == true)
                yield return StartCoroutine(cardKingCardPattern.Attacking());
            if (cardBossData.isP4 == true)
                yield return StartCoroutine(cardBoomPattern.Attacking());


            if (!cardBossData.isP1 && !cardBossData.isP2 && !cardBossData.isP3 && !cardBossData.isP4)
                cardBossData.isP1 = true;

            yield return null;
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

        //���⼭ ������������ ������ ������ ����
        PhaseChange(phase2CardBossData);

        while (true) // ** ��ø while�����ν� ���� ���� �ȵ� **
        {
            if (cardBossData.isP1 == true)
                yield return StartCoroutine(cardRadialShapePattern.Attacking());
            if (cardBossData.isP2 == true)
                yield return StartCoroutine(cardSidePattern.Attacking());
            if (cardBossData.isP3 == true)
                yield return StartCoroutine(cardKingCardPattern.Attacking());
            if (cardBossData.isP4 == true)
                yield return StartCoroutine(cardBoomPattern.Attacking());

            if (!cardBossData.isP1 && !cardBossData.isP2 && !cardBossData.isP3 && !cardBossData.isP4)
                cardBossData.isP1 = true;
            /*
            int patternIndex = 0;
            while (patternIndex < 4)
            {
                RandomPatternValue();
                if (patternRandomValue[patternIndex] == 0)
                    yield return StartCoroutine(cardRadialShapePattern.Attacking());
                if (patternRandomValue[patternIndex] == 1)
                    yield return StartCoroutine(cardSidePattern.Attacking());
                if (patternRandomValue[patternIndex] == 2)
                    yield return StartCoroutine(cardKingCardPattern.Attacking());
                if (patternRandomValue[patternIndex] == 3)
                    yield return StartCoroutine(cardBoomPattern.Attacking());

                patternIndex++;
            }
            */
            yield return null;
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

    protected override void CoroutineAllStop()
    {
        cardRadialShapePattern.CoroutineStop();
        cardSidePattern.CoroutineStop();
        cardKingCardPattern.CoroutineStop();
        cardBoomPattern.CoroutineStop();
        StopAllCoroutines();
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
