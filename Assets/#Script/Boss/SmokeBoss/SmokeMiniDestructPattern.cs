using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMiniDestructPattern : SmokeBossPatternBase
{
    [SerializeField] private GameObject miniSmokeGameObject;
    [Header("��ġ ������ ��")]
    [SerializeField] MapData mapData;

    private Vector3[] spawnPoints = new Vector3[4];

    public override IEnumerator Attacking()
    {
                int currentCount = 0;
        int spawnPointIndex;

        yield return new WaitForSeconds(waitTime);

        while (currentCount < smokeBoss.smokeBossData.p2_SpawnCount)
        {
            SpawnPointsInit(); // ������ġ ������
            spawnPointIndex = Random.Range(0, spawnPoints.Length); // ������ġ ����
            SpawnMiniSmokeObject(spawnPoints[spawnPointIndex]); // ���� 
            currentCount++;
            yield return new WaitForSeconds(smokeBoss.smokeBossData.p2_RespawnCycleTime);
        }
    }
    private void SpawnMiniSmokeObject(Vector3 pos) // �̴� ��� ���� 
    {
        GameObject clone = Instantiate(miniSmokeGameObject);
        clone.transform.position = pos;
    }

    private void SpawnPointsInit() // ������ġ ������ 
    {
        spawnPoints[0] = new Vector3(Random.Range(mapData.LimitMin.x, mapData.LimitMax.x),
                                                          mapData.LimitMax.y); // ���� ���� ��ġ ���� ���� 

        spawnPoints[1] = new Vector3(mapData.LimitMax.x,
                                             Random.Range(mapData.LimitMin.y, mapData.LimitMax.y)); // ������ ���� ��ġ ���� ���� 

        spawnPoints[2] = new Vector3(Random.Range(mapData.LimitMin.x, mapData.LimitMax.x),
                                                          mapData.LimitMin.y); // ���� ���� ��ġ ���� ���� 

        spawnPoints[3] = new Vector3(mapData.LimitMin.x,
                                             Random.Range(mapData.LimitMin.y, mapData.LimitMax.y)); // ������ ���� ��ġ ���� ���� 
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
