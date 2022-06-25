using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeRandomMovePattern : SmokeBossPatternBase
{
    [SerializeField] private MapData smokeBossMapData;
    [SerializeField] private GameObject touchObject;
    [SerializeField] private int attackCount;
    [SerializeField] private float moveTime;
    [SerializeField] private float cycleTime;
    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        Appear();
        while (attackCount > currentCount)
        {
            yield return StartCoroutine("SmoothMovement");
            yield return new WaitForSeconds(cycleTime);
            currentCount++;
        }

        smokeBossAnimator.SetTrigger("Hide");
         smokeBoss.HideorAppear();
         
        //yield return new WaitForSeconds(1.0f);
    }

    protected override void Init()
    {
       
    }

    private void Appear()
    {
        transform.position = new Vector3(Random.Range(smokeBossMapData.LimitMin.x, smokeBossMapData.LimitMax.x),
                                         Random.Range(smokeBossMapData.LimitMin.y, smokeBossMapData.LimitMax.y));

        smokeBossAnimator.SetTrigger("Appear");
        smokeBoss.HideorAppear();
    }
    private IEnumerator SmoothMovement()
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = new Vector3(Random.Range(smokeBossMapData.LimitMin.x, smokeBossMapData.LimitMax.x),
                                         Random.Range(smokeBossMapData.LimitMin.y, smokeBossMapData.LimitMax.y));
        float percent = 0;
        //StartCoroutine("TouchDrop");
        while (percent < moveTime) // startPosition 에서 EndPosition까지 moveTime동안 이동
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);
            yield return null;
        }
        SpawnObject(touchObject);
        yield break;
    }

    private void SpawnObject(GameObject spawnObject)
    {
        GameObject clone = Instantiate(spawnObject);
        clone.transform.position = this.transform.position;
    }

}
