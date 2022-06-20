using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMiniDestructPattern : SmokeBossPatternBase
{
    [SerializeField] private GameObject miniSmokeGameObject;
    [Header("위치 데이터 값")]
    [SerializeField] MapData mapData;

    private Vector3[] spawnPoints = new Vector3[4];

    public override IEnumerator Attacking()
    {
                int currentCount = 0;
        int spawnPointIndex;

        yield return new WaitForSeconds(waitTime);

        while (currentCount < smokeBoss.smokeBossData.p2_SpawnCount)
        {
            SpawnPointsInit(); // 스폰위치 랜덤값
            spawnPointIndex = Random.Range(0, spawnPoints.Length); // 랜덤위치 선정
            SpawnMiniSmokeObject(spawnPoints[spawnPointIndex]); // 생성 
            currentCount++;
            yield return new WaitForSeconds(smokeBoss.smokeBossData.p2_RespawnCycleTime);
        }
    }
    private void SpawnMiniSmokeObject(Vector3 pos) // 미니 담배 생성 
    {
        GameObject clone = Instantiate(miniSmokeGameObject);
        clone.transform.position = pos;
    }

    private void SpawnPointsInit() // 스폰위치 랜덤값 
    {
        spawnPoints[0] = new Vector3(Random.Range(mapData.LimitMin.x, mapData.LimitMax.x),
                                                          mapData.LimitMax.y); // 위쪽 방향 위치 랜덤 생성 

        spawnPoints[1] = new Vector3(mapData.LimitMax.x,
                                             Random.Range(mapData.LimitMin.y, mapData.LimitMax.y)); // 오른쪽 방향 위치 랜덤 생성 

        spawnPoints[2] = new Vector3(Random.Range(mapData.LimitMin.x, mapData.LimitMax.x),
                                                          mapData.LimitMin.y); // 위쪽 방향 위치 랜덤 생성 

        spawnPoints[3] = new Vector3(mapData.LimitMin.x,
                                             Random.Range(mapData.LimitMin.y, mapData.LimitMax.y)); // 오른쪽 방향 위치 랜덤 생성 
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
