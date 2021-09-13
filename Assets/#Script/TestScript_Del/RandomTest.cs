using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    [SerializeField]
    private int[] randomValue = new int[4];

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < randomValue.Length; i++) // 중복없는 난수 출력
            {
                randomValue[i] = Random.Range(0, randomValue.Length);
                for (int j = 0; j < i; j++)
                {
                    if (randomValue[i] == randomValue[j])
                    {
                        i--;
                        break;
                    }
                }
            }
        }
    }
}
