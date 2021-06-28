using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hp : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    
    public int GetHp()
    {
        return hp;
    }

    protected abstract void TakeDamage();

}
