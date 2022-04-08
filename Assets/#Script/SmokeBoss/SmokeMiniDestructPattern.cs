using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMiniDestructPattern : MonoBehaviour
{
    [SerializeField] private GameObject miniSmokeGameObject;
    [Header("����� �ð�")]
    [SerializeField] private float reCycleTime;
    [Header("���� ����")]
    [SerializeField] private int spawnCount; // ������� �����Ұ��ΰ�?
    [SerializeField] private float waitTime;
    [Header("��ġ ������ ��")]
    [SerializeField] MapData mapData;

    private Vector3[] spawnPoints = new Vector3[4];

    public IEnumerator SpawnSmokeMini() // Main System
    {
        int currentCount = 0;
        int spawnPointIndex;

        yield return new WaitForSeconds(waitTime);

        while (currentCount <= spawnCount)
        {
            SpawnPointsInit(); // ������ġ ������
            spawnPointIndex = Random.Range(0, spawnPoints.Length); // ������ġ ����
            SpawnMiniSmokeObject(spawnPoints[spawnPointIndex]); // ���� 
            currentCount++;
            yield return new WaitForSeconds(reCycleTime);
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
        //StopCoroutine("ICardKingCardPattern");
        StopAllCoroutines();
    }
}
