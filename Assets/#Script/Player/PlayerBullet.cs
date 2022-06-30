using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float bulletMoveSpeed;
    private Vector2 direction;

    private bool isRangeLimit = false;


    private void Update()
    {
        if(isRangeLimit == true)
            Destroy(this.gameObject, 0.5f);

        transform.Translate(direction * bulletMoveSpeed * Time.deltaTime);
    }

    public void BulletMovement(Vector2 dir, float moveSpeed, bool rangeLimit)
    {
        direction = dir;
        bulletMoveSpeed = moveSpeed;
        isRangeLimit = rangeLimit;
    }
}
