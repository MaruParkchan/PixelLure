using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Player player;

    private GameObject[] enemyObjects; // �� ������Ʈ�� ���
    private GameObject[] playerObjects; // �÷��̾� ������Ʈ����

    [SerializeField] private HpUIsystem hpUIsystem;
    [SerializeField] private GameObject blinkObject;

    #region ��ȭâ 
    [SerializeField] private GameObject diglogObject; // ��ȭâ ������Ʈ 
    [SerializeField] private TextMeshProUGUI diglogText; // ��ȭâ TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;
    [SerializeField] private DiglogData diglogData;
    [SerializeField] private GameObject cursorArrowObject;
    private bool isChoice; // ������ �ߴ���?
    private bool isSeceleted = false; // �����Ͽ���

    private string[] diglogDatas; // ��ȭâ ������ string
    private string diglog = ""; // ��ȭâ����� string
    private int diglogIndex = 0; // ��ȭâ �ε���
    private float typingSpeed = 0.11f;
    private bool isFirst = true; 
    private bool isTyping = false; // Ÿ����������
    private bool isAccept = false; // ����, ������ ���ð� 
    #endregion

    private void Start()
    {
        diglogText.text = "";
        diglogData.TextFistInitUpdate();
    }

    public void PauseAndTalk() // ù��° - �����ϱ� ���� ��� ����
    {
        StartCoroutine("Choice"); // ��ȭâ ������Ʈ �ѱ�
    }

    public void PlayerChoice() // �ι�° - �÷��̾� ����
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        //diglogObject.SetActive(false); // ��ȭâ ������Ʈ ����
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // ����° - ������ ���� ��� �� �÷��̾� �̵� ���
    {
        isChoice = true;
        isSeceleted = true;
        player.Wait();
    }

    public void ResumeGame() // �׹�° - ���ÿϷ� �� ���� ����� 
    {
        player.Resume();
        boss.GetComponent<IPause>().Resume();
        hpUIsystem.SliderMaxValueUpdate(boss.GetComponent<BossHp>().GetSecondHp()); // ���� ������2 HP ��ȯ 
        diglogObject.SetActive(false);
    }

    private void GameObjectAllFind() // �÷��̾�, ���� �Ѿ� ������Ʈ ��ã�Ƽ� ������Ŵ
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("EnemyBullet");
        playerObjects = GameObject.FindGameObjectsWithTag("PlayerBullet");

        for (int i = 0; i < enemyObjects.Length; i++)
        {
            Destroy(enemyObjects[i]);
        }
        for (int i = 0; i < playerObjects.Length; i++)
        {
            Destroy(playerObjects[i]);
        }
    } // ������Ʈ �� ���� (�Ѿ�)

    private IEnumerator Choice()
    {
        player.Pause();
        blinkObject.SetActive(true);
        //GameObjectAllFind(); // �ָ� ��Ȱ��ȭ�ϸ� ���ý� �Ѿ��� �Ȼ����
        yield return new WaitForSeconds(2.0f);
        diglogObject.SetActive(true);
        StartCoroutine(DiglogUpdate());
    }

    private IEnumerator DiglogUpdate()
    {
        yield return new WaitUntil(() => FirstBossDiglogUpdate());
        yield return new WaitUntil(() => isChoice);
        yield return new WaitUntil(() => SecondBossDiglogUpdate());
    }

    private bool FirstBossDiglogUpdate()
    {
        if( isFirst == true) // ���� ��� ����
        {
            diglogIndex = 0; // �ؽ�Ʈ ������ 0���� �ʱ�ȭ
            StartCoroutine("TypingText");
            isFirst = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(isTyping == true)
            {
                isTyping = false;

                StopCoroutine("TypingText");
                cursorArrowObject.SetActive(true);
                diglogText.text = diglog;

                return false;
            }


            if(diglogDatas.Length - 1 > diglogIndex)
            {
                SetNextDiglog();
            }
            else
            {
                PlayerChoice();
                isFirst = true;
                return true;
            }
        }
        return false;
    }

    private bool SecondBossDiglogUpdate()
    {
        if (isFirst == true) // ���� ��� ����
        {
            diglogIndex = 0; // �ؽ�Ʈ ������ 0���� �ʱ�ȭ
            StartCoroutine("TypingText");
            cursorArrowObject.SetActive(false);
            isFirst = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isTyping == true)
            {
                isTyping = false;

                StopCoroutine("TypingText");
                diglogText.text = diglog;
                cursorArrowObject.SetActive(true);
                return false;
            }

            if ((diglogDatas.Length - 2 == diglogIndex) && isChoice == true) // ������ ���� ���� ī�带 ���ֱ� ���� if�� ����
            {
                if (isAccept == true)
                {
                    ChoiceCard(leftChoiceObject, Color.red);
                }
                else
                {
                    ChoiceCard(rightChoiceObject, Color.blue);
                }
            }

            if (diglogDatas.Length - 1 > diglogIndex)
            {
                SetNextDiglog();
            }
            else
            {
                ResumeGame();
                return true;
            }
        }
        return false;
    }


    private void SetNextDiglog()
    {
        diglogIndex++; // ���� ��� ���� 
        cursorArrowObject.SetActive(false);
        StartCoroutine("TypingText");
    }

    private IEnumerator TypingText()
    {
        int index = 0;
        diglogText.text = "";
        diglog = diglogDatas[diglogIndex];
        isTyping = true;
        while(index < diglog.Length)
        {
            diglogText.text += diglog[index];
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        cursorArrowObject.SetActive(true);
        yield return null;
    }

    private void ChoiceCard(GameObject choiceObject, Color color)
    {
        choiceObject.GetComponent<ChoiceEvent>().Effect();
        diglogText.color = color;
    }

    public void TextDataSetUpdate(string[] textDatas) // �����ѰͿ� ���� �ؽ�Ʈ ������ �����ϱ� 
    {
        diglogDatas = new string[textDatas.Length]; // �޾ƿ� string�� �迭 ũ�⸸ŭ �ٲٱ�
        
        for(int i = 0; i < textDatas.Length; i++) // �ؽ�Ʈ������ �ֱ� 
        {
            diglogDatas[i] = textDatas[i];
        }
    }

    public void ChoiceSelect(bool isAccept) // Yes or No ���������� �޾ƿ��� 
    {
        this.isAccept = isAccept;
        if (isAccept == true)
        {
            rightChoiceObject.SetActive(false); // Yes�̱⶧���� ���� ����� ������ ����
        }
        else
        {
            leftChoiceObject.SetActive(false);
        }

        diglogData.TextDataUpdate(isAccept);
        PlayerFreezeAndLastTalk();
       // StartCoroutine(TextUpdate());
    }
}
