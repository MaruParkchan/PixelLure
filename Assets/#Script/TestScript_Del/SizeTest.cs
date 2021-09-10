using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeTest : MonoBehaviour
{
    [SerializeField]
    private GameObject clone;

    [SerializeField]
    private float size;

    private void Update()
    {
        clone.transform.localScale = new Vector3(1, size, 1);
    }

}
