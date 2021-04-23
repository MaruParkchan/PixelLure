using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMovePattern : MonoBehaviour
{
    [SerializeField] private GameObject touchObject;
    [SerializeField] private float touchDropCycleTime; // 라이터 뿌리는 시간
    [SerializeField] private int cycleCount; // n번 반복 횟수
    [SerializeField] private float moveTime; // 해당 지점까지 도착시간

    private SmokeBoss smokeBoss;
    private Animator animator;

    #region 위치 벡터값
    [SerializeField] private Transform[] movePositions;
    private Vector2[] startPoints = new Vector2[30];
    private Vector2[] endPoints = new Vector2[30];
    #endregion

    private void Start()
    {
        smokeBoss = GetComponent<SmokeBoss>();
        animator = GetComponent<Animator>();
        PositionInit(); // 스타트, 도착지점 초기화
    }

    public IEnumerator MovePattern()
    {
        int currentCount = 0;
        int pointIndex = 0;
        animator.SetTrigger("Appear");

        while (cycleCount >= currentCount)
        {
            pointIndex = Random.Range(0, 30);
            transform.position = startPoints[pointIndex];
            yield return StartCoroutine(SmoothMovement(endPoints[pointIndex]));
            currentCount++;
        }

        yield return new WaitForSeconds(2.0f);
    }

    private IEnumerator SmoothMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        while (percent < moveTime) // startPosition 에서 EndPosition까지 moveTime동안 이동
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
    }

    private void PositionInit()
    {
        #region 시작지점
        startPoints[0]  = movePositions[0].position;
        startPoints[1]  = movePositions[1].position;
        startPoints[2]  = movePositions[2].position;
        startPoints[3]  = movePositions[3].position;
        startPoints[4]  = movePositions[4].position;
        startPoints[5]  = movePositions[5].position;
        startPoints[6]  = movePositions[6].position;
        startPoints[7]  = movePositions[7].position;
        startPoints[8]  = movePositions[8].position;
        startPoints[9]  = movePositions[9].position;
        startPoints[10] = movePositions[10].position;
        startPoints[11] = movePositions[11].position;
        startPoints[12] = movePositions[12].position;
        startPoints[13] = movePositions[13].position;
        startPoints[14] = movePositions[14].position;
        startPoints[15] = movePositions[15].position;
        startPoints[16] = movePositions[16].position;
        startPoints[17] = movePositions[17].position;
        startPoints[18] = movePositions[18].position;
        startPoints[19] = movePositions[19].position;
        startPoints[20] = movePositions[20].position;
        startPoints[21] = movePositions[21].position;
        startPoints[22] = movePositions[22].position;
        startPoints[23] = movePositions[23].position;
        startPoints[24] = movePositions[24].position;
        startPoints[25] = movePositions[25].position;
        startPoints[26] = movePositions[26].position;
        startPoints[27] = movePositions[27].position;
        startPoints[28] = movePositions[28].position;
        startPoints[29] = movePositions[29].position;
        #endregion 시작지점

        #region 도착지점
        endPoints[0] = movePositions[15].position;
        endPoints[1] = movePositions[24].position;
        endPoints[2] = movePositions[23].position;
        endPoints[3] = movePositions[22].position;
        endPoints[4] = movePositions[21].position;
        endPoints[5] = movePositions[20].position;
        endPoints[6] = movePositions[19].position;
        endPoints[7] = movePositions[18].position;
        endPoints[8] = movePositions[17].position;
        endPoints[9] = movePositions[16].position;
        endPoints[10] = movePositions[25].position;
        endPoints[11] = movePositions[29].position;
        endPoints[12] = movePositions[28].position;
        endPoints[13] = movePositions[27].position;
        endPoints[14] = movePositions[26].position;
        endPoints[15] = movePositions[0].position;
        endPoints[16] = movePositions[9].position;
        endPoints[17] = movePositions[8].position;
        endPoints[18] = movePositions[7].position;
        endPoints[19] = movePositions[6].position;
        endPoints[20] = movePositions[5].position;
        endPoints[21] = movePositions[4].position;
        endPoints[22] = movePositions[3].position;
        endPoints[23] = movePositions[2].position;
        endPoints[24] = movePositions[1].position;
        endPoints[25] = movePositions[10].position;
        endPoints[26] = movePositions[14].position;
        endPoints[27] = movePositions[13].position;
        endPoints[28] = movePositions[12].position;
        endPoints[29] = movePositions[11].position;
        #endregion 도착지점
    }
}
