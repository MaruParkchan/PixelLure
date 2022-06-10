using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTorch : MonoBehaviour
{
    [SerializeField] private GameObject effectObject;
    [SerializeField] private float destroyTime; // ������ �ð�

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnDestroy()
    {
        GameObject clone = Instantiate(effectObject);
        clone.transform.position = this.transform.position;
    }
}
