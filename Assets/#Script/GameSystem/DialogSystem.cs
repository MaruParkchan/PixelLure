using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    private string[] cardBossDiglog;
    private DiglogData diglogData;

    private void Awake()
    {
        cardBossDiglog = diglogData.cardBossFitstDiglogs;
    }
}
