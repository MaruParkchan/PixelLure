using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    public void DestroyTimer(float timer)
    {
        Destroy(this.gameObject, timer);
    }
}
