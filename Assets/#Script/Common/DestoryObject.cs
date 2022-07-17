using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    public void DestroyTimer(float timer)
    {
        Destroy(this.gameObject, timer);
    }
}
