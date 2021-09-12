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

    [SerializeField] private GameObject blinkObject;


    #region ��ȭâ 
    [SerializeField] private GameObject diglogObject; // ��ȭâ ������Ʈ 
    [SerializeField] private TextMeshProUGUI diglogText; // ��ȭâ TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;
    [SerializeField] private DiglogData diglogData;
    private bool isChoice; // ������ �ߴ���?

    private string[] diglogDatas; // ��ȭâ ������ string
    private int diglogIndex = 0; // ��ȭâ �ε���
    private int choiceValue = 0; // ����, ������ ���ð� 
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
        diglogObject.SetActive(false);
    }

    private void GameObjectAllFind()
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
        GameObjectAllFind();
        yield return new WaitForSeconds(2.0f);
        diglogObject.SetActive(true);
        StartCoroutine(TextUpdate());
    }

    private IEnumerator TextUpdate()
    {
        diglogText.text = "";

        for (int i = 0; i < diglogDatas[diglogIndex].Length; i++)
        {
            diglogText.text += diglogDatas[diglogIndex][i];
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((diglogDatas.Length - 2 == diglogIndex) && isChoice == true) // ������ ���� ���� ī�带 ���ֱ� ���� if�� ����
                {
                    if(choiceValue == 0)
                        leftChoiceObject.GetComponent<ChoiceEvent>().Effect();
                    else
                        rightChoiceObject.GetComponent<ChoiceEvent>().Effect();
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

    public void TextDataSetUpdate(string[] textDatas) // �����ѰͿ� ���� �ؽ�Ʈ ������ �����ϱ� 
    {
        diglogDatas = new string[textDatas.Length]; // �޾ƿ� string�� �迭 ũ�⸸ŭ �ٲٱ�
        
        for(int i = 0; i < textDatas.Length; i++) // �ؽ�Ʈ������ �ֱ� 
        {
            diglogDatas[i] = textDatas[i];
        }
    }

    public void ChoiceSelect(int value) // Yes or No ���������� �޾ƿ��� 
    {
        choiceValue = value;

        if (choiceValue == 0)
        {
            rightChoiceObject.SetActive(false);
        }
        else
        {
            leftChoiceObject.SetActive(false);
        }
        diglogIndex = 0; // �ؽ�Ʈ ������ 0���� �ʱ�ȭ
        diglogData.TextDataUpdate(choiceValue);
        PlayerFreezeAndLastTalk();
        StartCoroutine(TextUpdate());
    }

}
