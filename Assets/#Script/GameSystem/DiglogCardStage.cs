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

    private void FirstDiglogUpdate() // ī�� ���� ù ��ȭ ������ GameSystem�� �ֱ�
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
