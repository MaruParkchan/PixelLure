using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoomPattern : CardBossPatternBase 
{
    [SerializeField] private GameObject boomEffect;

    public override IEnumerator Attacking()
    {
        Appear(); // ����

        yield return new WaitForSeconds(waitTime);

        int currentCount = 0;

        while (currentCount < cardBoss.cardBossData.p4_attackCount)
        {
            SpawnBoomEffect();
            currentCount++;
            yield return new WaitForSeconds(cardBoss.cardBossData.p4_attackDelayTime);
        }

        cardBossAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // ��� �ð�
    }

    private void SpawnBoomEffect()
    {
        GameObject clone = Instantiate(boomEffect);
        clone.transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x), 
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
    }

    private void Appear()
    {
        // ���� ��ġ ����
        transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x),
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
        cardBossAnimator.SetTrigger("Appear"); // ���� �ִϸ��̼� ���
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
