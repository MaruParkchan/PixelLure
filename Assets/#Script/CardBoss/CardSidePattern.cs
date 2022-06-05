using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : MonoBehaviour 
{
    // ī�庸�� ����2  �¿� ���̵� ����
    [SerializeField] private GameObject cardObject; // ī�� ������Ʈ

    [Header("ī�� ���� ����")]
    [SerializeField]
    private int spawnCardMaxCount; // ī�� ������ �ִ� ���� 

    [SerializeField]
    private float waitTime; // ��� �ð�
    [SerializeField]
    private float moveTime; // �̵� �ð�
    [SerializeField]
    private float cardSpawnCycleTime; // ī�� �� ���� �ӵ�
    [SerializeField]
    private float cardColorChangeTime; // ī�� ���İ� �ӵ�
    [SerializeField]
    private float cardMoveSpeed; // ī�� �̵� �ӵ�

    private Animator animator;

    #region ������ġ ���Ͱ�
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector3 leftStartPoint = new Vector3(-11, 0, 0); // ���� ��Ÿ���� ������
    private Vector3 leftEndPoint = new Vector3(-8, 0, 0);    // ���ʿ��� ��Ÿ���� ��������
    private Vector3 rightStartPoint = new Vector3(11, 0, 0); // ������ ��Ÿ���� ������
    private Vector3 rightEndPoint = new Vector3(8, 0, 0);    // �����ʿ��� ��Ÿ���� ��������
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ISidePattern()
    {
        PlayerAttackPosition();
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(endPosition)); // ���� �����ϱ����� ������������ ����
        yield return StartCoroutine(CardSpawn()); // ī�� ����
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(startPosition)); // �� ������ ������� ���� ������ �ٽ� ����
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator SmoothMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        while(percent < moveTime) // startPosition ���� EndPosition���� moveTime���� �̵�
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
    }

    private IEnumerator CardSpawn()
    {
        animator.SetTrigger("Attack2");
        int count = 0;
        while (spawnCardMaxCount > count)
        {
            GameObject clone = Instantiate(cardObject);
            clone.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.5f, 4.0f), 0);
            clone.GetComponent<CardBullet>().Init(cardColorChangeTime, cardMoveSpeed);
            yield return new WaitForSeconds(cardSpawnCycleTime);
            count++;
        }
    }

    private void PlayerAttackPosition() // �¿� �� ��ġ ��������
    {
        int appearPoint = Random.Range(0, 2);
        if (appearPoint == 0) // ����
        {
            transform.position = leftStartPoint;
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            startPosition = leftStartPoint;
            endPosition = leftEndPoint;
        }
        else // ������
        {
            transform.position = rightStartPoint;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            startPosition = rightStartPoint;
            endPosition = rightEndPoint;
        }

        animator.SetTrigger("Attack2Idle");
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }
}
