using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : MonoBehaviour
{
    [SerializeField] private GameObject cardObject; // 카드 오브젝트

    [Header("카드 생성 수")]
    [SerializeField]
    private int spawnCardMaxCount; // 카드 생성할 최대 갯수 

    [SerializeField]
    private float waitTime; // 대기 시간
    [SerializeField]
    private float moveTime; // 이동 시간

    public IEnumerator ISidePattern()
    {
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

    private void PlayerAttackPosition() // 좌우 중 위치 랜덤지정
    {
        int i = Random.Range(0, 2); // 위치 2곳 and 캐릭터 회전 

        if (i == 0) // 왼쪽
        {
            transform.position = new Vector3(-11, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }
        else // 오른쪽
        {
            transform.position = new Vector3(11, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
