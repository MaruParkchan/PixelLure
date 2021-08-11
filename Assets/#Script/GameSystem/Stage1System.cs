using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage1System : MonoBehaviour
{
    [SerializeField] private CardBoss cardBoss;

    [SerializeField] private Player player;

    private GameObject[] enemyObjects; // �� ������Ʈ�� ���
    private GameObject[] playerObjects; // �÷��̾� ������Ʈ����

    [SerializeField] private GameObject blinkObject;

    private bool isChoice; // ������ ������? (���������̸� ���߱�)

    #region ��ȭâ 
    [SerializeField] private GameObject diglogObject; // ��ȭâ ������Ʈ 
    [SerializeField] private TextMeshProUGUI diglogText; // ��ȭâ TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;

    private string[] diglogData = new string[3]; // ��ȭâ ������ string
    private int diglogIndex = 0; // ��ȭâ �ε���
    #endregion

    private void Start()
    {
        DiglogDataGet();
    }

    public void PauseAndTalk() // ù��° - �����ϱ� ���� ��� ����
    {
        StartCoroutine("Choice"); // ��ȭâ ������Ʈ �ѱ�
    }

    public void PlayerChoice() // �ι�° - �÷��̾� ����
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        diglogObject.SetActive(false); // ��ȭâ ������Ʈ ����
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // ����° - ������ ���� ��� �� �÷��̾� �̵� ���
    {
        player.Wait();
    }

    public void ResumeGame()
    {
        player.Resume();
        cardBoss.Resume();
    }

    private void GameObjectAllFind()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("EnemyBullet");
        playerObjects = GameObject.FindGameObjectsWithTag("PlayerBullet");

        for(int i = 0; i < enemyObjects.Length; i++)
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
        StartCoroutine(TextUpdate(diglogIndex));
    }

    private void DiglogDataGet()
    {
        for(int i = 0; i < DiglogData.Instance.cardBossFitstDiglogs.Length; i++)
        {
            diglogData[i] = DiglogData.Instance.cardBossFitstDiglogs[i];
        }
    }

    private IEnumerator TextUpdate(int index)
    {
        diglogText.text = "";

        if(diglogData.Length -1 <= index) // ù��° ��Ʈ ������ �����϶� ������ �߱� 
        {
            leftChoiceObject.SetActive(true);
            rightChoiceObject.SetActive(true);
        }

        for (int i = 0; i < diglogData[index].Length; i++)
        {
            diglogText.text += diglogData[index][i];
            yield return new WaitForSeconds(0.1f);
        }
         
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (diglogData.Length -1 <= diglogIndex)
                {
                    PlayerChoice(); 
                    yield break;
                }
                diglogIndex++;
                StartCoroutine(TextUpdate(diglogIndex));
                yield break;
            }
            yield return null;
        }
    }

}
