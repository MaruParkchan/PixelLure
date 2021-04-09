using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoss : MonoBehaviour
{
    [SerializeField]
    private MapData cardBossMapData; // 보스 나타나는 좌표 데이터 
    public MapData CardBossMapData => cardBossMapData;
    [Header("AuraEffect")]
    [SerializeField]
    private ParticleSystem auraEffect;

    private Animator animator;
    private CardRadialShapePattern cardRadialShapePattern;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardRadialShapePattern = GetComponent<CardRadialShapePattern>();
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
        }
        
    }

    private bool isInvincibility; //무적

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
}
