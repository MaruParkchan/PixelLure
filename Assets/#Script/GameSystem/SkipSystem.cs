using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkipSystem : MonoBehaviour
{
    [SerializeField] private GameObject skipText;
    
    void Start()
    {
        StartCoroutine("ActiveTimer");
    }

    IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(1.0f);
        skipText.SetActive(true);
        
    }
}
