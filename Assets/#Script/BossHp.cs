using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossHp : MonoBehaviour
{
    protected int currentHp;

    [SerializeField]
    protected int firstHp;

    [SerializeField]
    protected int secondHp;
    
    public int GetHp() // 현재 체력
    {
        return currentHp;
    }

    public int GetFirstHp() // 페이즈1 체력
    {
        return firstHp;
    }

    public int GetSecondHp() // 페이즈2 체력
    {
        return secondHp;
    }

    protected abstract void HpRecharging(); // HP 재회복 


    protected abstract void TakeDamage();

}
