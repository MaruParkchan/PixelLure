using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogCardStage : DiglogData
{
    [SerializeField] private GameSystem gameSystem;

    private void Start()
    {
        FirstDiglogUpdate();
    }

    private void FirstDiglogUpdate() // 카드 보스 첫 대화 데이터 GameSystem에 넣기
    {
        gameSystem.TextDataSetUpdate(cardBossFirstDiglogs_Korean);
    }

    public override void TextDataUpdate(int value)
    {
        if(value == 0)
        {
            gameSystem.TextDataSetUpdate(cardBossLeftChoiceDiglogs_Korean);
        }
        else
        {
            gameSystem.TextDataSetUpdate(cardBossRightChoiceDiglogs_Korean);
        }
    }
}
