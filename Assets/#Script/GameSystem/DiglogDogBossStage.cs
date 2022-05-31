using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogDogBossStage : DiglogData
{
    private void Start()
    {
        FirstDiglogUpdate();
    }

    private void FirstDiglogUpdate() // 카드 보스 첫 대화 데이터 GameSystem에 넣기
    {
        //gameSystem.TextDataSetUpdate(dogBossFirstDiglogs_Korean);
    }

    public override void TextFistInitUpdate()
    {

    }

    public override void TextDataUpdate(bool isAccept)
    {
        if(isAccept == true)
        {
            //gameSystem.TextDataSetUpdate(dogBossLeftChoiceDiglogs_Korean);
        }
        else
        {
            //gameSystem.TextDataSetUpdate(dogBossRightChoiceDiglogs_Korean);
        }
    }
}
