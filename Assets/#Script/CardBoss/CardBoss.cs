using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : BossHp, ICoroutineStop, IPause
{
    [SerializeField]
    private MapData cardBossMapData; // ���� ��Ÿ���� ��ǥ ������ 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private GameSystem gameSystem;
    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern; // ����1
    private CardSidePattern cardSidePattern;               // ����2
    private CardKingCardPattern cardKingCardPattern;       // ����3
    private CardBoomPattern cardBoomPattern;               // ����4
    private int[] patternRandomValue = new int[4]; // ������ �����Ŀ� ���� ������ ���� ��
                                                   // ũ��� ������ �� ��ŭ �����ؾ���
   
    [SerializeField]
    private int limitBossHp; // �����ǰ� �ȴٸ� ������ ������ hp ��ġ��
    private int phaseCount = 0;
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
        currentHp = GetFirstHp();
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

    private IEnumerator CardBossPatternTwo() // ������ ������ ���� ����� �ڷ�ƾ 
    {
        // ����۵Ǵ� ������ ������ ���⼭ �ο��ϰ� �����ؾ��ҵ� 
        animator.SetTrigger("Hide");
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
                Debug.Log("������ ������ ���� ����� " + patternIndex);
            }
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
    private bool isHit = false;

    private IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
    }

    public void Resume() // ������ �������� ���� �������� �����
    {
        HpRecharging(); // �� �����
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
                if (limitBossHp >= currentHp) // ������ ���� �Ǵ¼��� ������â Ȱ��ȭ
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

    private void ChoiceOn() // ������ ������ �����ϱ� ���� ���� �� �ý��� ���߰� ���̾�α� Ȱ��ȭ
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
