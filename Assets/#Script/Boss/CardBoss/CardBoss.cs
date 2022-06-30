using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : Boss
{
    [SerializeField]
    private MapData cardBossMapData; // 보스 나타나는 좌표 데이터 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField] private ParticleSystem auraEffect;

    public CardBossData cardBossData;
    public CardBossData phase1CardBossData;
    public CardBossData phase2CardBossData;
    private CardRadialShapePattern cardRadialShapePattern; // 패턴1
    private CardSidePattern cardSidePattern;               // 패턴2
    private CardKingCardPattern cardKingCardPattern;       // 패턴3
    private CardBoomPattern cardBoomPattern;               // 패턴4
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        patternRandomValue = new int[4]; // 선택지 선택후에 랜덤 패턴을 위한 값 크기는 패턴의 수 만큼 조정해야함
        PhaseChange(phase1CardBossData);
    }

    private void PhaseChange(CardBossData cardBossData)
    {
        this.cardBossData = cardBossData;
    }

    public void AuraEffectOn() // 아우리 이펙트 재생
    {
        auraEffect.Play();
    }

    public void AuraEffectOff() // 아우리 이펙트 정지
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
        // 재시작되는 패턴의 값들을 여기서 부여하고 시작해야할듯 
        animator.SetTrigger("Hide");
        HpRecharging();
        if (GameSystem.isAccept)  //여기서 선택조건으로 변경후 페이즈 시작
            PhaseChange(phase2CardBossData);

        
        yield return new WaitForSeconds(2.0f);
        ColliderEnableOn();
        IsisInvincibilityOff();

        //여기서 선택조건으로 변경후 페이즈 시작

        while (true) // ** 중첩 while문으로써 뭔가 맘에 안듬 **
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

    public override void BossDiedEvent()
    {
        CoroutineAllStop();
        AuraEffectOff();
        this.transform.position = Vector3.zero;
        animator.SetTrigger("Die");
        bossAudioSource.Stop();

        if (GameSystem.isAccept == true)
        {
            PlayerPrefs.SetInt("Stage1_RedChain", 1);
        }
        else if (GameSystem.isAccept == false)
        {
            PlayerPrefs.SetInt("Stage1_BlueChain", 1);
        }
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
