using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Stage1"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
        }

        if (collision.transform.CompareTag("Stage2"))
        {

        }

        if (collision.transform.CompareTag("Stage3"))
        {

        }
    }
}
