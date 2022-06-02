using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPause
{
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject playerHitEffectObject;
    [SerializeField]
    private float bulletMoveSpeed; // �Ѿ� �ӵ� 

    private bool isHit = false; // Ÿ�� ���ߴ°�?
    private bool isPause; // ������ Ȱ��ȭ���̸� ����
    private bool isFireLock; // �Ѿ� �߻縦 ��ɴ���?
    private bool isInvincibility; // �������ΰ�?

    [SerializeField] private int playerHp;
    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPause)
            return;

        Movement();
        LimitPosition();
        Rotate();

        if (Input.GetMouseButtonDown(0) && isFireLock == false)
        {
            Fire();
        }
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * playerData.MoveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - this.transform.position.x, mousePosition.y - this.transform.position.y);

        transform.up = direction;
    }

    private void LimitPosition() // �̵����� 
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mapData.LimitMin.x, mapData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, mapData.LimitMin.y, mapData.LimitMax.y));
    }

    private void Fire()
    {
        GameObject clone = Instantiate(bulletPrefab);
        clone.GetComponent<PlayerBullet>().BulletMovement(Vector2.up, bulletMoveSpeed);
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
    }

    private void PositionReset() // �������� ���� ��ġ ������ 
    {
        transform.position = new Vector3(0, -4.0f, 0);
        transform.rotation = Quaternion.identity;
    }

    public void Pause() // ������ Ȱ��ȭ - 1
    {
        isPause = true; // ������ Ȱ��ȭ
        isFireLock = true; // ���� ���� 
        isInvincibility = true; // ����Ȱ��ȭ
        PositionReset();
    }

    public void Choice() // ������ �ϱ� ���� �̵�, ȸ�� �ٽ� Ǯ��  - 2
    {
        isPause = false;
    }

    public void Wait() // ������ �� �������� ��� ���߱� ���� �޼ҵ� - 3
    {
        isPause = true;
    }

    public void Resume() // ���� �Ϸ� ���� �簳 - 4
    {
        isPause = false;
        isFireLock = false;
        isInvincibility = false;
    }

    private IEnumerator Hit()
    {
        isHit = true;
        Instantiate(playerHitEffectObject, transform.position, Quaternion.identity);
        playerHp--;
        PlayerHpUISystem.playerUISystem(); // Action PlayerUI Event
        playerAnimator.SetTrigger("Hit");
        yield return new WaitForSeconds(2.0f);
        playerAnimator.SetTrigger("BaseState");
        isHit = false;
    }

    private void TakeDamage()
    {
        if (isHit == true || isInvincibility == true)
            return;

        StartCoroutine(Hit());
    }

    private void OnTriggerEnter2D(Collider2D collision) // ���� , ����� , ��ƼŬ ���� �ؾ��� 
    {
        if(collision.transform.CompareTag("BulletPenetrationPossible")) // ���밡�� 
        {
            TakeDamage();
        }

        if(collision.transform.CompareTag("BulletPenetrationImpossible")) // ���� �Ұ��� 
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }

        if(collision.transform.CompareTag("Boss"))
        {
            TakeDamage();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.transform.CompareTag("BulletPenetrationImpossible"))
        {
            TakeDamage();
        }
    }
}
