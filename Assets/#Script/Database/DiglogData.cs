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

    protected string[] dogBossFirstDiglogs_Korean = new string[4];
    protected string[] dogBossLeftChoiceDiglogs_Korean = new string[3];
    protected string[] dogBossRightChoiceDiglogs_Korean = new string[3];

    //private string[] smokeBossDiglogs_Korean;
    //private string[] dogBossDiglogs_Korean;

    private void Awake()
    {
        CardBossDiglogInit();
        SmokeBossDiglogInit();
        DogBossDiglogInit();
    }

    private void CardBossDiglogInit() // ��ȭ ������ �ʱ�ȭ
    {
        cardBossFirstDiglogs_Korean[0]  = "�Ǹ��ϱ���...";
        cardBossFirstDiglogs_Korean[1]  = "���� �ɷ��� �����Ѵ�.";
        cardBossFirstDiglogs_Korean[2]  = "�ΰ��̿�, ���� �Բ� ������ �ϰڴ°�?";

        cardBossLeftChoiceDiglogs_Korean[0]  = "���� �Բ� ������ �ϰ� �ʹٴ�";
        cardBossLeftChoiceDiglogs_Korean[1]  = "�ΰ��̶� ��¿�� ���� ��������";
        cardBossLeftChoiceDiglogs_Korean[2]  = "�÷��̾��� ��ȭ�� �̷�������ϴ�.";

        cardBossRightChoiceDiglogs_Korean[0]  = "������ �ϰ� ���� �ʴٴ�???";
        cardBossRightChoiceDiglogs_Korean[1] = "�� ���� ��ȸ�ϰ� ���ְڴ�.";
        cardBossRightChoiceDiglogs_Korean[2] = "������ ��ȭ�� �̷�������ϴ�.";
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

    public abstract void TextDataUpdate(int value);

}
