using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
   
    private Animator animator;

    private DogBubblePattern dogBubblePattern;
    private DogSmallSojuPattern dogSmallSojuPattern;
    private DogBigTracePattern dogBigTracePattern;
    private DogBigLaserPattern dogBigLaserPattern;
    private DogBigPoundingPattern dogBigPoundingPattern;

    [SerializeField]
    private bool isBulkUp; // 벌크업하였는가?

    private void Start()
    {
        dogBubblePattern = GetComponent<DogBubblePattern>();
        dogSmallSojuPattern = GetComponent<DogSmallSojuPattern>();
        dogBigTracePattern = GetComponent<DogBigTracePattern>();
        dogBigLaserPattern = GetComponent<DogBigLaserPattern>();
        dogBigPoundingPattern = GetComponent<DogBigPoundingPattern>();
        StartCoroutine(DogPattern());
    }

    private IEnumerator DogPattern()
    {
        yield return new WaitForSeconds(4.0f);
        while (true)
        {
            if (isBulkUp == false)
            {
                yield return StartCoroutine(dogBubblePattern.IBubbleSpawner());
                yield return StartCoroutine(dogSmallSojuPattern.ISojuPattern());
            }
            else
            {
               //yield return StartCoroutine(dogBigTracePattern.ISpawnSoju());
               //yield return StartCoroutine(dogBigLaserPattern.ILaserPattern());
                yield return StartCoroutine(dogBigPoundingPattern.ISojuRain());
            }
        }
    }
}
