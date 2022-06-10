using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBossPatternBase : MonoBehaviour
{
    protected Animator cardBossAnimator;
    protected CardBoss cardBoss;
    protected float waitTime = 2.0f;

    //[SerializeField]
    //protected int attackCount; // ���� Ƚ��
    //[SerializeField]
    //protected float attackDelayTime; // ���� ���� ���ð�

    private void Awake()
    {
        cardBossAnimator = GetComponent<Animator>();
        cardBoss = GetComponent<CardBoss>();
        Init();
    }

    protected abstract void Init();
    public abstract IEnumerator Attacking();

}
