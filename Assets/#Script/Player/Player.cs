using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MapData mapData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject playerHitEffectObject;
    [SerializeField] private float bulletMoveSpeed; // �Ѿ� �ӵ� 

    private bool isHit = false; // Ÿ�� ���ߴ°�?
    private bool isPause; // ������ Ȱ��ȭ���̸� ����
    private bool isFireLock; // �Ѿ� �߻縦 ��ɴ���?
    private bool isFireRangeLimit; // �Ѿ� ��Ÿ� �����ϴ°�?
    private bool isDied = false;
    [Range(0, 2)][SerializeField] private int diedIndex; // 0 ī�� , 1 ��� , 2 ������
    [SerializeField] private bool isInvincibility; // �������ΰ�?

    private int playerHp = 3;
    private float playerMoveSpeed = 7.0f;
    [SerializeField][Range(0.01f, 1.00f)] private float fireRate = 0.15f;
    private float nextFire = 0.0f;
    private Animator playerAnimator;

    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip diedSoundClip;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPause || isDied)
            return;

        Movement();
        LimitPosition();
        Rotate();

        // if (Input.GetMouseButtonDown(0) && isFireLock == false)
        // {
        //     Fire();
        // }

        if (Input.GetMouseButton(0) && Time.time > nextFire && isFireLock == false)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    public void PlayerDebuffs()
    {
        switch (diedIndex)
        {
            case 0:
                PlayerSizeUp();
                break;
            case 1:
                PlayerFireRangeLimit();
                break;
        }
        Instantiate(playerHitEffectObject, transform.position, Quaternion.identity);
    }

    private void PlayerSizeUp() // CardBoss Dubuff
    {
        //transform.localScale = new Vector3(transform.localScale.x * 2.5f, transform.localScale.y * 1.5f, 0);
        transform.localScale = new Vector3(2.5f, 2.5f, 0);
    }

    private void PlayerFireRangeLimit()
    {
        isFireRangeLimit = true;
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * playerMoveSpeed * Time.deltaTime;
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
        clone.GetComponent<PlayerBullet>().BulletMovement(Vector2.up, bulletMoveSpeed, isFireRangeLimit);
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

    public void PlayerDiedEvent()
    {
        StartCoroutine("IPlayerDiedEvent");
    }

    private IEnumerator IPlayerDiedEvent()
    {
        isDied = true;
        AudioClipChange(diedSoundClip);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetInteger("DiedIndex", diedIndex);
        playerAnimator.SetTrigger("Died");
        //transform.rotation = Quaternion.identity; // Ȱ��ȭ�ϸ� ȸ�� ����
    }

    public void BossDiedEvent()
    {
        isPause = true;
        isInvincibility = true;
    }

    private IEnumerator Hit()
    {
        isHit = true;
        Instantiate(playerHitEffectObject, transform.position, Quaternion.identity);
        playerHp--;
        PlayerHpUISystem.playerUISystem(); // Action PlayerUI Event

        if (playerHp <= 0)
        {
            GameSystem.PlayerDied();
            yield break;
        }

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
        if (collision.transform.CompareTag("BulletPenetrationPossible")) // ���밡�� 
        {
            TakeDamage();
        }

        if (collision.transform.CompareTag("BulletPenetrationImpossible")) // ���� �Ұ��� 
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }

        if (collision.transform.CompareTag("Boss"))
        {
            TakeDamage();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("BulletPenetrationImpossible"))
        {
            TakeDamage();
        }
    }

    private void AudioClipChange(AudioClip clip)
    {
        playerAudioSource.clip = clip;
        playerAudioSource.Play();
    }

}
