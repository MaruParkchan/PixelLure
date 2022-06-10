using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSojuTrace : MonoBehaviour
{
    private float rotateSpeed;
    private float moveSpeed;
    private float attackTime;
    private Transform target;
    private bool isRotate = true; // 회전하는지? 
    private bool isAttack; // 타겟한테 공격
    private BoxCollider2D boxCollider2D;
    Vector3 dir;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(TargetAttack());
    }

    private void Update()
    {
        if (isRotate == true)
            transform.Rotate(0, 0, 5 * rotateSpeed * Time.deltaTime);

        else
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // 타겟 방향으로 다가감
        if (isAttack == true)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    public void Init(float rotateSpeed, float moveSpeed,float attackTime)
    {
        this.rotateSpeed = rotateSpeed;
        this.moveSpeed = moveSpeed;
        this.attackTime = attackTime;
    }

    private IEnumerator TargetAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isRotate = false;
        dir = target.position - transform.position;
        isAttack = true;
        boxCollider2D.enabled = true;
    }
}
