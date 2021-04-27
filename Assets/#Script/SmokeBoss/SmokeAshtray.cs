using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAshtray : MonoBehaviour
{
    [SerializeField] private float moveTime; // �ش� �������� �̵� �ӵ�
    private CameraShake cameraShake;
    private bool isShake = false;
    private float yLimitValue = 6.5f;

    private void Start()
    {
        cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        StartCoroutine("DropAshtray");
    }

    private IEnumerator DropAshtray()
    {
        yield return SmoothMovement(new Vector2(transform.position.x, -yLimitValue));
        cameraShake.ShakeStart();
    }

    private IEnumerator SmoothMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        while (percent < moveTime) // startPosition ���� EndPosition���� moveTime���� �̵�
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
    }
}
