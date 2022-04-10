using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int playerHp;

    public void TakeDamage()
    {
        Debug.Log("데미지!");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.CompareTag("EnemyBullet"))
        {
            Destroy(col.gameObject);
            TakeDamage();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("EnemyBullet")) // 관통하는지 안하는지 구분하기 
        {
            TakeDamage();
        }
    }
}
