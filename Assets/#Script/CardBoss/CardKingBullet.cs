using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKingBullet : MonoBehaviour
{
    private float accelerationWaitTime; // ���ӵ� ��� �ð�
    private float initialMoveSpeed; // ó�� �̵��ӵ�
    private float accelerationSpeed; // ���ӵ�
    private float moveSpeed;// ŷ ī�� �̵��ӵ�

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    // ���ӵ� ���ð� , ó�� �̵��ӵ�, ���ӵ� 
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
