using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : CardBossPatternBase
{
    // ī�庸�� ����2  �¿� ���̵� ����
    [SerializeField] private GameObject cardObject; // ī�� ������Ʈ
    private float moveTime = 1.0f; // �ش� �������� �������� �ɸ��� �ð�

    #region ������ġ ���Ͱ�
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;
    private Vector3 leftStartPoint = new Vector3(-11, 0, 0); // ���� ��Ÿ���� ������
    private Vector3 leftEndPoint = new Vector3(-8, 0, 0);    // ���ʿ��� ��Ÿ���� ��������
    private Vector3 rightStartPoint = new Vector3(11, 0, 0); // ������ ��Ÿ���� ������
    private Vector3 rightEndPoint = new Vector3(8, 0, 0);    // �����ʿ��� ��Ÿ���� ��������
    #endregion

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
        cardBossAnimator.SetTrigger("Attack2");
        int count = 0;
        while (cardBoss.cardBossData.p2_attackCount > count)
        {
            GameObject clone = Instantiate(cardObject);
            clone.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.5f, 4.0f), 0);
            clone.GetComponent<CardBullet>().Init(cardBoss.cardBossData.p2_cardColorChangeTime, cardBoss.cardBossData.p2_cardMoveSpeed);
            yield return new WaitForSeconds(cardBoss.cardBossData.p2_attackDelayTime);
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

        cardBossAnimator.SetTrigger("Attack2Idle");
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

    public override IEnumerator Attacking()
    {
        PlayerAttackPosition();
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(endPosition)); // ���� �����ϱ����� ������������ ����
        yield return StartCoroutine(CardSpawn()); // ī�� ����
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(startPosition)); // �� ������ ������� ���� ������ �ٽ� ����
        cardBossAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime);
    }
}
