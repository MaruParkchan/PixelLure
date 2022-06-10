using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoomPattern : CardBossPatternBase 
{
    [SerializeField] private GameObject boomEffect;

    public override IEnumerator Attacking()
    {
        Appear(); // 등장

        yield return new WaitForSeconds(waitTime);

        int currentCount = 0;

        while (currentCount < cardBoss.cardBossData.p4_attackCount)
        {
            SpawnBoomEffect();
            currentCount++;
            yield return new WaitForSeconds(cardBoss.cardBossData.p4_attackDelayTime);
        }

        cardBossAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // 대기 시간
    }

    private void SpawnBoomEffect()
    {
        GameObject clone = Instantiate(boomEffect);
        clone.transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x), 
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
    }

    private void Appear()
    {
        // 랜덤 위치 등장
        transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x),
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
        cardBossAnimator.SetTrigger("Appear"); // 등장 애니메이션 재생
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
