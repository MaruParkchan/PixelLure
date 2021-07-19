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
        DogDiglog();
    }

    private string[] cardBossDiglogs_English = new string[3];
    private string[] cardBossDiglogs_Korean = new string[3];

    //private string[] smokeBossDiglogs_English;
    //private string[] smokeBossDiglogs_Korean;

    //private string[] dogBossDiglogs_English;
    //private string[] dogBossDiglogs_Korean;

    public string[] cardBossDiglogs = new string[3];


    private void DogDiglog()
    {
        cardBossDiglogs_English[0] = "That's great...";
        cardBossDiglogs_Korean[0]  = "�Ǹ��ϱ���...";

        cardBossDiglogs_English[1] = "I test your abilities.";
        cardBossDiglogs_Korean[1]  = "���� �ɷ��� �����Ѵ�.";

        cardBossDiglogs_English[2] = "Choose, human beings.";
        cardBossDiglogs_Korean[2] = "�ΰ��̿�, �����ض�.";
    }

    public void LanguageChange(int index)// ��� ����
    {       
        for(int i = 0; i < cardBossDiglogs.Length; i++)
        {
            if(index == 0)
            {
                cardBossDiglogs[i] = cardBossDiglogs_English[i];
            }
            else if (index == 1)
            {
                cardBossDiglogs[i] = cardBossDiglogs_Korean[i];
            }
        }
    }

}
