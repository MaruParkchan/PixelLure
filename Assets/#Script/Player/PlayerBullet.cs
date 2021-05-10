using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float bulletMoveSpeed;
    private Vector2 direction;


    private void Update()
    {
        transform.Translate(direction * bulletMoveSpeed * Time.deltaTime);
    }

    public void BulletMovement(Vector2 dir, float moveSpeed)
    {
        direction = dir;
        bulletMoveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {

        }
    }
}
