using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language { English, Korean}

public abstract class DiglogData : MonoBehaviour
{
    [SerializeField] private GameSystem gameSystem;
    protected string[] bossDiglog;
    protected Language lauguage;

    protected string[] cardBossFirstDiglog          = new string[3];
    protected string[] cardBossLeftChoiceDiglog     = new string[3];
    protected string[] cardBossRightChoiceDiglog    = new string[3];

    protected string[] smokeBossFirstDiglog         = new string[4];
    protected string[] smokeBossLeftChoiceDiglog    = new string[3];
    protected string[] smokeBossRightChoiceDiglog   = new string[3];

    protected string[] dogBossFirstDiglog           = new string[4];
    protected string[] dogBossLeftChoiceDiglog      = new string[3];
    protected string[] dogBossRightChoiceDiglog     = new string[3];

    //CardBoss English
    protected string[] cardBossFirstDiglogs_English        = new string[3];
    protected string[] cardBossLeftChoiceDiglogs_English   = new string[3];
    protected string[] cardBossRightChoiceDiglogs_English  = new string[3];
    //CardBoss Korean
    protected string[] cardBossFirstDiglogs_Korean         = new string[3];
    protected string[] cardBossLeftChoiceDiglogs_Korean    = new string[3];
    protected string[] cardBossRightChoiceDiglogs_Korean   = new string[3];

    //SmokeBoss English
    protected string[] smokeBossFirstDiglogs_Korean        = new string[4];
    protected string[] smokeBossLeftChoiceDiglogs_Korean   = new string[3];
    protected string[] smokeBossRightChoiceDiglogs_Korean  = new string[3];
    //SmokeBoss Korean
    protected string[] smokeBossFirstDiglogs_English       = new string[4];
    protected string[] smokeBossLeftChoiceDiglogs_English  = new string[3];
    protected string[] smokeBossRightChoiceDiglogs_English = new string[3];

    //DogBoss English
    protected string[] dogBossFirstDiglogs_Korean          = new string[4];
    protected string[] dogBossLeftChoiceDiglogs_Korean     = new string[3];
    protected string[] dogBossRightChoiceDiglogs_Korean    = new string[3];
    //DogBoss Korean
    protected string[] dogBossFirstDiglogs_English         = new string[4];
    protected string[] dogBossLeftChoiceDiglogs_English    = new string[3];
    protected string[] dogBossRightChoiceDiglogs_English   = new string[3];

    //private string[] smokeBossDiglogs_Korean;
    //private string[] dogBossDiglogs_Korean;

    private void Awake()
    {
        CardBossDiglogInit();
        SmokeBossDiglogInit();
        DogBossDiglogInit();
        ChanageLauguage();
    }

