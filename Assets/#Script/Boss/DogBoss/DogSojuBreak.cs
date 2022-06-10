using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSojuBreak : MonoBehaviour
{
    [SerializeField]
    private GameObject sojuEffect;
    private float moveSpeed;
    private bool isBreak = false;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
    }

    private void Update()
    {
        if(isBreak == false)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }

    public void Init(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void SojuEffect()
    {
        GameObject clone = Instantiate(sojuEffect);
        clone.transform.position = this.transform.position;
    }

    public void Break()
    {
        animator.SetTrigger("Break");
        isBreak = true;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}