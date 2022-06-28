using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSprayingFirePattern : SmokeBossPatternBase
{
    [SerializeField] private MapData smokeBossMapData;
    [SerializeField] private GameObject SprayingFirePrefab;

    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        while (smokeBoss.smokeBossData.p4_AttackCount > currentCount)
        {
            // smokeBoss.HideorAppear();
            Appear();
            yield return new WaitForSeconds(smokeBoss.smokeBossData.p4_RespawnCycleTime);
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
        float eulerXvalue = 360.0f / smokeBoss.smokeBossData.p4_FireBulletCount;
        for (int i = 0; i < smokeBoss.smokeBossData.p4_FireBulletCount; i++)
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
