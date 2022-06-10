using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogBossPatternBase : MonoBehaviour
{
    protected Animator dogBossAnimator;
    protected DogBoss dogBoss;
    protected float waitTime = 2.0f;

    //[SerializeField]
    //protected int attackCount; // ���� Ƚ��
    //[SerializeField]
    //protected float attackDelayTime; // ���� ���� ���ð�

    private void Awake()
    {
        dogBossAnimator = GetComponent<Animator>();
        dogBoss = GetComponent<DogBoss>();
        Init();
    }

    protected abstract void Init();
    public abstract IEnumerator Attacking();
}
