using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage1System : MonoBehaviour
{
    [SerializeField] private CardBoss cardBoss;

    [SerializeField] private Player player;

    private GameObject[] enemyObjects; // 적 오브젝트들 담기
    private GameObject[] playerObjects; // 플레이어 오브젝트들담기

    [SerializeField] private GameObject blinkObject;

    private bool isChoice; // 선택지 중인지? (선택지중이면 멈추기)

    #region 대화창 
    [SerializeField] private GameObject diglogObject; // 대화창 오브젝트 
    [SerializeField] private TextMeshProUGUI diglogText; // 대화창 TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;

    private string[] diglogData = new string[3]; // 대화창 데이터 string
    private int diglogIndex = 0; // 대화창 인덱스
    #endregion

    private void Start()
    {
        DiglogDataGet();
    }

    public void PauseAndTalk() // 첫번째 - 선택하기 위한 모든 정지
    {
        StartCoroutine("Choice"); // 대화창 오브젝트 켜기
    }

    public void PlayerChoice() // 두번째 - 플레이어 선택
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        diglogObject.SetActive(false); // 대화창 오브젝트 끄기
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
    } // 오브젝트 다 삭제 (총알)

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

        if(diglogData.Length -1 <= index) // 첫번째 멘트 마지막 문구일때 선택지 뜨기 
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
