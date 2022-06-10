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
    private int colorIndex; // ī�� ���� �ε���
    private int cardIndex; // ī�� �̹��� �ε���
    private float moveSpeed; // ī�� �̵� �ӵ� 
    private float cardColorChangeTime; // ī�� ���� ��ȭ�� �ð�
    private Vector3 moveDirection = Vector3.zero; // �����̴� ����


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

    public void Init(float colorChangeTime, float moveSpeed) // ī�� ���İ� ��ȭ�� �ð� , ī�� ���ǵ�
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
