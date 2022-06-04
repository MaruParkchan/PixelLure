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

    [SerializeField] private HpUIsystem hpUIsystem;
    [SerializeField] private GameObject blinkObject;

    #region 대화창 
    [SerializeField] private GameObject diglogObject; // 대화창 오브젝트 
    [SerializeField] private TextMeshProUGUI diglogText; // 대화창 TextMeshPro
    [SerializeField] private GameObject leftChoiceObject;
    [SerializeField] private GameObject rightChoiceObject;
    [SerializeField] private DiglogData diglogData;
    [SerializeField] private GameObject cursorArrowObject;
    private bool isChoice; // 선택을 했는지?
    private bool isSeceleted = false; // 선택하였다

    private string[] diglogDatas; // 대화창 데이터 string
    private string diglog = ""; // 대화창출력할 string
    private int diglogIndex = 0; // 대화창 인덱스
    private float typingSpeed = 0.11f;
    private bool isFirst = true; 
    private bool isTyping = false; // 타이핑중인지
    private bool isAccept = false; // 왼쪽, 오른쪽 선택값 
    #endregion

    private void Start()
    {
        diglogText.text = "";
        diglogData.TextFistInitUpdate();
    }

    public void PauseAndTalk() // 첫번째 - 선택하기 위한 모든 정지
    {
        StartCoroutine("Choice"); // 대화창 오브젝트 켜기
    }

    public void PlayerChoice() // 두번째 - 플레이어 선택
    {
        leftChoiceObject.SetActive(true);
        rightChoiceObject.SetActive(true);
        //diglogObject.SetActive(false); // 대화창 오브젝트 끄기
        player.Choice();
    }

    public void PlayerFreezeAndLastTalk() // 세번째 - 마지막 문구 출력 및 플레이어 이동 잠금
    {
        isChoice = true;
        isSeceleted = true;
        player.Wait();
    }

    public void ResumeGame() // 네번째 - 선택완료 및 게임 재시작 
    {
        player.Resume();
        boss.GetComponent<IPause>().Resume();
        hpUIsystem.SliderMaxValueUpdate(boss.GetComponent<BossHp>().GetSecondHp()); // 보스 페이즈2 HP 교환 
        diglogObject.SetActive(false);
    }

    private void GameObjectAllFind() // 플레이어, 적의 총알 오브젝트 다찾아서 삭제시킴
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
        //GameObjectAllFind(); // 애를 비활성화하면 선택시 총알이 안사라짐
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
        if( isFirst == true) // 최초 대사 시작
        {
            diglogIndex = 0; // 텍스트 데이터 0으로 초기화
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
        if (isFirst == true) // 최초 대사 시작
        {
            diglogIndex = 0; // 텍스트 데이터 0으로 초기화
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

            if ((diglogDatas.Length - 2 == diglogIndex) && isChoice == true) // 마지막 문구 전에 카드를 없애기 위해 if문 설계
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
        diglogIndex++; // 다음 대사 진행 
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

    public void TextDataSetUpdate(string[] textDatas) // 선택한것에 따라 텍스트 데이터 변경하기 
    {
        diglogDatas = new string[textDatas.Length]; // 받아온 string의 배열 크기만큼 바꾸기
        
        for(int i = 0; i < textDatas.Length; i++) // 텍스트데이터 넣기 
        {
            diglogDatas[i] = textDatas[i];
        }
    }

    public void ChoiceSelect(bool isAccept) // Yes or No 선택했으면 받아오기 
    {
        this.isAccept = isAccept;
        if (isAccept == true)
        {
            rightChoiceObject.SetActive(false); // Yes이기때문에 왼쪽 남기고 오른쪽 삭제
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
