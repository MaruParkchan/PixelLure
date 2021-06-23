using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : MonoBehaviour, ICoroutineStop
{
    [SerializeField]
    private MapData cardBossMapData; // 보스 나타나는 좌표 데이터 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;

    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern; // 패턴1
    private CardSidePattern cardSidePattern;               // 패턴2
    private CardKingCardPattern cardKingCardPattern;       // 패턴3
    private CardBoomPattern cardBoomPattern;               // 패턴4

    [SerializeField]
    private int cardBossHp;
    public int CardBossHp { get { return cardBossHp; } }
    private int limitBossHp = 50; // 일정피가 된다면 선택지 등장할 hp 수치값

    private bool isInvincibility; //무적

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        isInvincibility = true; // 등장할때 무적인상태
    }

    private void Start()
    {
        StartCoroutine("CardBossPattern");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StopCoroutine("CardBossPattern");
            animator.SetTrigger("Choice");
            transform.position = Vector3.zero;
        }
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


    public void AuraEffectOn() // 아우리 이펙트 재생
    {
        auraEffect.Play();
    }

    public void AuraEffectOff() // 아우리 이펙트 정지
    {
        auraEffect.Stop();
    }

    public void IsisInvincibilityOn() // 무적 활성화
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // 무적 비활성화 
    {
        isInvincibility = false;
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
        cardRadialShapePattern.CoroutineStop();
        cardSidePattern.CoroutineStop();
        cardKingCardPattern.CoroutineStop();
        cardBoomPattern.CoroutineStop();
        //animator.ResetTrigger("Hide");
        //animator.ResetTrigger("Appear");
        //animator.ResetTrigger("Attack1");
        //animator.ResetTrigger("Attack2");
        animator.SetTrigger("Choice");
        this.transform.position = Vector3.zero;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.transform.gameObject);

            if(isInvincibility)
            {
                return;
            }

            if(limitBossHp >= cardBossHp)
            {
                Debug.Log("선택지!");
            }
        }
    }
}
