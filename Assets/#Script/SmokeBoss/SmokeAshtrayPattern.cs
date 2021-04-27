using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAshtrayPattern : MonoBehaviour
{
    [SerializeField] private GameObject dangerWarningLine;
    [SerializeField] private int spawnCount; // 총 생성 수 
    [SerializeField] private float recycleTime; // 재 생성 시간
    [SerializeField] private float waitTime;

    private Vector3[] spawnPoints = new Vector3[3];

    private void Start()
    {
        SpawnInit();
    }

    public IEnumerator DrapPattern() // Main System
    {
        int currentCount = 0;
        int pointIndex = 0;
        yield return new WaitForSeconds(waitTime);

        while (currentCount < spawnCount)
        {
            pointIndex = Random.Range(0, spawnPoints.Length);
            SpawnObject(dangerWarningLine, spawnPoints[pointIndex]);
            yield return new WaitForSeconds(recycleTime);
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
}
