using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1System : MonoBehaviour
{
    [SerializeField] private CardBoss cardBoss;

    [SerializeField] private Player player;

    private GameObject[] enemyObjects; // �� ������Ʈ�� ���
    private GameObject[] playerObjects; // �÷��̾� ������Ʈ����

    [SerializeField] private GameObject blinkObject;

    private bool isChoice; // ������ ������? (���������̸� ���߱�)

    private void Start()
    {

    }


    public void PauseAndTalk() // ù��° - �����ϱ� ���� ��� ����
    {
        StartCoroutine("Choice");
    }

    public void PlayerChoice() // �ι�° - �÷��̾� ����
    {
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
    }

    private IEnumerator Choice()
    {
        player.Pause();
        blinkObject.SetActive(true);
        GameObjectAllFind();
        yield return new WaitForSeconds(2.0f);
    }
}
