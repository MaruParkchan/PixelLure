using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceEvent : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject chainEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            animator.SetBool("Choice", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            animator.SetBool("Choice", false);
        }
    }

    public void Effect()
    {
        GameObject clone = Instantiate(chainEffect);
        clone.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}