using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D collider;

    [SerializeField]
    private GameObject stage1Sprite;
    [SerializeField]
    private GameObject stage2Sprite;
    [SerializeField]
    private GameObject stage3Sprite;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Click");
        }
    }

    IEnumerator Click()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(0.01f);
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Stage1"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            this.gameObject.SetActive(false);
            stage2Sprite.SetActive(false);
            stage3Sprite.SetActive(false);
        }

        if (collision.transform.CompareTag("Stage2"))
        {

        }

        if (collision.transform.CompareTag("Stage3"))
        {

        }
    }
}
