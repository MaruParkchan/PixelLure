using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKingCardPattern : CardBossPatternBase
{
    [SerializeField] private GameObject kingCardBulletPrefab;

    #region ��ǥ �����Ͱ�
    private Vector3[] spawnPoints = new Vector3[56];
    private int[] spawnPointIndexs = new int[56];
    #endregion

    public override IEnumerator Attacking()
    {
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < spawnPoints.Length; i++) // �ߺ����� ���� ���
        {
            spawnPointIndexs[i] = Random.Range(0, spawnPoints.Length);
            for (int j = 0; j < i; j++)
            {
                if (spawnPointIndexs[i] == spawnPointIndexs[j])
                {
                    i--;
                    break;
                }
            }
        }
        int currentCount = 0;

        while (cardBoss.cardBossData.p3_attackCount > currentCount)
        {
            GameObject obj = Instantiate(kingCardBulletPrefab);
            obj.transform.position = spawnPoints[spawnPointIndexs[currentCount]];
            // WaitTime, MoveSpeed, accSpeed
            obj.GetComponent<CardKingBullet>().KingCardInit(cardBoss.cardBossData.p3_accelerationWaitTime,
                cardBoss.cardBossData.p3_initialMoveSpeed, cardBoss.cardBossData.p3_accelerationSpeed);

            if (spawnPointIndexs[currentCount] >= 0 && spawnPointIndexs[currentCount] <= 16)
                obj.transform.rotation = Quaternion.Euler(0f, 0f, 180.0f);
            else if (spawnPointIndexs[currentCount] >= 17 && spawnPointIndexs[currentCount] <= 33)
                obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            else if (spawnPointIndexs[currentCount] >= 34 && spawnPointIndexs[currentCount] <= 44)
                obj.transform.rotation = Quaternion.Euler(0f, 0f, -90.0f);
            else if (spawnPointIndexs[currentCount] >= 45 && spawnPointIndexs[currentCount] <= 55)
                obj.transform.rotation = Quaternion.Euler(0f, 0f, 90.0f);

            currentCount++;

            yield return new WaitForSeconds(cardBoss.cardBossData.p3_attackDelayTime);
        }
    }
    protected override void Init()
    {
        SpawnPointInit();
    }
    private void SpawnPointInit()
    {
        #region ���� ���� ������ 0 ~ 16
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

        #region �Ʒ��� ���� ������ 17 ~ 33
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

        #region ���� ���� ������ 34 ~ 44
        spawnPoints[34] = new Vector3(-10.5f, -4.5f, 0f);
        spawnPoints[35] = new Vector3(-10.5f, -3.5f, 0f);
        spawnPoints[36] = new Vector3(-10.5f, -2.5f, 0f);
        spawnPoints[37] = new Vector3(-10.5f, -1.5f, 0f);
        spawnPoints[38] = new Vector3(-10.5f, -0.5f, 0f);
        spawnPoints[39] = new Vector3(-10.5f, 0f, 0f);
        spawnPoints[40] = new Vector3(-10.5f, 0.5f, 0f);
        spawnPoints[41] = new Vector3(-10.5f, 1.5f, 0f);
        spawnPoints[42] = new Vector3(-10.5f, 2.5f, 0f);
        spawnPoints[43] = new Vector3(-10.5f, 3.5f, 0f);
        spawnPoints[44] = new Vector3(-10.5f, 4.5f, 0f);
        #endregion

        #region ������ ���� ������ 45 ~ 55
        spawnPoints[45] = new Vector3(10.5f, -4.5f, 0f);
        spawnPoints[46] = new Vector3(10.5f, -3.5f, 0f);
        spawnPoints[47] = new Vector3(10.5f, -2.5f, 0f);
        spawnPoints[48] = new Vector3(10.5f, -1.5f, 0f);
        spawnPoints[49] = new Vector3(10.5f, -0.5f, 0f);
        spawnPoints[50] = new Vector3(10.5f, 0f, 0f);
        spawnPoints[51] = new Vector3(10.5f, 0.5f, 0f);
        spawnPoints[52] = new Vector3(10.5f, 1.5f, 0f);
        spawnPoints[53] = new Vector3(10.5f, 2.5f, 0f);
        spawnPoints[54] = new Vector3(10.5f, 3.5f, 0f);
        spawnPoints[55] = new Vector3(10.5f, 4.5f, 0f);
        #endregion
    }
    public void CoroutineStop()
    {
        StopAllCoroutines();
    }


}
