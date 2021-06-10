using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // �̵� �ӵ�

    [SerializeField] private float scaleValue = 1.0f; // ũ�� 
    [SerializeField] private GameObject bubbleBoomEffect; // �ǰ� ��� ������ ����Ʈ
    [SerializeField] private GameObject bubbleRainEffect; // �ְ����� ������ �����ϸ� ������ ����Ʈ
    [SerializeField] private int bubbleHp;

    private void Update()
    {
        if(transform.position.y > 5.0f)
        {
            BubbleRainBoom();
            return;
        }

        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        scaleValue += Time.deltaTime;
        transform.localScale = new Vector3(scaleValue, scaleValue, 0);
    }

    private void BubbleRainBoom()
    {
        GameObject clone = Instantiate(bubbleRainEffect);
        clone.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBullet"))
        {
            bubbleHp--;
        }
    }



}
