using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogDogBossStage : DiglogData
{
    public override void TextFistInitUpdate()
    {
        DiglogGameSystemTextUpdate(dogBossFirstDiglog);
    }

    public override void TextDataUpdate(bool isAccept)
    {
        if (isAccept == true)
        {
            DiglogGameSystemTextUpdate(dogBossLeftChoiceDiglog);
        }
        else
        {
            DiglogGameSystemTextUpdate(dogBossRightChoiceDiglog);
        }
    }
}
