using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIsystem : MonoBehaviour
{
    [SerializeField]
    private Slider bossHpBar;

    [SerializeField]
    private Boss boss;

    private void Start()
    {
        bossHpBar.maxValue = boss.Phase1BossHp;
        bossHpBar.value = boss.Phase1BossHp;
    }

    private void Update()
    {
        bossHpBar.value = boss.CurrentBossHp;
    }

    public void SliderMaxValueUpdate() // Hp bar의 MaxValue 업데이트
    {
        bossHpBar.maxValue = boss.Phase2BossHp;
        bossHpBar.value = boss.Phase2BossHp;
    }
}
