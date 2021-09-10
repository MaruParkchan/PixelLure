using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IPause
{
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletMoveSpeed; // 총알 속도 

    private bool isPause; // 선택지 활성화중이면 멈춤
    private bool isFireLock; // 총알 발사를 잠궜는지?
    private bool isInvincibility; // 무적중인가?

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

    private void LimitPosition() // 이동제한 
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

    private void PositionReset() // 선택지를 위한 위치 재조정 
    {
        transform.position = new Vector3(0, -4.0f, 0);
        transform.rotation = Quaternion.identity;
    }

    public void Pause() // 선택지 활성화 - 1
    {
        isPause = true; // 선택지 활성화
        isFireLock = true; // 공격 중지 
        isInvincibility = true; // 무적활성화
        PositionReset();
    }

    public void Choice() // 선택을 하기 위해 이동, 회전 다시 풀기  - 2
    {
        isPause = false;
    }

    public void Wait() // 선택지 다 차오르면 잠시 멈추기 위한 메소드 - 3
    {
        isPause = true;
    }

    public void Resume() // 선택 완료 게임 재개 - 4
    {
        isPause = false;
        isFireLock = false;
        isInvincibility = false;
    }
}
