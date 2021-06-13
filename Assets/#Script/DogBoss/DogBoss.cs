using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
   
    private Animator animator;

    private DogBubblePattern dogBubblePattern;
    private DogSmallSojuPattern dogSmallSojuPattern;



    private void Start()
    {
        dogBubblePattern = GetComponent<DogBubblePattern>();
        dogSmallSojuPattern = GetComponent<DogSmallSojuPattern>();
        StartCoroutine(DogSmallPattern());
    }

    private IEnumerator DogSmallPattern()
    {
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            yield return StartCoroutine(dogBubblePattern.IBubbleSpawner());
            yield return StartCoroutine(dogSmallSojuPattern.ISojuPattern());
        }
    }
}
