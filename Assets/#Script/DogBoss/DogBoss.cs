using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("DogSmallBossPattern");
    }

    private IEnumerator DogSmallBossPattern()
    {
        yield return new WaitForSeconds(4.0f);

        while (true)
        {

        }
    }
}
