using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private float duration;

    private float timer;
    private Vector3 originPos;

    private void Awake()
    {
        originPos = transform.localPosition;
    }

    private void Start()
    {
        timer = duration;
    }

    private void Update()
    {
        Shake();
    }

    private void Shake()
    {
        if (timer <= duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * amount + originPos;

            timer += Time.deltaTime;
        }
        else
            transform.localPosition = originPos;
    }

    public void ShakeStart()
    {
        timer = 0;
    }
}
