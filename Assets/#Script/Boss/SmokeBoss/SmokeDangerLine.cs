using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDangerLine : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private GameObject ashtrayObject;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnDestroy()
    {
        GameObject clone = Instantiate(ashtrayObject);
        clone.transform.position = new Vector3(transform.position.x, 6.5f, 0);
    }
}
