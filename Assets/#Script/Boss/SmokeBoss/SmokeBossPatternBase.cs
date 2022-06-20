using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmokeBossPatternBase : MonoBehaviour
{
    protected Animator smokeBossAnimator;
    protected SmokeBoss smokeBoss;
    protected float waitTime = 2.0f;

    private void Awake()
    {
        smokeBossAnimator = GetComponent<Animator>();
        smokeBoss = GetComponent<SmokeBoss>();
        Init();
    }

    protected abstract void Init();
    public abstract IEnumerator Attacking();

}
