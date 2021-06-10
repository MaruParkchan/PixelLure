using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // 이동 속도

    [SerializeField] private float scaleValue = 1.0f; // 크기 
    [SerializeField] private GameObject bubbleBoomEffect; // 피가 닳면 터지는 이펙트
    [SerializeField] private GameObject bubbleRainEffect; // 최고지점 위까지 도달하면 터지는 이펙트
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
