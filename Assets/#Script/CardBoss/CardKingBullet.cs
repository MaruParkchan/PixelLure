using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKingBullet : MonoBehaviour
{
    private float accelerationWaitTime; // 가속도 대기 시간
    private float initialMoveSpeed; // 처음 이동속도
    private float accelerationSpeed; // 가속도
    private float moveSpeed;// 킹 카드 이동속도

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    // 가속도 대기시간 , 처음 이동속도, 가속도 
    public void KingCardInit(float accelerationWaitTime, float initialMoveSpeed, float accelerationSpeed)
    {
        this.accelerationWaitTime = accelerationWaitTime;
        this.initialMoveSpeed = initialMoveSpeed;
        this.accelerationSpeed = accelerationSpeed;

        StartCoroutine("SpeedAcceleration");
    }

    private IEnumerator SpeedAcceleration()
    {
        moveSpeed = initialMoveSpeed;
        yield return new WaitForSeconds(accelerationWaitTime);
        moveSpeed = accelerationSpeed;
    }
}
