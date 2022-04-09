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
    
    public int GetHp() // ���� ü��
    {
        return currentHp;
    }

    public int GetFirstHp() // ������1 ü��
    {
        return firstHp;
    }

    public int GetSecondHp() // ������2 ü��
    {
        return secondHp;
    }

    protected abstract void HpRecharging(); // HP ��ȸ�� 


    protected abstract void TakeDamage();

}
