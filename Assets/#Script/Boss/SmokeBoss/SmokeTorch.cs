using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTorch : MonoBehaviour
{
    [SerializeField] private GameObject effectObject;


    private void Start()
    {
        if (GameSystem.isAccept)
            StartCoroutine(TorchBoom(Random.Range(0.5f, 1.3f)));
        else
            StartCoroutine(TorchBoom(1.0f));
    }


    private IEnumerator TorchBoom(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject clone = Instantiate(effectObject);
        clone.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
}
