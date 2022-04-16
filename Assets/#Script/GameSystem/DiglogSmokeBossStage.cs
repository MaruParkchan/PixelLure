using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogSmokeBossStage : DiglogData
{
    [SerializeField] private GameSystem gameSystem;

    private void Start()
    {
        FirstDiglogUpdate();
    }

    private void FirstDiglogUpdate() // 카드 보스 첫 대화 데이터 GameSystem에 넣기
    {
        gameSystem.TextDataSetUpdate(smokeBossFirstDiglogs_Korean);
    }

    public override void TextDataUpdate(bool isAccept)
    {
        if (isAccept == true)
        {
            gameSystem.TextDataSetUpdate(smokeBossLeftChoiceDiglogs_Korean);
        }
        else
        {
            gameSystem.TextDataSetUpdate(smokeBossRightChoiceDiglogs_Korean);
        }
    }
}
