using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBundleLaser : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Destroy(this.gameObject, 3.0f);
    }
}
