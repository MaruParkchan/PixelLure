using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAshtray : MonoBehaviour
{
    [SerializeField] private float moveTime; // 해당 지점까지 이동 속도
    private bool isShake = false;
    private float yLimitValue = 6.5f;

    private void Start()
    {
        StartCoroutine("DropAshtray");
    }

    private IEnumerator DropAshtray()
    {
        yield return SmoothMovement(new Vector2(transform.position.x, -yLimitValue - 7.0f));
        CameraShake.cameraShake();
    }

    private IEnumerator SmoothMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        while (percent < moveTime) // startPosition 에서 EndPosition까지 moveTime동안 이동
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
    }
}
