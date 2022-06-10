using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRadialShapePattern : CardBossPatternBase
{
    [SerializeField]
    private GameObject cardRadialShapeEffect; // ����� ����Ʈ 

    public override IEnumerator Attacking()
    {
        int count = 0;
        Appear(); // ����
        yield return new WaitForSeconds(waitTime); // ��� �ð�
        while (cardBoss.cardBossData.p1_attackCount > count)
        {
            cardBossAnimator.SetTrigger("Attack1");
            yield return new WaitForSeconds(1.0f); // ���� �ӵ�, �����Ǿ���� ������ġ�� �ִϸ��̼� ����
            count++; 
        }
        cardBossAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // ��� �ð�
    }

    public void SpawnCardRadialShapeEffect() // ���� ���� - 1 ���� (�����)
    {
        GameObject clone = Instantiate(cardRadialShapeEffect);
        clone.transform.position = this.transform.position;
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
        //attackDelayTime = 1.0f; // �����Ǿ���� ������ġ�� �ִϸ��̼� ����
    }

}
