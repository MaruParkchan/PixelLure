using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LauguageSystem { English, Korean}

public class DiglogData : MonoBehaviour
{
    private static DiglogData instance;
    public static DiglogData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<DiglogData>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DogDiglogInit();
        LanguageChange(PlayerPrefs.GetInt("LanguageData"));
    }

    private string[] cardBossFirstDiglogs_English        = new string[3];
    private string[] cardBossFirstDiglogs_Korean         = new string[3];
    private string[] cardBossLeftChoiceDiglogs_English   = new string[3];
    private string[] cardBossLeftChoiceDiglogs_Korean    = new string[3];
    private string[] cardBossRightChoiceDiglogs_English  = new string[3];
    private string[] cardBossRightChoiceDiglogs_Korean   = new string[3];

    //private string[] smokeBossDiglogs_English;
    //private string[] smokeBossDiglogs_Korean;

    //private string[] dogBossDiglogs_English;
    //private string[] dogBossDiglogs_Korean;

    public string[] cardBossFitstDiglogs = new string[3];


    private void DogDiglogInit() // ��ȭ ������ �ʱ�ȭ
    {
        cardBossFirstDiglogs_English[0] = "That's great...";
        cardBossFirstDiglogs_Korean[0]  = "�Ǹ��ϱ���...";

        cardBossFirstDiglogs_English[1] = "I test your abilities.";
        cardBossFirstDiglogs_Korean[1]  = "���� �ɷ��� �����Ѵ�.";

        cardBossFirstDiglogs_English[2] = "Choose, human beings.";
        cardBossFirstDiglogs_Korean[2]  = "�ΰ��̿�, ���� �Բ� ������ �ϰڴ°�?";

        cardBossLeftChoiceDiglogs_English[0] = "You want to gamble with me";
        cardBossLeftChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� �ʹٴ�";

        cardBossLeftChoiceDiglogs_English[1] = "Human beings can't be helped";
        cardBossLeftChoiceDiglogs_Korean[1]  = "�ΰ��̶� ��¿�� ���� ��������";

        cardBossLeftChoiceDiglogs_English[2] = "A change in the boss has been made.";
        cardBossLeftChoiceDiglogs_Korean[2]  = "������ ��ȭ�� �̷�������ϴ�.";

        cardBossRightChoiceDiglogs_English[0] = "You don't want to gamble with me.";
        cardBossRightChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� ���� �ʴٴ�.";

        cardBossRightChoiceDiglogs_English[1] = "I'll make you regret that choice.";
        cardBossRightChoiceDiglogs_Korean[1] = "�� ���� ��ȸ�ϰ� ���ְڴ�.";

        cardBossRightChoiceDiglogs_English[2] = "Player changes have been made";
        cardBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ��ȭ�� �̷�������ϴ�.";
    }

    public void LanguageChange(int index)// ��� ����
    {
        PlayerPrefs.SetInt("LanguageData", index);

        for(int i = 0; i < cardBossFitstDiglogs.Length; i++)
        {
            if(index == 0)
            {
                cardBossFitstDiglogs[i] = cardBossFirstDiglogs_English[i];
            }
            else if (index == 1)
            {
                cardBossFitstDiglogs[i] = cardBossFirstDiglogs_Korean[i];
            }
        }
    }

}
