using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nameText;

    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] [Range(0.01f, 1.00f)] private float timer;

    [SerializeField] private bool isStop = false;


    private string bossName;
    private char nameData;
    void Start()
    {
        StartCoroutine(RandomTextEffectCycle());
    }

    private void RandomTextSetting(int index)
    {
        nameData = (char)Random.Range(65, 91);
        nameText[index].text = "" + nameData;
        bossName += nameData;
        bossNameText.text = bossName;
    }
    private void RandomTextOutput()
    {
        bossName = "";
        for (int i = 0; i < nameText.Length; i++)
        {
            RandomTextSetting(i);
        }
    }

    IEnumerator RandomTextEffectCycle()
    {
        while (isStop == false)
        {
            RandomTextOutput();
            yield return new WaitForSeconds(timer);
            RandomTextOutput();
        }
    }

}
