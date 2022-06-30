
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private Player player;
    [SerializeField] private GameObject mainCamera;

    private GameObject[] enemyObjects1; // �� ������Ʈ�� ���
    private GameObject[] enemyObjects2; // �� ������Ʈ�� ���
    private GameObject[] playerObjects; // �÷��̾� ������Ʈ����
    List<GameObject> bullets;

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
    // [HideInInspector]
    // public bool isAccept = false; // ����, ������ ���ð� 
    #endregion

    public static Action PlayerDied;
    public static Action BossDied;
    public static bool isAccept;

    private void Start()
    {
        diglogText.text = "";
        diglogData.TextFistInitUpdate();
        PlayerDied = () => { PlayerDiedEvent(); };
        BossDied = () => { BossDiedEvent(); };
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
        boss.Resume();
        hpUIsystem.SliderMaxValueUpdate();
        diglogObject.SetActive(false);
    }

    public void PlayerDiedEvent()
    {
        player.PlayerDiedEvent();
        boss.PlayerDiedEvent();
        blinkObject.SetActive(true);
        GameObjectAllFind();
        CameraSetting();
        PlayerPrefs.SetInt("DiedCount", PlayerPrefs.GetInt("DiedCount") + 1);
        StartCoroutine("IPlayerDiedEvent");
    }

    public void BossDiedEvent()
    {
        blinkObject.SetActive(true);
        GameObjectAllFind();
        player.BossDiedEvent();
        boss.BossDiedEvent();
        StartCoroutine("IBossDiedEvent");
    }

    private void CameraSetting()
    {
        CameraShake.cameraPlayerFoucs();
    }

    private void GameObjectAllFind() // �÷��̾�, ���� �Ѿ� ������Ʈ ��ã�Ƽ� ������Ŵ
    {
        enemyObjects1 = GameObject.FindGameObjectsWithTag("BulletPenetrationImpossible");
        enemyObjects2 = GameObject.FindGameObjectsWithTag("BulletPenetrationPossible");
        playerObjects = GameObject.FindGameObjectsWithTag("PlayerBullet");

        for (int i = 0; i < enemyObjects1.Length; i++)
        {
            Destroy(enemyObjects1[i]);
        }
        for (int i = 0; i < enemyObjects2.Length; i++)
        {
            Destroy(enemyObjects2[i]);
        }
        for (int i = 0; i < playerObjects.Length; i++)
        {
            Destroy(playerObjects[i]);
        }
    } // ������Ʈ �� ���� (�Ѿ�)

    private IEnumerator IPlayerDiedEvent()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Intro");
    }

    private IEnumerator IBossDiedEvent()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Intro");
    }

    private IEnumerator Choice()
    {
        player.Pause();
        blinkObject.SetActive(true);
        GameObjectAllFind(); // �ָ� ��Ȱ��ȭ�ϸ� ���ý� �Ѿ��� �Ȼ����
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
        if (isFirst == true) // ���� ��� ����
        {
            diglogIndex = 0; // �ؽ�Ʈ ������ 0���� �ʱ�ȭ
            StartCoroutine("TypingText");
            isFirst = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isTyping == true)
            {
                isTyping = false;

                StopCoroutine("TypingText");
                cursorArrowObject.SetActive(true);
                diglogText.text = diglog;

                return false;
            }


            if (diglogDatas.Length - 1 > diglogIndex)
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
                if (isAccept == false)
                    player.PlayerDebuffs();
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
        while (index < diglog.Length)
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

        for (int i = 0; i < textDatas.Length; i++) // �ؽ�Ʈ������ �ֱ� 
        {
            diglogDatas[i] = textDatas[i];
        }
    }

    public void ChoiceSelect(bool accept) // Yes or No ���������� �޾ƿ��� 
    {
        isAccept = accept;
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
