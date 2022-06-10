using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBubblePattern : DogBossPatternBase
{
    [SerializeField] private GameObject bubblePrefab;

    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        dogBossAnimator.SetTrigger("Bubble"); // �ִϸ��̼� ��ȯ

        yield return new WaitForSeconds(waitTime); // ���
        while (dogBoss.dogBossData.p1_AttackCount > currentCount)
        {
            SpawnBubble(); // ����� ����
            yield return new WaitForSeconds(dogBoss.dogBossData.p1_AttackDelayTime); // ���� ���(�ֱ�)
            currentCount++;
        }
        yield return new WaitForSeconds(waitTime); // ���
        dogBossAnimator.SetTrigger("End"); // �ִϸ��̼� ��ȯ
    }

    private void SpawnBubble()
    {
        GameObject clone = Instantiate(bubblePrefab);
        clone.transform.position = new Vector3(Random.Range(-6.0f, 6.0f), -6.0f);
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
