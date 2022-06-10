using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMiniDestructPattern : MonoBehaviour
{
    [SerializeField] private GameObject miniSmokeGameObject;
    [Header("재생성 시간")]
    [SerializeField] private float reCycleTime;
    [Header("생성 갯수")]
    [SerializeField] private int spawnCount; // 몇번까지 생성할것인가?
    [SerializeField] private float waitTime;
    [Header("위치 데이터 값")]
    [SerializeField] MapData mapData;

    private Vector3[] spawnPoints = new Vector3[4];

    public IEnumerator SpawnSmokeMini() // Main System
    {
        int currentCount = 0;
        int spawnPointIndex;

        yield return new WaitForSeconds(waitTime);

        while (currentCount <= spawnCount)
        {
            SpawnPointsInit(); // 스폰위치 랜덤값
            spawnPointIndex = Random.Range(0, spawnPoints.Length); // 랜덤위치 선정
            SpawnMiniSmokeObject(spawnPoints[spawnPointIndex]); // 생성 
            currentCount++;
            yield return new WaitForSeconds(reCycleTime);
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
        //StopCoroutine("ICardKingCardPattern");
        StopAllCoroutines();
    }
}
