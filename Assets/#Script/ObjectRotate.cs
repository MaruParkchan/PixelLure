using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }

    public void RotateInit(float value)
    {
        rotateSpeed = value;
    }
}
