using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LauguageSystem { English, Korean}

public abstract class DiglogData : MonoBehaviour
{

    protected string[] cardBossFirstDiglogs_Korean         = new string[3];
    protected string[] cardBossLeftChoiceDiglogs_Korean    = new string[3];
    protected string[] cardBossRightChoiceDiglogs_Korean   = new string[3];

    protected string[] smokeBossFirstDiglogs_Korean        = new string[4];
    protected string[] smokeBossLeftChoiceDiglogs_Korean   = new string[3];
    protected string[] smokeBossRightChoiceDiglogs_Korean  = new string[3];

    //private string[] smokeBossDiglogs_Korean;
    //private string[] dogBossDiglogs_Korean;

    private void Awake()
    {
        DogBossDiglogInit();
        SmokeBossDiglogInit();
    }

    private void DogBossDiglogInit() // 대화 데이터 초기화
    {
        cardBossFirstDiglogs_Korean[0]  = "훌륭하구나...";
        cardBossFirstDiglogs_Korean[1]  = "너의 능력을 시험한다.";
        cardBossFirstDiglogs_Korean[2]  = "인간이여, 나와 함께 도박을 하겠는가?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "나와 함께 도박을 하고 싶다니";
        cardBossLeftChoiceDiglogs_Korean[1]  = "인간이란 어쩔수 없는 존재이지";
        cardBossLeftChoiceDiglogs_Korean[2]  = "보스의 변화가 이루워졌습니다.";

        cardBossRightChoiceDiglogs_Korean[0]  = "도박을 하고 싶지 않다니???";
        cardBossRightChoiceDiglogs_Korean[1] = "그 선택 후회하게 해주겠다.";
        cardBossRightChoiceDiglogs_Korean[2] = "플레이어의 변화가 이루워졌습니다.";
    }

    private void SmokeBossDiglogInit()
    {
        smokeBossFirstDiglogs_Korean[0] = "하하하하 대단한데??!";
        smokeBossFirstDiglogs_Korean[1] = "나를 정말로 화나게 만든건 니가 처음이야!";
        smokeBossFirstDiglogs_Korean[2] = "너에게 좋은 제안을 하겠어!";
        smokeBossFirstDiglogs_Korean[3] = "나의 담배 연기를 마셔볼래??"; // 이거의 변화 - 희원이랑 회의 < Yes , No 선택시 변화가 바뀌어야할듯 > 

        smokeBossLeftChoiceDiglogs_Korean[0] = "어리석은 자식~! 크크크 ";
        smokeBossLeftChoiceDiglogs_Korean[1] = "나의 속임수에 걸려들었어~~ 하하";
        smokeBossLeftChoiceDiglogs_Korean[2] = "보스의 변화가 이루워졌습니다.";

        smokeBossRightChoiceDiglogs_Korean[0] = "나의 연기를 거부해?!?!?";
        smokeBossRightChoiceDiglogs_Korean[1] = "정말로 나를 화나게 하는군! 너를 죽여주겠다!";
        smokeBossRightChoiceDiglogs_Korean[2] = "플레이어의 변화가 이루워졌습니다.";
    }

    public abstract void TextDataUpdate(int value);


    //public void LanguageChange(int index)// 언어 변경
    //{
    //    PlayerPrefs.SetInt("LanguageData", index);

    //    for(int i = 0; i < cardBossFitstDiglogs.Length; i++)
    //    {
    //        if(index == 0)
    //        {
    //            cardBossFitstDiglogs[i] = cardBossFirstDiglogs_English[i];
    //        }
    //        else if (index == 1)
    //        {
    //            cardBossFitstDiglogs[i] = cardBossFirstDiglogs_Korean[i];
    //        }
    //    }
    //}

}
