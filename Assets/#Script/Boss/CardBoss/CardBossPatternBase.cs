using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBossPatternBase : MonoBehaviour
{
    protected Animator cardBossAnimator;
    protected CardBoss cardBoss;
    protected float waitTime = 2.0f;

    //[SerializeField]
    //protected int attackCount; // 공격 횟수
    //[SerializeField]
    //protected float attackDelayTime; // 재사용 공격 대기시간

    private void Awake()
    {
        cardBossAnimator = GetComponent<Animator>();
        cardBoss = GetComponent<CardBoss>();
        Init();
    }

    protected abstract void Init();
    public abstract IEnumerator Attacking();

}
