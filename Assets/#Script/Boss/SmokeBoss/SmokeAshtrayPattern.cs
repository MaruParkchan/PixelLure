using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAshtrayPattern : SmokeBossPatternBase
{
    [SerializeField] private GameObject dangerWarningLine;

    private Vector3[] spawnPoints = new Vector3[3];

    protected override void Init()
    {
        SpawnInit();
    }

    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        int pointIndex = 0;
        yield return new WaitForSeconds(waitTime);

        while (currentCount < smokeBoss.smokeBossData.p3_SpawnCount)
        {
            pointIndex = Random.Range(0, spawnPoints.Length);
            SpawnObject(dangerWarningLine, spawnPoints[pointIndex]);
            yield return new WaitForSeconds(smokeBoss.smokeBossData.p3_RespawnCycleTime);
            currentCount++;
        }
   }

    private void SpawnInit() // 위치 3곳 지정
    {
        spawnPoints[0] = new Vector3(-5.5f, 0);
        spawnPoints[1] = new Vector3(0, 0);
        spawnPoints[2] = new Vector3(5.5f, 0);
    }

    private void SpawnObject(GameObject obj, Vector3 spawnPoint)
    {
        GameObject clone = Instantiate(obj);
        clone.transform.position = spawnPoint;
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }
}

