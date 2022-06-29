using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSprayingFirePattern : SmokeBossPatternBase
{
    [SerializeField] private MapData smokeBossMapData;
    [SerializeField] private GameObject SprayingFirePrefab;
    private float respawnCycleTime = 1.4f;

    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        while (smokeBoss.smokeBossData.p4_AttackCount > currentCount)
        {
            // smokeBoss.HideorAppear();
            Appear();
            yield return new WaitForSeconds(respawnCycleTime);
            currentCount++;
        }
    }

    private void Appear()
    {
        transform.position = new Vector3(Random.Range(smokeBossMapData.LimitMin.x, smokeBossMapData.LimitMax.x),
                                         Random.Range(smokeBossMapData.LimitMin.y, smokeBossMapData.LimitMax.y));

        smokeBoss.HideorAppear();
        smokeBossAnimator.SetTrigger("Ready");
    }

    public void EngrgyBoom()
    {
        int fireBulletCount;

        if(GameSystem.isAccept)
            fireBulletCount = Random.Range(smokeBoss.smokeBossData.p4_FireBulletMinCount, smokeBoss.smokeBossData.p4_FireBulletMaxCount);
        else 
            fireBulletCount = 6;

        float eulerXvalue = 360.0f / fireBulletCount;
        
        for (int i = 0; i < fireBulletCount; i++)
        {
            GameObject clone = Instantiate(SprayingFirePrefab);
            clone.transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y + 0.5f);
            clone.transform.rotation = Quaternion.Euler(0,0, eulerXvalue * i);

            Debug.Log(eulerXvalue * i);
        }
        smokeBoss.HideorAppear();
        smokeBossAnimator.SetTrigger("Hide");
    }

    public void CoroutineStop()
    {
        //StopCoroutine("ICardKingCardPattern");
        StopAllCoroutines();
    }

    protected override void Init()
    {

    }
}
