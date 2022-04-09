using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : BossHp, ICoroutineStop, IPause
{
    [SerializeField]
    private MapData cardBossMapData; // 보스 나타나는 좌표 데이터 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private GameSystem gameSystem;
    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern; // 패턴1
    private CardSidePattern cardSidePattern;               // 패턴2
    private CardKingCardPattern cardKingCardPattern;       // 패턴3
    private CardBoomPattern cardBoomPattern;               // 패턴4
    private int[] patternRandomValue = new int[4]; // 선택지 선택후에 랜덤 패턴을 위한 값
                                                   // 크기는 패턴의 수 만큼 조정해야함
   
    [SerializeField]
    private int limitBossHp; // 일정피가 된다면 선택지 등장할 hp 수치값
    private int phaseCount = 0;
    private bool isChoice = false;
    private bool isDie = false;

    private bool isInvincibility; //무적

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentHp = GetFirstHp();
       // isInvincibility = true; // 등장할때 무적인상태
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

    private IEnumerator CardBossPatternTwo() // 선택지 선택후 패턴 재시작 코루틴 
    {
        // 재시작되는 패턴의 값들을 여기서 부여하고 시작해야할듯 
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(2.0f);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true) // ** 중첩 while문으로써 뭔가 맘에 안듬 **
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
                Debug.Log("선택지 선택후 패턴 재시작 " + patternIndex);
            }
        }
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
        boxCollider2D.enabled = true;
    }

    public void ColliderEnableOff()
    {
        boxCollider2D.enabled = false;
    }

    public void CoroutineStop()
    {
        isChoice = true;
        isInvincibility = true; // 무적 활성화

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
    private bool isHit = false;

    private IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
    }

    public void Resume() // 선택을 다했으면 패턴 랜덤으로 재시작
    {
        HpRecharging(); // 피 재생성
        StartCoroutine("CardBossPatternTwo");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBullet") && isDie == false)
        {
            Destroy(collision.transform.gameObject);

            if (isHit == true || isInvincibility == true)
                return;

            TakeDamage();

            if (isChoice == false)
            {
                if (limitBossHp >= currentHp) // 일정피 이하 되는순간 선택지창 활성화
                {
                    ChoiceOn();
                }
            }
            else if(currentHp <= 0)
            {
                isDie = true;
            }
        }
    }

    private void ChoiceOn() // 일정피 닳으면 선택하기 위해 패턴 및 시스템 멈추고 다이얼로그 활성화
    {
        CoroutineStop();
        gameSystem.PauseAndTalk();
    }

    protected override void TakeDamage()
    {
        currentHp--;
    }

    protected override void HpRecharging()
    {
        currentHp = GetSecondHp();
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
}
