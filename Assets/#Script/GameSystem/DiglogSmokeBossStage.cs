using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogSmokeBossStage : DiglogData
{
    public override void TextFistInitUpdate()
    {
        DiglogGameSystemTextUpdate(smokeBossFirstDiglog);
    }

    public override void TextDataUpdate(bool isAccept)
    {
        if (isAccept == true)
        {
            DiglogGameSystemTextUpdate(smokeBossLeftChoiceDiglog);
        }
        else
        {
            DiglogGameSystemTextUpdate(smokeBossRightChoiceDiglog);
        }
    }
}
