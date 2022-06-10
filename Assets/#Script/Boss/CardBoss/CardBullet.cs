using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBullet : MonoBehaviour
{
    [SerializeField]
    private Color[] cardColor;
    [SerializeField]
    private Sprite[] cardSprites = new Sprite[2];
    private Transform target;
    private SpriteRenderer spriteRender;
    private BoxCollider2D boxCollider2D;
    private int colorIndex; // 카드 색깔 인덱스
    private int cardIndex; // 카드 이미지 인덱스
    private float moveSpeed; // 카드 이동 속도 
    private float cardColorChangeTime; // 카드 색깔 변화는 시간
    private Vector3 moveDirection = Vector3.zero; // 움직이는 방향


    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        colorIndex = Random.Range(0, cardColor.Length);
        cardIndex = Random.Range(0, cardSprites.Length);
        spriteRender.color = cardColor[colorIndex];
        spriteRender.sprite = cardSprites[cardIndex];

        boxCollider2D.enabled = false;
        StartCoroutine("Attack2");
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void Init(float colorChangeTime, float moveSpeed) // 카드 알파값 변화는 시간 , 카드 스피드
    {
        cardColorChangeTime = colorChangeTime;
        this.moveSpeed = moveSpeed;
    }

    private IEnumerator Attack2()
    {
        float currentTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / cardColorChangeTime;
            Color color = spriteRender.color;
            color.a = Mathf.Lerp(0, 1, percent);
            spriteRender.color = color;
            yield return null;
        }
        boxCollider2D.enabled = true;
        moveDirection = (target.position - transform.position).normalized;
        yield return null;
    }
}
