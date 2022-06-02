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

            //DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_Korean);
            //DiglogChange(cardBossLeftChoiceDiglog, cardBossLeftChoiceDiglogs_Korean);
            //DiglogChange(dogBossFirstDiglog,  cardBossRightChoiceDiglogs_Korean);

            //DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_English);
            //DiglogChange(smokeBossFirstDiglog, smokeBossFirstDiglogs_English);
            //DiglogChange(dogBossFirstDiglog, dogBossFirstDiglogs_English);
            ////
            //DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_English);
            //DiglogChange(smokeBossFirstDiglog, smokeBossFirstDiglogs_English);
            //DiglogChange(dogBossFirstDiglog, dogBossFirstDiglogs_English);
        }
        else if(languageIndex == 1)
        {
           cardBossFirstDiglog = DiglogChange(cardBossFirstDiglog, cardBossFirstDiglogs_Korean);
           cardBossLeftChoiceDiglog = DiglogChange(cardBossLeftChoiceDiglog, cardBossLeftChoiceDiglogs_Korean);
           cardBossRightChoiceDiglog = DiglogChange(cardBossRightChoiceDiglog, cardBossRightChoiceDiglogs_Korean);
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
        cardBossFirstDiglogs_English[2] = "Would you gamble with me, man?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� �ͱ���";
        cardBossLeftChoiceDiglogs_Korean[1]  = "�ΰ��̶� ��¿�� ���� ��������!";
        cardBossLeftChoiceDiglogs_Korean[2]  = "�÷��̾��� ���°� ����Ǿ����ϴ�.";
        cardBossLeftChoiceDiglogs_English[0] = "You want to gamble with me!";
        cardBossLeftChoiceDiglogs_English[1] = "Humans are powerless to stop themselves!";
        cardBossLeftChoiceDiglogs_English[2] = "The status of the player has changed.";

        cardBossRightChoiceDiglogs_Korean[0]  = "������ �ϰ� ���� �ʴٴ�???";
        cardBossRightChoiceDiglogs_Korean[1] = "�� ���� ��ȸ�ϰ� ���ְڴ�.";
        cardBossRightChoiceDiglogs_Korean[2] = "������ ��ȭ�� �̷�������ϴ�.";
        cardBossRightChoiceDiglogs_English[0] = "Don't you want to gamble???";
        cardBossRightChoiceDiglogs_English[1] = "I'll make you regret your decision.";
        cardBossRightChoiceDiglogs_English[2] = "The status of Boss has been changed.";
    }

    private void SmokeBossDiglogInit()
    {
        smokeBossFirstDiglogs_Korean[0]      = "�������� ����ѵ�??!";
        smokeBossFirstDiglogs_Korean[1]      = "���� ������ ȭ���� ����� �ϰ� ó���̾�!";
        smokeBossFirstDiglogs_Korean[2]      = "�ʿ��� ���� ������ �ϰھ�!";
        smokeBossFirstDiglogs_Korean[3]      = "���� ��� ���⸦ ���ź���??"; // �̰��� ��ȭ - ����̶� ȸ�� < Yes , No ���ý� ��ȭ�� �ٲ����ҵ� > 

        smokeBossLeftChoiceDiglogs_Korean[0] = "����� �ڽ�~! ũũũ ";
        smokeBossLeftChoiceDiglogs_Korean[1] = "���� ���Ӽ��� �ɷ������~~ ����";
        smokeBossLeftChoiceDiglogs_Korean[2] = "������ ��ȭ�� �̷�������ϴ�.";

        smokeBossRightChoiceDiglogs_Korean[0] = "���� ���⸦ �ź���?!?!?";
        smokeBossRightChoiceDiglogs_Korean[1] = "������ ���� ȭ���� �ϴ±�! �ʸ� �׿��ְڴ�!";
        smokeBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ��ȭ�� �̷�������ϴ�.";
    }

    private void DogBossDiglogInit()
    {
        dogBossFirstDiglogs_Korean[0] = "��������....";
        dogBossFirstDiglogs_Korean[1] = "�Ӹ��� �ʹ� ����....";
        dogBossFirstDiglogs_Korean[2] = "�� ���� ���ǻ��~~~";
        dogBossFirstDiglogs_Korean[3] = "�� ���� ���� �� ���Ƿ�?"; // �̰��� ��ȭ - ����̶� ȸ�� < Yes , No ���ý� ��ȭ�� �ٲ����ҵ� > 

        dogBossLeftChoiceDiglogs_Korean[0] = "ũ��������~~~~ ";
        dogBossLeftChoiceDiglogs_Korean[1] = "���� ������ ��������~~!!";
        dogBossLeftChoiceDiglogs_Korean[2] = "������ ��ȭ�� �̷�������ϴ�.";

        dogBossRightChoiceDiglogs_Korean[0] = "���� �� ���ñ� �ȴٰ�???!";
        dogBossRightChoiceDiglogs_Korean[1] = "����������!!!!!�ƾƾ�!!!!!!";
        dogBossRightChoiceDiglogs_Korean[2] = "�÷��̾��� ��ȭ�� �̷�������ϴ�.";
    }

    public abstract void TextFistInitUpdate();
    public abstract void TextDataUpdate(bool isAccept);

}
