using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : CardBossPatternBase
{
    // 카드보스 패턴2  좌우 사이드 패턴
    [SerializeField] private GameObject cardObject; // 카드 오브젝트
    private float moveTime = 1.0f; // 해당 지점까지 도착까지 걸리는 시간

    #region 등장위치 벡터값
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;
    private Vector3 leftStartPoint = new Vector3(-11, 0, 0); // 왼쪽 나타나는 시작점
    private Vector3 leftEndPoint = new Vector3(-8, 0, 0);    // 왼쪽에서 나타날시 도착지점
    private Vector3 rightStartPoint = new Vector3(11, 0, 0); // 오른쪽 나타나는 시작점
    private Vector3 rightEndPoint = new Vector3(8, 0, 0);    // 오른쪽에서 나타날시 도착지점
    #endregion

    private IEnumerator SmoothMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        while(percent < moveTime) // startPosition 에서 EndPosition까지 moveTime동안 이동
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

    private void PlayerAttackPosition() // 좌우 중 위치 랜덤지정
    {
        int appearPoint = Random.Range(0, 2);
        if (appearPoint == 0) // 왼쪽
        {
            transform.position = leftStartPoint;
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            startPosition = leftStartPoint;
            endPosition = leftEndPoint;
        }
        else // 오른쪽
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
        yield return StartCoroutine(SmoothMovement(endPosition)); // 패턴 시작하기위해 도착지점까지 도착
        yield return StartCoroutine(CardSpawn()); // 카드 스폰
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(startPosition)); // 맵 밖으로 사라지기 위한 시작점 다시 가기
        cardBossAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime);
    }
}
