using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKingCardPattern : MonoBehaviour
{
    [SerializeField] private GameObject kingCardBulletPrefab;
    [SerializeField] private int spawnCount; // 킹카드 생성 수
    [SerializeField] private float spawnCycleTime; // 재 생성 시간
    [SerializeField] private float waitTime; // 대기 시간
    #region
    private Vector3[] spawnPoints;

    #endregion

    private void Start()
    {
        
    }

    public IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(cardAnimatorSystem.DelayTime);
        for (int i = 0; i < kingCardSpawnPoints.Length; i++) // 중복없는 난수 출력
        {
            pointIndexs[i] = Random.Range(0, kingCardSpawnPoints.Length);
            for (int j = 0; j < i; j++)
            {
                if (pointIndexs[i] == pointIndexs[j])
                {
                    i--;
                    break;
                }
            }
        }

        int currentCount = 0;
        int pointIndexCount = 0; // point Random Value Position

        while (true)
        {
            GameObject obj = Instantiate(kingCardBulletPrefab);
          //  obj.transform.position = kingCardSpawnPoints[pointIndexs[pointIndexCount]].position;

            //if (pointIndexs[pointIndexCount] >= 5 && pointIndexs[pointIndexCount] <= 9)
            //    obj.transform.rotation = Quaternion.Euler(0, -180.0f, 90.0f);
            //else if (pointIndexs[pointIndexCount] >= 10 && pointIndexs[pointIndexCount] <= 18)
            //    obj.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
            //else if (pointIndexs[pointIndexCount] >= 19 && pointIndexs[pointIndexCount] <= 27)
            //    obj.transform.rotation = Quaternion.Euler(0, 0, 0);

            yield return new WaitForSeconds(spawnCycleTime);

            currentCount++;
            pointIndexCount++;

            if (pointIndexCount > kingCardSpawnPoints.Length - 1)
                pointIndexCount = 0;
        }
    }

    private void SpawnPointInit()
    {
        #region 위쪽 생성 포지션
        spawnPoints[0]  = new Vector3(-7.5f, 6.5f, 0f);
        spawnPoints[1]  = new Vector3(-6.5f, 6.5f, 0f);
        spawnPoints[2]  = new Vector3(-5.5f, 6.5f, 0f);
        spawnPoints[3]  = new Vector3(-4.5f, 6.5f, 0f);
        spawnPoints[4]  = new Vector3(-3.5f, 6.5f, 0f);
        spawnPoints[5]  = new Vector3(-2.5f, 6.5f, 0f);
        spawnPoints[6]  = new Vector3(-1.5f, 6.5f, 0f);
        spawnPoints[7]  = new Vector3(-0.5f, 6.5f, 0f);
        spawnPoints[8]  = new Vector3(0f, 6.5f, 0f);
        spawnPoints[9]  = new Vector3(0.5f, 6.5f, 0f);
        spawnPoints[10] = new Vector3(1.5f, 6.5f, 0f);
        spawnPoints[11] = new Vector3(2.5f, 6.5f, 0f);
        spawnPoints[12] = new Vector3(3.5f, 6.5f, 0f);
        spawnPoints[13] = new Vector3(4.5f, 6.5f, 0f);
        spawnPoints[14] = new Vector3(5.5f, 6.5f, 0f);
        spawnPoints[15] = new Vector3(6.5f, 6.5f, 0f);
        spawnPoints[16] = new Vector3(7.5f, 6.5f, 0f);
        #endregion

        #region 아래쪽 생성 포지션
        spawnPoints[17] = new Vector3(-7.5f, -6.5f, 0f);
        spawnPoints[18] = new Vector3(-6.5f, -6.5f, 0f);
        spawnPoints[19] = new Vector3(-5.5f, -6.5f, 0f);
        spawnPoints[20] = new Vector3(-4.5f, -6.5f, 0f);
        spawnPoints[21] = new Vector3(-3.5f, -6.5f, 0f);
        spawnPoints[22] = new Vector3(-2.5f, -6.5f, 0f);
        spawnPoints[23] = new Vector3(-1.5f, -6.5f, 0f);
        spawnPoints[24] = new Vector3(-0.5f, -6.5f, 0f);
        spawnPoints[25] = new Vector3(0f, -6.5f, 0f);
        spawnPoints[26] = new Vector3(0.5f, -6.5f, 0f);
        spawnPoints[27] = new Vector3(1.5f, -6.5f, 0f);
        spawnPoints[28] = new Vector3(2.5f, -6.5f, 0f);
        spawnPoints[29] = new Vector3(3.5f, -6.5f, 0f);
        spawnPoints[30] = new Vector3(4.5f, -6.5f, 0f);
        spawnPoints[31] = new Vector3(5.5f, -6.5f, 0f);
        spawnPoints[32] = new Vector3(6.5f, -6.5f, 0f);
        spawnPoints[33] = new Vector3(7.5f, -6.5f, 0f);
        #endregion

    }
}
