using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraShake : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private float duration;

    private float timer;
    private Vector3 originPos;
    private bool isShaking = false;

    public static Action cameraShake;
    public static Action cameraPlayerFoucs;

    private void Awake()
    {
        originPos = transform.localPosition;
        cameraShake = () => { Shake(); };
        cameraPlayerFoucs = () => { CameraPlayerFoucs(); };
    }

    public void Shake()
    {
        StartCoroutine(IShake());
    }

    IEnumerator IShake()
    {
        if (isShaking)
            yield break;

        while(timer <= duration)
        {
            isShaking = true;
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * amount + originPos;
            timer += Time.deltaTime;
            yield return null;

        }
        transform.localPosition = originPos;
        timer = 0;
        isShaking = false;
        yield return null;
    }

    public void CameraPlayerFoucs()
    {
        StopAllCoroutines();
         Transform playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
         transform.position  = new Vector3(playerPos.position.x, playerPos.position.y, -10.0f);
         transform.GetComponent<Camera>().orthographicSize = 1.5f;
    }
}
