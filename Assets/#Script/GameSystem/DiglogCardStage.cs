using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogCardStage : DiglogData
{
    public override void TextFistInitUpdate()
    {
        DiglogGameSystemTextUpdate(cardBossFirstDiglog);
    }

    public override void TextDataUpdate(bool isAccept)
    {
        if(isAccept == true)
        {
            DiglogGameSystemTextUpdate(cardBossLeftChoiceDiglog);
        }
        else
        {
            DiglogGameSystemTextUpdate(cardBossRightChoiceDiglog);
        }
    }
}
