using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : MonoBehaviour
{
    // 카드보스 패턴2  좌우 사이드 패턴
    [SerializeField] private GameObject cardObject; // 카드 오브젝트

    [Header("카드 생성 수")]
    [SerializeField]
    private int spawnCardMaxCount; // 카드 생성할 최대 갯수 

    [SerializeField]
    private float waitTime; // 대기 시간
    [SerializeField]
    private float moveTime; // 이동 시간
    [SerializeField]
    private float cardSpawnCycleTime; // 카드 재 생성 속도
    [SerializeField]
    private float card

    private Animator animator;

    #region 등장위치 벡터값
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector3 leftStartPoint = new Vector3(-11, 0, 0); // 왼쪽 나타나는 시작점
    private Vector3 leftEndPoint = new Vector3(-8, 0, 0);    // 왼쪽에서 나타날시 도착지점
    private Vector3 rightStartPoint = new Vector3(11, 0, 0); // 오른쪽 나타나는 시작점
    private Vector3 rightEndPoint = new Vector3(8, 0, 0);    // 오른쪽에서 나타날시 도착지점
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ISidePattern()
    {
        PlayerAttackPosition();
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(endPosition));
        yield return StartCoroutine(CardSpawn());
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(SmoothMovement(startPosition));
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime);
    }

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
        animator.SetTrigger("Attack2");
        int count = 0;
        while (spawnCardMaxCount > count)
        {
            GameObject clone = Instantiate(cardObject);
            clone.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
            clone.GetComponent<CardBullet>().Init(1.25f, 6.0f);
            yield return new WaitForSeconds(cardSpawnCycleTime);
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

        animator.SetTrigger("Attack2Idle");
    }
}
