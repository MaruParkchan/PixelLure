using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1System : MonoBehaviour
{
    [SerializeField]
    private CardBoss cardBoss;

    [SerializeField]
    private Player player;

    private GameObject[] enemyObjects; // 적 오브젝트들 담기
    private GameObject[] playerObjects; // 플레이어 오브젝트들담기

    private bool isChoice; // 선택지 중인지? (선택지중이면 멈추기)

    private void Start()
    {

    }


    public void PauseAndTalk() // 첫번째 - 선택하기 위한 모든 정지
    {
        player.Pause();
        GameObjectAllFind();
    }

    public void PlayerChoice() // 두번째 - 플레이어 선택
    {
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // 세번째 - 마지막 문구 출력 및 플레이어 이동 잠금
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
}
