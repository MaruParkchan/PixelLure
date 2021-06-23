using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : MonoBehaviour, ICoroutineStop
{
    [SerializeField]
    private MapData cardBossMapData; // ���� ��Ÿ���� ��ǥ ������ 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;

    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern; // ����1
    private CardSidePattern cardSidePattern;               // ����2
    private CardKingCardPattern cardKingCardPattern;       // ����3
    private CardBoomPattern cardBoomPattern;               // ����4

    [SerializeField]
    private int cardBossHp;
    public int CardBossHp { get { return cardBossHp; } }
    private int limitBossHp = 50; // �����ǰ� �ȴٸ� ������ ������ hp ��ġ��

    private bool isInvincibility; //����

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
        cardSidePattern = GetComponent<CardSidePattern>();
        cardKingCardPattern = GetComponent<CardKingCardPattern>();
        cardBoomPattern = GetComponent<CardBoomPattern>();
        isInvincibility = true; // �����Ҷ� �����λ���
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


    public void AuraEffectOn() // �ƿ츮 ����Ʈ ���
    {
        auraEffect.Play();
    }

    public void AuraEffectOff() // �ƿ츮 ����Ʈ ����
    {
        auraEffect.Stop();
    }

    public void IsisInvincibilityOn() // ���� Ȱ��ȭ
    {
        isInvincibility = true;
    }

    public void IsisInvincibilityOff() // ���� ��Ȱ��ȭ 
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
                Debug.Log("������!");
            }
        }
    }
}
