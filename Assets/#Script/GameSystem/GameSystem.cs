using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Player player;

    private GameObject[] enemyObjects; // 적 오브젝트들 담기
    private GameObject[] playerObjects; // 플레이어 오브젝트들담기

    [SerializeField] private GameObject blinkObject;


    #region 대화창 
    [SerializeField] private GameObject diglogObject; // 대화창 오브젝트 
    [SerializeField] private TextMeshProUGUI diglogText; // 대화창 TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;
    [SerializeField] private DiglogData diglogData;
    private bool isChoice; // 선택을 했는지?

    private string[] diglogDatas; // 대화창 데이터 string
    private int diglogIndex = 0; // 대화창 인덱스
    private int choiceValue = 0; // 왼쪽, 오른쪽 선택값 
    #endregion

    private void Start()
    {
        diglogText.text = "";
    }

    public void PauseAndTalk() // 첫번째 - 선택하기 위한 모든 정지
    {
        StartCoroutine("Choice"); // 대화창 오브젝트 켜기
    }

    public void PlayerChoice() // 두번째 - 플레이어 선택
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        // diglogObject.SetActive(false); // 대화창 오브젝트 끄기
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // 세번째 - 마지막 문구 출력 및 플레이어 이동 잠금
    {
        isChoice = true;
        player.Wait();
    }

    public void ResumeGame() // 네번째 - 선택완료 및 게임 재시작 
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
    } // 오브젝트 다 삭제 (총알)

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
                if ((diglogDatas.Length - 2 == diglogIndex) && isChoice == true) // 마지막 문구 전에 카드를 없애기 위해 if문 설계
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

    public void TextDataSetUpdate(string[] textDatas) // 선택한것에 따라 텍스트 데이터 변경하기 
    {
        diglogDatas = new string[textDatas.Length]; // 받아온 string의 배열 크기만큼 바꾸기
        
        for(int i = 0; i < textDatas.Length; i++) // 텍스트데이터 넣기 
        {
            diglogDatas[i] = textDatas[i];
        }
    }

    public void ChoiceSelect(int value) // Yes or No 선택했으면 받아오기 
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
        diglogIndex = 0; // 텍스트 데이터 0으로 초기화
        diglogData.TextDataUpdate(choiceValue);
        PlayerFreezeAndLastTalk();
        StartCoroutine(TextUpdate());
    }

}
