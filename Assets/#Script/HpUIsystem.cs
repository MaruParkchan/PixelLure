using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIsystem : MonoBehaviour
{
    [SerializeField]
    private Slider bossHpBar;

    [SerializeField]
    private Hp bossHp;

    private void Start()
    {
        bossHpBar.maxValue = bossHp.GetHp();
        bossHpBar.value = bossHp.GetHp();
    }

    private void Update()
    {
        bossHpBar.value = bossHp.GetHp();
    }


}
