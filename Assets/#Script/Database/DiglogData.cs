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

    private string[] DiglogChange(string[] bossDiglogs, string[] diglogsData) // 대화 데이터 바꿔주기
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

    private void CardBossDiglogInit() // 대화 데이터 초기화
    {
        cardBossFirstDiglogs_Korean[0]  = "훌륭하구나...";
        cardBossFirstDiglogs_Korean[1]  = "너의 능력을 시험한다.";
        cardBossFirstDiglogs_Korean[2]  = "인간이여, 나와 함께 도박을 하겠는가?";
        cardBossFirstDiglogs_English[0] = "That's fantastic...";
        cardBossFirstDiglogs_English[1] = "I put you to the test.";
        cardBossFirstDiglogs_English[2] = "Would you gamble with me, player?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "나와 함께 도박을 하고 싶구나";
        cardBossLeftChoiceDiglogs_Korean[1]  = "인간이란 어쩔수 없는 존재이지!";
        cardBossLeftChoiceDiglogs_Korean[2]  = "보스의 상태가 변경되었습니다.";
        cardBossLeftChoiceDiglogs_English[0] = "You want to gamble with me!";
        cardBossLeftChoiceDiglogs_English[1] = "Humans are powerless to stop \nthemselves!";
        cardBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed.";

        cardBossRightChoiceDiglogs_Korean[0]  = "도박을 하고 싶지 않다니???";
        cardBossRightChoiceDiglogs_Korean[1] = "그 선택 후회하게 해주겠다.";
        cardBossRightChoiceDiglogs_Korean[2] = "플레이어의 상태가 변경되었습니다.";
        cardBossRightChoiceDiglogs_English[0] = "Don't you want to gamble???";
        cardBossRightChoiceDiglogs_English[1] = "I'll make you regret your decision.";
        cardBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";
    }

    private void SmokeBossDiglogInit()
    {
        smokeBossFirstDiglogs_Korean[0]  = "푸하푸하하~! 대단해! 정말 대단해!!";
        smokeBossFirstDiglogs_Korean[1]  = "나를 정말로 화나게 만든건 \n니가 처음이라구~";
        smokeBossFirstDiglogs_Korean[2]  = "너에게 좋은 제안을 하겠어!";
        smokeBossFirstDiglogs_Korean[3]  = "내가 너에게 담배를 줄테니!\n나랑 같이 담배 즐길래?";
        smokeBossFirstDiglogs_English[0] = "Hahahahaha~! That is incredible!\nYou are truly amazing!!";
        smokeBossFirstDiglogs_English[1] = "You're the first person\nto irritate me~";
        smokeBossFirstDiglogs_English[2] = "I'll make a good suggestion!";
        smokeBossFirstDiglogs_English[3] = "I'll give you one of my cigarettes!\nWould you like to smoke with me?";

        smokeBossLeftChoiceDiglogs_Korean[0] = "어리석은 자식~! 푸하푸하하~!";
        smokeBossLeftChoiceDiglogs_Korean[1] = "나의 속임수에 걸려들었어~~\n강해진 나의 모습을 보라구!";
        smokeBossLeftChoiceDiglogs_Korean[2] = "보스의 상태가 변경되었습니다.";
        smokeBossLeftChoiceDiglogs_English[0] = "You moron~! Hahahaha~!";
        smokeBossLeftChoiceDiglogs_English[1] = "You fell for my ruse~~\nLook at how much stronger I'm becoming!";
        smokeBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed.";

        smokeBossRightChoiceDiglogs_Korean[0] = "나의 담배를 거부해??!!\n너 건방지구나?";
        smokeBossRightChoiceDiglogs_Korean[1] = "정말로 나를 화나게 하는군!\n너를 죽여주겠다!";
        smokeBossRightChoiceDiglogs_Korean[2] = "플레이어의 상태가 변경되었습니다.";
        smokeBossRightChoiceDiglogs_English[0] = "Do you refuse to take my cigarettes???\nYou're conceited?";
        smokeBossRightChoiceDiglogs_English[1] = "You really irritate me!\nI'm going to kill you!";
        smokeBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";

    }

    private void DogBossDiglogInit()
    {
        dogBossFirstDiglogs_Korean[0] = "으으으으........";
        dogBossFirstDiglogs_Korean[1] = "외로워...\n나랑 술 마실사람없나?";
        dogBossFirstDiglogs_Korean[2] = "으으으... 외로운건 싫어...";
        dogBossFirstDiglogs_Korean[3] = "거기 너! 나랑 같이 술 마실래?"; // 이거의 변화 - 희원이랑 회의 < Yes , No 선택시 변화가 바뀌어야할듯 > 
        dogBossFirstDiglogs_English[0] = "ughhhhh........";
        dogBossFirstDiglogs_English[1] = "Lonely...\nIs there anyone who wants\nto join me for a drink?";
        dogBossFirstDiglogs_English[2] = "Ugh...\nI despise being alone...";
        dogBossFirstDiglogs_English[3] = "You're there!\nDo you want to join me for a drink?";

        dogBossLeftChoiceDiglogs_Korean[0] = "나랑 마셔주는거야??";
        dogBossLeftChoiceDiglogs_Korean[1] = "술은 언제나 달콤하지~~!!\n나 기분이 너무 좋아졌어!!";
        dogBossLeftChoiceDiglogs_Korean[2] = "보스의 상태가 변경되었습니다.";
        dogBossLeftChoiceDiglogs_English[0] = "Are you drinking alongside me??";
        dogBossLeftChoiceDiglogs_English[1] = "Alcohol is always delicious~~!!\nI'm in a great mood!!";
        dogBossLeftChoiceDiglogs_English[2] = "The status of Boss has been changed";

        dogBossRightChoiceDiglogs_Korean[0] = "나랑 술 마시기 싫다고???!";
        dogBossRightChoiceDiglogs_Korean[1] = "으으으으으!!!!!아아아!!!!!!\n너무 외로워!!!!!";
        dogBossRightChoiceDiglogs_Korean[2] = "플레이어의 상태가 변경되었습니다.";
        dogBossRightChoiceDiglogs_English[0] = "You don't want to have a drink with me???!";
        dogBossRightChoiceDiglogs_English[1] = "Ugh!!!!! Oh, my!!!!!!\nI'm so alone!!!!!";
        dogBossRightChoiceDiglogs_English[2] = "The status of the Player has changed.";

    }

    public abstract void TextFistInitUpdate();
    public abstract void TextDataUpdate(bool isAccept);

}