    public void ChanageLauguage()
    {
        int languageIndex = PlayerPrefs.GetInt("LanguageIndex");

        if(languageIndex == 0)
        {
            cardBossFirstDiglog       = DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_English);
            cardBossLeftChoiceDiglog  = DiglogChange(cardBossLeftChoiceDiglog, cardBossLeftChoiceDiglogs_English);
            cardBossRightChoiceDiglog = DiglogChange(cardBossRightChoiceDiglog, cardBossRightChoiceDiglogs_English);
            smokeBossFirstDiglog       = DiglogChange(smokeBossFirstDiglog, smokeBossFirstDiglogs_English);
            smokeBossLeftChoiceDiglog  = DiglogChange(smokeBossLeftChoiceDiglog, smokeBossLeftChoiceDiglogs_English);
            smokeBossRightChoiceDiglog = DiglogChange(smokeBossRightChoiceDiglog, smokeBossRightChoiceDiglogs_English);
            dogBossFirstDiglog = DiglogChange(dogBossFirstDiglog, dogBossFirstDiglogs_English);
            dogBossLeftChoiceDiglog = DiglogChange(dogBossLeftChoiceDiglog, dogBossLeftChoiceDiglogs_English);
            dogBossRightChoiceDiglog = DiglogChange(dogBossRightChoiceDiglog, dogBossRightChoiceDiglogs_English);
        }
        else if(languageIndex == 1)
        {
           cardBossFirstDiglog        = DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_Korean);
           cardBossLeftChoiceDiglog   = DiglogChange(cardBossLeftChoiceDiglog, cardBossLeftChoiceDiglogs_Korean);
           cardBossRightChoiceDiglog  = DiglogChange(cardBossRightChoiceDiglog, cardBossRightChoiceDiglogs_Korean);
           smokeBossFirstDiglog       = DiglogChange(smokeBossFirstDiglog, smokeBossFirstDiglogs_Korean);
           smokeBossLeftChoiceDiglog  = DiglogChange(smokeBossLeftChoiceDiglog, smokeBossLeftChoiceDiglogs_Korean);
           smokeBossRightChoiceDiglog = DiglogChange(smokeBossRightChoiceDiglog, smokeBossRightChoiceDiglogs_Korean);
           dogBossFirstDiglog         = DiglogChange(dogBossFirstDiglog, dogBossFirstDiglogs_Korean);
           dogBossLeftChoiceDiglog    = DiglogChange(dogBossLeftChoiceDiglog, dogBossLeftChoiceDiglogs_Korean);
           dogBossRightChoiceDiglog   = DiglogChange(dogBossRightChoiceDiglog, dogBossRightChoiceDiglogs_Korean);

        }
    }

    private string[] DiglogChange(string[] bossDiglogs, string[] diglogsData) // ��ȭ ������ �ٲ��ֱ�
    {
        bossDiglogs = new string[diglogsData.Length];

        for (int i = 0; i < diglogsData.Length; i++)
            bossDiglogs[i] = diglogsData[i];

        return bossDiglogs;
    }

    protected void DiglogGameSystemTextUpdate(string[] diglogs)
    {
        gameSystem.TextDataSetUpdate(diglogs);
    }

    private void CardBossDiglogInit() // ��ȭ ������ �ʱ�ȭ
    {
        cardBossFirstDiglogs_Korean[0]  = "�Ǹ��ϱ���...";
        cardBossFirstDiglogs_Korean[1]  = "���� �ɷ��� �����Ѵ�.";
        cardBossFirstDiglogs_Korean[2]  = "�ΰ��̿�, ���� �Բ� ������ �ϰڴ°�?";
        cardBossFirstDiglogs_English[0] = "That's fantastic...";
        cardBossFirstDiglogs_English[1] = "I put you to the test.";
        cardBossFirstDiglogs_English[2] = "Would you gamble with me, player?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� �ͱ���";
        cardBossLeftChoiceDiglogs_Korean[1]  = "�ΰ��̶� ��¿�� ���� ��������!";
        cardBossLeftChoiceDiglogs_Korean[2]  = "������ ���°� ����Ǿ����ϴ�.";
        cardBossLeftChoiceDiglogs_English[0] = "You want to gamble with me!";
        cardBossLeftChoiceDiglogs_English[1] = "Humans are powerless to stop \nthemselves!";
        cardBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed.";

        cardBossRightChoiceDiglogs_Korean[0]  = "������ �ϰ� ���� �ʴٴ�???";
        cardBossRightChoiceDiglogs_Korean[1] = "�� ���� ��ȸ�ϰ� ���ְڴ�.";
        cardBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ���°� ����Ǿ����ϴ�.";
        cardBossRightChoiceDiglogs_English[0] = "Don't you want to gamble???";
        cardBossRightChoiceDiglogs_English[1] = "I'll make you regret your decision.";
        cardBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";
    }

    private void SmokeBossDiglogInit()
    {
        smokeBossFirstDiglogs_Korean[0]  = "Ǫ��Ǫ����~! �����! ���� �����!!";
        smokeBossFirstDiglogs_Korean[1]  = "���� ������ ȭ���� ����� \n�ϰ� ó���̶�~";
        smokeBossFirstDiglogs_Korean[2]  = "�ʿ��� ���� ������ �ϰھ�!";
        smokeBossFirstDiglogs_Korean[3]  = "���� �ʿ��� ��踦 ���״�!\n���� ���� ��� ��淡?";
        smokeBossFirstDiglogs_English[0] = "Hahahahaha~! That is incredible!\nYou are truly amazing!!";
        smokeBossFirstDiglogs_English[1] = "You're the first person\nto irritate me~";
        smokeBossFirstDiglogs_English[2] = "I'll make a good suggestion!";
        smokeBossFirstDiglogs_English[3] = "I'll give you one of my cigarettes!\nWould you like to smoke with me?";

        smokeBossLeftChoiceDiglogs_Korean[0] = "����� �ڽ�~! Ǫ��Ǫ����~!";
        smokeBossLeftChoiceDiglogs_Korean[1] = "���� ���Ӽ��� �ɷ������~~\n������ ���� ����� ����!";
        smokeBossLeftChoiceDiglogs_Korean[2] = "������ ���°� ����Ǿ����ϴ�.";
        smokeBossLeftChoiceDiglogs_English[0] = "You moron~! Hahahaha~!";
        smokeBossLeftChoiceDiglogs_English[1] = "You fell for my ruse~~\nLook at how much stronger I'm becoming!";
        smokeBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed.";

        smokeBossRightChoiceDiglogs_Korean[0] = "���� ��踦 �ź���??!!\n�� �ǹ�������?";
        smokeBossRightChoiceDiglogs_Korean[1] = "������ ���� ȭ���� �ϴ±�!\n�ʸ� �׿��ְڴ�!";
        smokeBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ���°� ����Ǿ����ϴ�.";
        smokeBossRightChoiceDiglogs_English[0] = "Do you refuse to take my cigarettes???\nYou're conceited?";
        smokeBossRightChoiceDiglogs_English[1] = "You really irritate me!\nI'm going to kill you!";
        smokeBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";

    }

    private void DogBossDiglogInit()
    {
        dogBossFirstDiglogs_Korean[0] = "��������........";
        dogBossFirstDiglogs_Korean[1] = "�ܷο�...\n���� �� ���ǻ������?";
        dogBossFirstDiglogs_Korean[2] = "������... �ܷο�� �Ⱦ�...";
        dogBossFirstDiglogs_Korean[3] = "�ű� ��! ���� ���� �� ���Ƿ�?"; // �̰��� ��ȭ - ����̶� ȸ�� < Yes , No ���ý� ��ȭ�� �ٲ����ҵ� > 
        dogBossFirstDiglogs_English[0] = "ughhhhh........";
        dogBossFirstDiglogs_English[1] = "Lonely...\nIs there anyone who wants\nto join me for a drink?";
        dogBossFirstDiglogs_English[2] = "Ugh...\nI despise being alone...";
        dogBossFirstDiglogs_English[3] = "You're there!\nDo you want to join me for a drink?";

        dogBossLeftChoiceDiglogs_Korean[0] = "���� �����ִ°ž�??";
        dogBossLeftChoiceDiglogs_Korean[1] = "���� ������ ��������~~!!\n�� ����� �ʹ� ��������!!";
        dogBossLeftChoiceDiglogs_Korean[2] = "������ ���°� ����Ǿ����ϴ�.";
        dogBossLeftChoiceDiglogs_English[0] = "Are you drinking alongside me??";
        dogBossLeftChoiceDiglogs_English[1] = "Alcohol is always delicious~~!!\nI'm in a great mood!!";
        dogBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed";

        dogBossRightChoiceDiglogs_Korean[0] = "���� �� ���ñ� �ȴٰ�???!";
        dogBossRightChoiceDiglogs_Korean[1] = "����������!!!!!�ƾƾ�!!!!!!\n�ʹ� �ܷο�!!!!!";
        dogBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ���°� ����Ǿ����ϴ�.";
        dogBossRightChoiceDiglogs_English[0] = "You don't want to have a drink with me???!";
        dogBossRightChoiceDiglogs_English[1] = "Ugh!!!!! Oh, my!!!!!!\nI'm so alone!!!!!";
        dogBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";

    }

    public abstract void TextFistInitUpdate();
    public abstract void TextDataUpdate(bool isAccept);

}
