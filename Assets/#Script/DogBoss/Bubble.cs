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

    private Animator animator;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private bool isBoom = false; // 물방울이 터진 상태?
    private void Start()
    {
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBoom)
            return;

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
            Destroy(collision.gameObject);
            StopCoroutine("HitColor");
            StartCoroutine("HitColor");
            if (bubbleHp <= 0)
            {
                BubbleBoomEffectSpawn();
                BubbleBoom();
            }
        }
    }

    private IEnumerator HitColor()
    {
        spriteRenderer.color = new Color(255f/255f, 64f/255f, 69f/255f, 255f/255f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }

    private void BubbleBoom()
    {
        animator.SetTrigger("Boom");
        circleCollider2D.enabled = false;
        isBoom = true;
    }

    private void BubbleBoomEffectSpawn()
    {
        GameObject clone = Instantiate(bubbleBoomEffect);
        clone.GetComponent<DogBubbleBoomEffect>().ParticleSystemInit(5.0f * scaleValue);
        clone.transform.position = this.transform.position;
    }

    public void DestroyBubble()
    {
        Destroy(this.gameObject);
    }
}
