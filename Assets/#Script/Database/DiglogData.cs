using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LauguageSystem { English, Korean}

public abstract class DiglogData : MonoBehaviour
{

    protected string[] cardBossFirstDiglogs_Korean         = new string[3];
    protected string[] cardBossLeftChoiceDiglogs_Korean    = new string[3];
    protected string[] cardBossRightChoiceDiglogs_Korean   = new string[3];

    //private string[] smokeBossDiglogs_Korean;
    //private string[] dogBossDiglogs_Korean;

    private void Awake()
    {
        DogDiglogInit();
    }

    private void DogDiglogInit() // ��ȭ ������ �ʱ�ȭ
    {
        cardBossFirstDiglogs_Korean[0]  = "�Ǹ��ϱ���...";
        cardBossFirstDiglogs_Korean[1]  = "���� �ɷ��� �����Ѵ�.";
        cardBossFirstDiglogs_Korean[2]  = "�ΰ��̿�, ���� �Բ� ������ �ϰڴ°�?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� �ʹٴ�";
        cardBossLeftChoiceDiglogs_Korean[1]  = "�ΰ��̶� ��¿�� ���� ��������";
        cardBossLeftChoiceDiglogs_Korean[2]  = "������ ��ȭ�� �̷�������ϴ�.";

        cardBossRightChoiceDiglogs_Korean[0]  = "������ �ϰ� ���� �ʴٴ�???";
        cardBossRightChoiceDiglogs_Korean[1] = "�� ���� ��ȸ�ϰ� ���ְڴ�.";
        cardBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ��ȭ�� �̷�������ϴ�.";

    }


    public abstract void TextDataUpdate(int value);


    //public void LanguageChange(int index)// ��� ����
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
