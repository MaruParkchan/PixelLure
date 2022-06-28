using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFireMove : MonoBehaviour
{
    [SerializeField] private float minMoveSpeedValue;
    [SerializeField] private float maxMoveSpeedValue;
    [SerializeField] private GameObject bombEffect;

    private float moveSpeed;
    private float bombTimer;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeedValue, maxMoveSpeedValue);
        bombTimer = Random.Range(0.5f,1.5f);

        Destroy(this.gameObject, bombTimer);
    }

    private void Update()
    {
        transform.Translate(Vector3.right* Time.deltaTime * moveSpeed);
    }

    private void OnDestroy()
    {
         GameObject clone = Instantiate(bombEffect);
         clone.transform.position = transform.position;
    }
}
