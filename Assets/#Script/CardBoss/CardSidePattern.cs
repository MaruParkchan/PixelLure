using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSidePattern : MonoBehaviour
{
    [SerializeField] private GameObject cardObject; // ī�� ������Ʈ

    [Header("ī�� ���� ��")]
    [SerializeField]
    private int spawnCardMaxCount; // ī�� ������ �ִ� ���� 

    [SerializeField]
    private float waitTime; // ��� �ð�
    [SerializeField]
    private float moveTime; // �̵� �ð�

    public IEnumerator ISidePattern()
    {
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

    private void PlayerAttackPosition() // �¿� �� ��ġ ��������
    {
        int i = Random.Range(0, 2); // ��ġ 2�� and ĳ���� ȸ�� 

        if (i == 0) // ����
        {
            transform.position = new Vector3(-11, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }
        else // ������
        {
            transform.position = new Vector3(11, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
