using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogDogBossStage : DiglogData
{
    private void Start()
    {
        FirstDiglogUpdate();
    }

    private void FirstDiglogUpdate() // ī�� ���� ù ��ȭ ������ GameSystem�� �ֱ�
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
