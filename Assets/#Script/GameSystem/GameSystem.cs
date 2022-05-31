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
    private bool isChoice; // ������ �ߴ���?

    private string[] diglogDatas; // ��ȭâ ������ string
    private string diglog; // ��ȭâ����� string
    private int diglogIndex = 0; // ��ȭâ �ε���
    private bool isTyping = false; // Ÿ����������
    private bool isAccept = false; // ����, ������ ���ð� 
    #endregion

    private void Start()
    {
        diglogText.text = "";
    }

    public void PauseAndTalk() // ù��° - �����ϱ� ���� ��� ����
    {
        StartCoroutine("Choice"); // ��ȭâ ������Ʈ �ѱ�
    }

    public void PlayerChoice() // �ι�° - �÷��̾� ����
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        // diglogObject.SetActive(false); // ��ȭâ ������Ʈ ����
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // ����° - ������ ���� ��� �� �÷��̾� �̵� ���
    {
        isChoice = true;
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
        StartCoroutine(TextUpdate());
    }

    private bool DiglogTyping()
    {
        return true;
    }

    private IEnumerator TextUpdate()
    {
        diglogText.text = "";
        diglog = diglogDatas[diglogIndex];

        for (int i = 0; i < diglogDatas.Length; i++)
        {
            diglogText.text += diglog[i];
            yield return new WaitForSeconds(0.07f);
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if ((diglogDatas.Length - 1 == diglogIndex) && isChoice == true) // ������ ���� ���� ī�带 ���ֱ� ���� if�� ����
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

                if (diglogDatas.Length - 1 <= diglogIndex)
                {
                    if (isChoice == false)
                        PlayerChoice();
                    else
                        ResumeGame();

                    yield break;
                }

                diglogIndex++;
                StartCoroutine(TextUpdate());
                yield break;
            }
            yield return null;
        }
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
        diglogIndex = 0; // �ؽ�Ʈ ������ 0���� �ʱ�ȭ
        diglogData.TextDataUpdate(isAccept);
        StartCoroutine(TextUpdate());
        PlayerFreezeAndLastTalk();
    }

}
