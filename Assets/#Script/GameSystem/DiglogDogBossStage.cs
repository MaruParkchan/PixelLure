using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogDogBossStage : DiglogData
{
    [SerializeField] private GameSystem gameSystem;

    private void Start()
    {
        FirstDiglogUpdate();
    }

    private void FirstDiglogUpdate() // 카드 보스 첫 대화 데이터 GameSystem에 넣기
    {
        gameSystem.TextDataSetUpdate(dogBossFirstDiglogs_Korean);
    }

    public override void TextDataUpdate(int value)
    {
        if(value == 0)
        {
            gameSystem.TextDataSetUpdate(dogBossLeftChoiceDiglogs_Korean);
        }
        else
        {
            gameSystem.TextDataSetUpdate(dogBossRightChoiceDiglogs_Korean);
        }
    }
}
